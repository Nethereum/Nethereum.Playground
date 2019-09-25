using System;
using System.Threading.Tasks;

namespace Nethereum.Playground.Components
{
    public class AbiCodeGenerateModel
    {

        public void Clear()
        {
            ContractName = null;
            Abi = null;
            ByteCode = null;
        }

        public string ContractName { get; set; }
        public string Abi { get; set; }
        public string ByteCode { get; set; }

        public async Task CodeGenerateAsync()
        {
            if (CodeGenerate != null)
            {
                await CodeGenerate.Invoke(ContractName, Abi, ByteCode);
                Clear();
            }
        }

        public event Func<string, string, string, Task> CodeGenerate;
    }
}