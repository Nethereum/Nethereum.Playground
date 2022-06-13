using System.Collections.Generic;

namespace Nethereum.Playground.Components.Monaco.MonacoDTOs
{
    public class SignatureCollection
    {
        public List<Signature> Signatures { get; set; } = new List<Signature>();
        
        public int ActiveSignature { get; set; }

        public int ActiveParameter { get; set; }
    }
}