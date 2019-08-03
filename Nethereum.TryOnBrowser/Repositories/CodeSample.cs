namespace Nethereum.TryOnBrowser.Repositories
{
    public class CodeSample
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Custom { get; set; } = false;
        public CodeLanguage Language { get; set; }

        public string DisplayTitle
        {
            get
            {
                if (Custom) return "My Sample:" + Name;
                return Name;
            }
        }
        
    }
}

