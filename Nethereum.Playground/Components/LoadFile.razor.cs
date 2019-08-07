using System;
using System.Linq;
using System.Threading.Tasks;
using Blazor.FileReader;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nethereum.Playground.Components.Modal;

namespace Nethereum.Playground.Components
{
    public class LoadFileBase: ComponentBase
    {
        ///[Inject] public IJSRuntime JSRuntime { get; set; }

        [Inject] public IFileReaderService FileReaderService { get; set; }

        public ElementRef FileUpload { get; set; }

        [Parameter]
        public LoadFileModel Model { get; set; } = new LoadFileModel(){AllowedExtension = ".cs"};
        
        public void OnChange(UIEventArgs eventArgs)
        {
            var changeEventArgs = (UIChangeEventArgs)eventArgs;
            Model.FileName = changeEventArgs.Value.ToString().Remove(0, changeEventArgs.Value.ToString().LastIndexOf("\\") + 1);
        }

        public async Task ReadFile()
        {
            //var fileReaderService = new Blazor.FileReader.FileReaderService(JSRuntime);
            var files = await FileReaderService.CreateReference(FileUpload).EnumerateFilesAsync();
            Console.WriteLine(files.Count());
            var stream = await files.First().CreateMemoryStreamAsync(); //assuming it's only one file (?)
            var streamReader = new System.IO.StreamReader(stream);
            var fileContent = streamReader.ReadToEnd();
            await Model.SetFileContentAsync(fileContent);

        }
    }
}