using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nethereum.TryOnBrowser.Repositories;
using Nethereum.TryOnBrowser.Components;
using Nethereum.TryOnBrowser.Components.Modal;
using Nethereum.TryOnBrowser.Components.Monaco;

namespace Nethereum.TryOnBrowser.Pages
{
    // Based on https://github.com/Suchiman/Runny all credit to him

    public abstract class EditorModelBase : ComponentBase
    {

        protected EditorModel editorModel;
        [Inject] ModalService ModalServices { get; set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }
        protected string Output { get; set; }
        [Inject] private HttpClient Client { get; set; }

        [Inject] public CodeSampleRepository CodeSampleRepository { get; set; }

        public List<CodeSample> CodeSamples { get; protected set; }
        public int SelectedCodeSample { get; protected set; }

        private LoadFileModel loadFileModel;

        private SaveAsFileModel savesAsFileModel;

        public async Task FileLoaded(string content, string fileName)
        {
            ModalServices.Close();
            editorModel.Script = content;
            await Interop.EditorSetAsync(JSRuntime, editorModel);

            await AddNewCodeSample(content, fileName);
        }

        private async Task AddNewCodeSample(string content, string fileName)
        {
            var codeSample = new CodeSample
            {
                Code = content,
                Name = fileName,
                Language = GetCodeLanguage(),
                Custom = true
            };
            CodeSamples.Add(codeSample);
            await CodeSampleRepository.AddCodeSampleAsync(codeSample);

            SelectedCodeSample = CodeSamples.Count - 1;
            StateHasChanged();
        }

        public abstract CodeLanguage GetCodeLanguage();

        protected override async Task OnInitAsync()
        {
            await base.OnInitAsync();
            loadFileModel = new LoadFileModel();
            loadFileModel.AllowedExtension = GetAllowedExtension();
            loadFileModel.ContentLoaded += FileLoaded;

            savesAsFileModel = new SaveAsFileModel();
            savesAsFileModel.SaveFileAs += SaveAsAsyncCallBack;

            await LoadCodeSamplesAsync();

            editorModel = new EditorModel
            {
                Language = GetEditorLanguage(),
                Script = CodeSamples[SelectedCodeSample].Code
            };

            Compiler.InitializeMetadataReferences(Client);

        }

        public abstract string GetEditorLanguage();
        public abstract string GetAllowedExtension();


        public async Task SaveAsAsyncCallBack(string content, string name)
        {
            ModalServices.Close();
            await AddNewCodeSample(content, name);
        }

        public void Run()
        {
            Compiler.WhenReady(RunInternal);
        }

        public async Task LoadSavedAsync()
        {
            await CodeSampleRepository.LoadUserSamplesAsync();
            await LoadCodeSamplesAsync();
        }

        public async Task SaveAsync()
        {
            editorModel = await Interop.EditorGetAsync(JSRuntime, editorModel);
            CodeSamples[SelectedCodeSample].Code = editorModel.Script;
            await CodeSampleRepository.SaveCustomCodeSamples();
        }

        public async Task RemoveAsync()
        {
            await CodeSampleRepository.RemoveCodeSampleAsync(CodeSamples[SelectedCodeSample]);
            CodeSamples.Remove(CodeSamples[SelectedCodeSample]);
            SelectedCodeSample = 0;
        }

        public async Task SaveAsAsync()
        {
            editorModel = await Interop.EditorGetAsync(JSRuntime, editorModel);
            savesAsFileModel.Init(CodeSamples[SelectedCodeSample].Name, editorModel.Script);
            ModalServices.ShowModal<SaveAsFile, SaveAsFileModel>("Save As", savesAsFileModel, "Model");
            StateHasChanged();
        }

        public async Task LoadCodeSamplesAsync()
        {
            CodeSamples = new List<CodeSample>();
            var codeSamples = await CodeSampleRepository.GetCodeSamplesAsync(GetCodeLanguage());
            CodeSamples.AddRange(codeSamples);
            SelectedCodeSample = 0;
        }

        public async Task LoadFromFileAsync()
        {
            ModalServices.ShowModal<LoadFile, LoadFileModel>("Load File", loadFileModel, "Model");
            StateHasChanged();
        }

        public async Task CodeSampleChanged(UIChangeEventArgs evt)
        {
            SelectedCodeSample = Int32.Parse(evt.Value.ToString());
            editorModel.Script = CodeSamples[SelectedCodeSample].Code;
            await Interop.EditorSetAsync(JSRuntime, editorModel);
        }

        public string GetDisplayTitle(CodeSample codeSample)
        {
            return Truncate(codeSample.DisplayTitle, 80);
        }

        public string Truncate(string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }

        public async Task RunInternal()
        {
            Output = "";
            editorModel = await Interop.EditorGetAsync(JSRuntime, editorModel);

            var currentOut = Console.Out;
            var writer = new StringWriter();
            Console.SetOut(writer);

            Exception exception = null;
            try
            {
                await CompileAndRun();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Output += writer.ToString();

            if (exception != null)
            {
                Output += "\r\n" + exception.ToString();
            }

            Console.SetOut(currentOut);

            StateHasChanged();
        }

        protected abstract Task CompileAndRun();
       
    }

    public class IndexModel : EditorModelBase
    {
        public override string GetEditorLanguage()
        {
            return "csharp";
        }

        public override string GetAllowedExtension()
        {
            return ".cs";
        }

        public override CodeLanguage GetCodeLanguage()
        {
            return CodeLanguage.CSharp;
        }

        protected override async Task CompileAndRun()
        {
            var (success, asm, rawBytes) = Compiler.LoadSource(editorModel.Script, editorModel.Language);

            if (success)
            {
                var assembly = AppDomain.CurrentDomain.Load(rawBytes);
                var entry = assembly.EntryPoint;
                if (entry.Name == "<Main>") // sync wrapper over async Task Main
                {
                    entry = entry.DeclaringType.GetMethod("Main",
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static); // reflect for the async Task Main
                }

                var hasArgs = entry.GetParameters().Length > 0;
                var result = entry.Invoke(null, hasArgs ? new object[] { new string[0] } : null);
                if (result is Task t)
                {
                    await t;
                }
            }
        }

    }

   

}

