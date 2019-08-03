using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Collections.Generic;
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
    public class IndexModel : ComponentBase
    {
        protected EditorModel editorModel;
        [Inject] ModalService ModalServices { get; set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }        
        protected string Output {get; set;}
        [Inject] private HttpClient Client { get; set; }

        [Inject] public CodeSampleRepository CodeSampleRepository { get; set;}

        public List<CodeSample> CodeSamples { get; protected set; }
        public int SelectedCodeSample { get; protected set; }

        private LoadFileModel loadFileModel;

        public async Task FileLoaded(string content, string fileName)
        {
            ModalServices.Close();
            editorModel.Script = content;
            await Interop.EditorSetAsync(JSRuntime, editorModel);

            // create a CodeSample object for this, and point to it
            var codeSample = new CodeSample {Code = content,
                                            Name = "My samples: " + fileName,
                                            Language = CodeLanguage.CSharp,
                                            Custom = true};
            CodeSamples.Add(codeSample);
            await CodeSampleRepository.AddCodeSampleAsync(codeSample);

            SelectedCodeSample = CodeSamples.Count -1;
            StateHasChanged();
        }

        protected override async Task OnInitAsync()
        {
            await base.OnInitAsync();
            loadFileModel = new LoadFileModel();
            loadFileModel.AllowedExtension = ".cs";
            loadFileModel.ContentLoaded += FileLoaded;

            await LoadCodeSamplesAsync();

            editorModel = new EditorModel
            {
                Language = "csharp",
                Script = CodeSamples[SelectedCodeSample].Code
            };

            Compiler.InitializeMetadataReferences(Client);
           
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

        }

        public async Task LoadCodeSamplesAsync()
        {
            CodeSamples = new List<CodeSample>();
            var codeSamples = await CodeSampleRepository.GetCodeSamplesAsync(CodeLanguage.CSharp);
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

        public async Task RunInternal()
        {
            Output = "";
            editorModel = await Interop.EditorGetAsync(JSRuntime, editorModel);
            Console.WriteLine("Compiling and Running code");

            var sw = Stopwatch.StartNew();

            var currentOut = Console.Out;
            var writer = new StringWriter();
            Console.SetOut(writer);

            Exception exception = null;
            try
            {
                var (success, asm, rawBytes) = Compiler.LoadSource(editorModel.Script, editorModel.Language);

                if (success)
                {
                    var assembly = AppDomain.CurrentDomain.Load(rawBytes);
                    var entry = assembly.EntryPoint;
                    if (entry.Name == "<Main>") // sync wrapper over async Task Main
                    {
                        entry = entry.DeclaringType.GetMethod("Main", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static); // reflect for the async Task Main
                    }
                    var hasArgs = entry.GetParameters().Length > 0;
                    var result = entry.Invoke(null, hasArgs ? new object[] { new string[0] } : null);
                    if (result is Task t)
                    {
                        await t;
                    }
                }
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
            Console.WriteLine("Output " + Output);

            sw.Stop();

            Console.WriteLine("Done in " + sw.ElapsedMilliseconds + "ms");
            StateHasChanged();
        }
    }
}

