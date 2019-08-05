using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nethereum.TryOnBrowser.Components.FileUtils;
using Nethereum.TryOnBrowser.Components.Modal;
using Nethereum.TryOnBrowser.Components.Monaco;
using Nethereum.TryOnBrowser.Repositories;

namespace Nethereum.TryOnBrowser.Components.PlaygroundEditor
{
    // Based on https://github.com/Suchiman/Runny all credit to him

    public class PlaygroundEditorViewModel : ComponentBase
    {

        protected EditorModel editorModel;
        [Inject] ModalService ModalServices { get; set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }
        protected string Output { get; set; }
        [Inject] private HttpClient Client { get; set; }

        [Inject] public CodeSampleRepository CodeSampleRepository { get; set; }

        [Inject] public Compiler Compiler { get;set;}

        public List<CodeSample> CodeSamples { get; protected set; }
        public int SelectedCodeSample { get; protected set; }

        private LoadFileModel loadFileModel;

        private SaveAsFileModel savesAsFileModel;
        [Parameter] CodeLanguage CodeLanguage { get; set; }

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
                Language = CodeLanguage,
                Custom = true
            };
            CodeSamples.Add(codeSample);
            await CodeSampleRepository.AddCodeSampleAsync(codeSample);

            SelectedCodeSample = CodeSamples.Count - 1;
            StateHasChanged();
        }

        private Timer _timer;

        protected override async Task OnInitAsync()
        {
            loadFileModel = new LoadFileModel();
            loadFileModel.AllowedExtension = GetAllowedExtension();
            loadFileModel.ContentLoaded += FileLoaded;

            savesAsFileModel = new SaveAsFileModel();
            savesAsFileModel.SaveFileAs += SaveAsAsyncCallBack;
         
        }
        
        protected override async Task OnParametersSetAsync()
        {
            await LoadCodeSamplesAsync();

            editorModel = new EditorModel
            {
                Language = GetEditorLanguage(),
                Script = CodeSamples[SelectedCodeSample].Code
            };
            await base.OnParametersSetAsync();
        }

        /// <summary>
        /// Super workaround:
        /// Storage is not available until we have rendered,
        /// We can then load User code samples stored in storage
        /// But if we call StateHasChanged then the Editor is duplicated
        /// So to avoid this a timer is set to run only one which will call StateHasChanged after
        /// 1 second
        /// </summary>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync()
        {
          
            if (!CodeSampleRepository.LoadedUserSamples)
            {
                await LoadSavedAsync();
                //waiting a second to do a state has changed to display the items
                // and avoid confusion with the editor.
                _timer = new Timer(new TimerCallback(_ => {
                    StateHasChanged();
                    _timer.Dispose();
                }), null, 1000, 1000);
            }
          
        }


        public string GetEditorLanguage()
        {
            switch (CodeLanguage)
            {
                case CodeLanguage.CSharp:
                    return "csharp";
                case CodeLanguage.VbNet:
                    return "vb";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public string GetAllowedExtension()
        {
            switch (CodeLanguage)
            {
                case CodeLanguage.CSharp:
                    return ".cs";
                case CodeLanguage.VbNet:
                    return ".vb";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


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

        public async Task SaveToFileAsync()
        {
            editorModel = await Interop.EditorGetAsync(JSRuntime, editorModel);
            await JSRuntime.SaveAs(CodeSamples[SelectedCodeSample].GetFileName(),
                UTF8Encoding.Unicode.GetBytes(editorModel.Script));
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
            var codeSamples = await CodeSampleRepository.GetCodeSamplesAsync(CodeLanguage);
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

        protected Task CompileAndRun()
        {
            switch (CodeLanguage)
            {
                case CodeLanguage.CSharp:
                    return CompileAndRunCsharp();
                case CodeLanguage.VbNet:
                    return CompileAndRunVbNet();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected async Task CompileAndRunCsharp()
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
                if (result is Task task)
                {
                    await task;
                }
            }
        }

        protected async Task CompileAndRunVbNet()
        {
            var (success, asm, rawBytes) = Compiler.LoadSource(editorModel.Script, editorModel.Language);
            if (success)

            {
                // well this is interesting - We can't do async task main in VB
                // attempting to run an async function through Sub Main causes Invoke to hang
                // until a more concrete solution is found, running async should be done through another function (RunAsync() as Task)
                // non-async ones can run through Sub Main or RunAsync (as a function name though not as apt)
                var assembly = AppDomain.CurrentDomain.Load(rawBytes);

                // check if RunAsync exists, favor this over Main
                var RunAsyncExists = (from type in assembly.GetTypes()
                    where type.GetMethod("RunAsync") != null
                    select type.GetMethod("RunAsync")).Any();

                var entry = assembly.EntryPoint;
                if (RunAsyncExists) // if RunAsync does not exist, fallback to Main
                {
                    entry = entry.DeclaringType.GetMethod("RunAsync",
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static); // reflect for RunAsync
                }
                else
                {
                    entry = entry.DeclaringType.GetMethod("Main",
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static); // reflect for Main
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

