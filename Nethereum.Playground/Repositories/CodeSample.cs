using System;

namespace Nethereum.Playground.Repositories
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

        public string GetFileName()
        {
            switch (Language)
            {
                case CodeLanguage.CSharp:
                    return Name + ".cs";
                case CodeLanguage.VbNet:
                    return Name + ".vb";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
    }
}

