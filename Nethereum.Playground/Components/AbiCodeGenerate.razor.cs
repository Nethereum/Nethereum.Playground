using System;
using System.Linq;
using System.Threading.Tasks;
using Blazor.FileReader;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nethereum.Playground.Components.Modal;

namespace Nethereum.Playground.Components
{
    public class AbiCodeGenerateBase: ComponentBase
    {
        [Inject] public IFileReaderService FileReaderService { get; set; }

        [Parameter]
        public AbiCodeGenerateModel Model { get; set; }

        public ElementReference ABIFileUpload { get; set; }
        public ElementReference ByteCodeFileUpload { get; set; }

        public async Task ReadFileABI()
        {
            var files = await FileReaderService.CreateReference(ABIFileUpload).EnumerateFilesAsync();
            var stream = await files.First().CreateMemoryStreamAsync();
            var streamReader = new System.IO.StreamReader(stream);
            Model.Abi = streamReader.ReadToEnd();
        }

        public async Task ReadFileByteCode()
        {
            var files = await FileReaderService.CreateReference(ByteCodeFileUpload).EnumerateFilesAsync();
            var stream = await files.First().CreateMemoryStreamAsync();
            var streamReader = new System.IO.StreamReader(stream);
            Model.ByteCode = streamReader.ReadToEnd();
        }
    }
}