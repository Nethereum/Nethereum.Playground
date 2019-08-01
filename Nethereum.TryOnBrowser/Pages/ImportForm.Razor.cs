using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;
using Blazor.FileReader;
using Microsoft.JSInterop;
using System.IO;
using Nethereum.TryOnBrowser.Monaco;

namespace Nethereum.TryOnBrowser.Pages
{
    public class ImportFormModel : ComponentBase
    {
        public ElementRef fileUpload { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }

        public async Task<string> readFile()
        {
            var fileReaderService = new FileReaderService(JSRuntime);
            var file = await fileReaderService.CreateReference(fileUpload).EnumerateFilesAsync();
            Stream stream = await file.First().CreateMemoryStreamAsync(); //assuming it's only one file (?)
            var sr = new StreamReader(stream);
            var filecontent = sr.ReadToEnd();
            return filecontent;
        }

        public async Task ImportFile()
        {
            // create a new code sample

        }
    }
}
