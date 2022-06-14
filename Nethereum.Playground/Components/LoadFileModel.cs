using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Nethereum.Playground.Components
{
    public class LoadFileModel
    {
        private string _fileContent;
        private IEnumerable<MemoryStream> _fileStreamsContent;

        public string FileName { get; set; }
        public string AllowedExtension { get; set; }
        public bool StreamsOutput { get; set; } = false;

        public string SubText { get; set; }

        public string FileContent
        {
            get => _fileContent;
            
        }

        public IEnumerable<MemoryStream> FileStreamsContent
        {
            get => _fileStreamsContent;

        }

        public async Task SetFileContentAsync(IEnumerable<MemoryStream> content)
        {

            _fileStreamsContent = content;
            if (ContentStreamsLoaded != null)
            {
                await ContentStreamsLoaded.Invoke(_fileStreamsContent, FileName);
            }

        }

        public async Task SetFileContentAsync(string content)
        {
        
            _fileContent = content;
            if (ContentLoaded != null)
            {
                await ContentLoaded.Invoke(_fileContent, FileName);
            }

        }

        public event Func<string, string, Task> ContentLoaded;
        public event Func<IEnumerable<MemoryStream>, string, Task> ContentStreamsLoaded;
    }
}
