using System.Reflection;
using Microsoft.CodeAnalysis;

namespace NetDapps.Assemblies
{
    public class UIAssemblyLoadInfo : AssemblyLoadInfo
    {
        public string[] UIComponents { get; set; }
    }
    public class AssemblyLoadInfo
    {
        public AssemblyLoadInfo()
        {
            
        }

        public AssemblyLoadInfo(string fullName)
        {
            FullName = fullName;
        }

        public AssemblyLoadInfo(string fullName, string url) : this(fullName)
        {
            Url = url;
        }

        public string FullName { get; set; }
        public string Url { get; set; }
        public MetadataReference MetadataReference { get; set;}
        public Assembly Assembly { get; set; }

    }
}