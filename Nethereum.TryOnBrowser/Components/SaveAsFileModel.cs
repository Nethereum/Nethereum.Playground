using System;
using System.Threading.Tasks;

namespace Nethereum.TryOnBrowser.Components
{
    public class SaveAsFileModel
    {
        public void Init(string originalName, string content)
        {
            OriginalName = originalName;
            Content = content;
            
        }

        public void Clear()
        {
            OriginalName = null;
            Content = null;
            NewName = null;
        }

        public string OriginalName { get; set; }
        public string NewName { get; set; }
        public string Content { get; set; }
        
        public async Task SaveFileAsAsync()
        {
            if (SaveFileAs != null)
            {
                await SaveFileAs.Invoke(Content, NewName);
                Clear();
            }
        }

        public event Func<string, string, Task> SaveFileAs;
    }
}