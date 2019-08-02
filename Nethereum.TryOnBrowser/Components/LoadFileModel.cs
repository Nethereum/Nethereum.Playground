using System;
using System.Collections.Generic;

namespace Nethereum.TryOnBrowser.Components
{
    public class LoadFileModel
    {
        private string _fileContent;

        public string FileName { get; set; }
        public string AllowedExtension { get; set; }

        public string FileContent
        {
            get => _fileContent;
            set
            {
                _fileContent = value;
                ContentLoaded?.Invoke(_fileContent, FileName);
            }
        }

        public event Action<string, string> ContentLoaded;
    }
}
