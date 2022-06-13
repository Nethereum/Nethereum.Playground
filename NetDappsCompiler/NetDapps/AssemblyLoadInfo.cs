using System.Collections.Generic;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

namespace NetDapps.Compiler.NetDapps
{

    public class UIComponent
    {
        [JsonProperty("entryPoint")]
        public string EntryPoint { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
    public class UIAssemblyLoadInfo : AssemblyLoadInfo
    {
        [JsonProperty("uiComponents")]
        public List<UIComponent> UIComponents { get; set; }
    }

    public partial class SmartContractComponentRegistry
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("chainId")]
        public long ChainId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public partial class Dependency
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("smartContractComponentRegistry")]
        public SmartContractComponentRegistry SmartContractComponentRegistry { get; set; }
    }

    public partial class Author
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("github")]
        public string Github { get; set; }

        [JsonProperty("proofOfHumanity")]
        public bool ProofOfHumanity { get; set; }
    }

    public partial class Drip
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("drippingTo")]
        public List<DrippingTo> DrippingTo { get; set; }
    }

    public partial class DrippingTo
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("percentage")]
        public long Percentage { get; set; }
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

        [JsonProperty("fullName")]
        public string FullName { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("dependencies")]
        public List<Dependency> Dependencies { get; set; }
        [JsonProperty("smartContractComponentRegistry")]
        public SmartContractComponentRegistry SmartContractComponentRegistry { get; set; }
        [JsonProperty("author")]
        public Author Author { get; set; }
        [JsonProperty("drip")]
        public Drip Drip { get; set; }

        [JsonIgnore]
        public MetadataReference MetadataReference { get; set; }

        [JsonIgnore]
        public Assembly Assembly { get; set; }

    }
}