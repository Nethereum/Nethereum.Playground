using System;

namespace Nethereum.Playground.Repositories
{
    public class CodeSample
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
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

        public string GetLocalExtension()
        {
            switch (Language)
            {
                case CodeLanguage.CSharp:
                    return  ".txt";
                case CodeLanguage.VbNet:
                    return  ".txt";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GetLocalPath()
        {
            switch (Language)
            {
                case CodeLanguage.CSharp:
                    return $"/samples/csharp/{Id}{GetLocalExtension()}";
                case CodeLanguage.VbNet:
                    return $"/samples/vb/{Id}{GetLocalExtension()}";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}

