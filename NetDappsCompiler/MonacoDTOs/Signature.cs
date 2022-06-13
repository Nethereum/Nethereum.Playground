using System.Collections.Generic;

namespace Nethereum.Playground.Components.Monaco.MonacoDTOs
{
    public class Signature
    {
        public string Name { get; set; }

        public string Label { get; set; }

        public string Documentation { get; set; }

        public List<SignatureParameter> Parameters { get; set; } = new List<SignatureParameter>();
    }
}