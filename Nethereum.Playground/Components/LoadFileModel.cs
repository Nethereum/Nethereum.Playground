using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nethereum.Playground.Components
{
    public class LoadFileModel
    {
        private string _fileContent;

        public string FileName { get; set; }
        public string AllowedExtension { get; set; }

        public string FileContent
        {
            get => _fileContent;
            
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
    }
}
