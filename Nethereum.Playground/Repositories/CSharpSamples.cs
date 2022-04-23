using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.Playground.Repositories
{
    public class CSharpSamples
    {
        public static List<CodeSample> GetSamples()
        {
            var samples = new List<CodeSample>
            {

                new CodeSample()
                {
                    Name = "Chain information: Query Ether account balance",
                    Id = "1001",
                    Code = @"
using System;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Threading.Tasks;
using Nethereum.Web3;

public class Program
{

    static async Task Main(string[] args)
    {

		// This sample shows how to connect to Ethereum mainnet using a public node like Infura
		// and check an account balance:

		// We first need to generate an instance of web3, using INFURA's mainnet url and 
		// our API key.
		// For this sample, we’ll use a special API key `ddd5ed15e8d443e295b696c0d07c8b02`,
		// but for your own project you’ll need your own key.
		var web3 = new Web3(""https://mainnet.infura.io/v3/ddd5ed15e8d443e295b696c0d07c8b02"");

		// Check the balance of one of the accounts provisioned in our chain, to do that, 
		// we can execute the GetBalance request asynchronously:
		var balance = await web3.Eth.GetBalance.SendRequestAsync(""0xde0b295669a9fd93d5f28d9ec85e40f4cb697bae"");
		Console.WriteLine(""Balance of Ethereum Foundation's account in Wei: "" + balance.Value);
		// Convert the balance into Ether from Wei (lowest unit)       
		Console.WriteLine(""Balance of Ethereum Foundation's account in Ether: "" + Web3.Convert.FromWei(balance.Value));
    }

}
                "
                },
                new CodeSample()
                {
                    Name = "Chain information: Get Block number, Block, Transaction and Receipt using Infura",
                    Id = "1002"
                },

                new CodeSample()
                {
                    Name = "Ether: Unit conversion between Ether and Wei",
                    Id = "1014"
                },

                new CodeSample()
                {
                    Name = "Ether: Transfer Ether to an account",
                    Id = "1003"
                },

                new CodeSample()
                {
                    Name = "Transactions / Signing: Raw transaction recreation and recovery of accounts",
                    Id = "1053"
                },

                new CodeSample()
                {
                    Name = "Transaction: How to send transactions using the transaction manager",
                    Id = "1059"
                },

                new CodeSample()
                {
                    Name = "Transactions: How to calculate the transaction hash ",
                    Id = "1058"
                },

                new CodeSample()
                {
                    Name = "Transactions: Transaction replacement",
                    Id = "1061"
                },

                new CodeSample()
                {
                    Name = "Transactions: Pending transactions from block and filters",
                    Id = "1062"
                },

                new CodeSample()
                {
                    Name = "Transactions: Decode Function from existing transaction input",
                    Id = "1063"
                },

                new CodeSample()
                {
                    Name = "Smart Contracts: Query ERC20 Smart contract balance using Preset",
                    Id = "1065",
                },

                new CodeSample()
                {
                    Name = "Smart Contracts: Query ERC20 Multiple Smart contract balances for owner, using multicall",
                    Id = "1066"
                },

                new CodeSample()
                {
                    Name = "Smart Contracts: Query ERC20 Smart contract balance",
                    Id = "1005",
                },

                new CodeSample()
                {
                    Name = "Smart Contracts: Query ERC721 Balance, token owner and all transfers using presets",
                    Id = "1067"
                },

                new CodeSample()
                {
                    Name = "Smart Contracts: Query ERC721 Smart contract balance and token owner",
                    Id = "1048"
                },

                new CodeSample()
                {
                    Name = "Smart Contracts: Query ERC721 Smart contract balance and token owner using human readable abi",
                    Id = "1068"
                },

                new CodeSample()
                {
                    Name = "Smart Contracts: Smart contract deployment",
                    Id = "1006",
                },

                new CodeSample()
                {
                    Name =
                        "Smart Contracts: Smart Contracts Deployment, Querying, Transactions, Nonces, Estimating Gas, Gas Price",
                    Id = "1007",
                },

                new CodeSample()
                {
                    Name =
                        "Smart Contracts: Retrieving Revert Messages",
                    Id = "1050"
                },

                new CodeSample()
                {
                    Name = "Smart Contracts: (Non type safe) Deployment, Calls(Querying), Transactions",
                    Id = "1045",
                },

                new CodeSample()
                {
                    Name = "Smart Contracts: (Non type safe / human readable abi) Deployment, Calls(Querying), Transactions",
                    Id = "1069",
                },
                new CodeSample()
                {
                    Name = "Smart Contracts: Events (End to End Introduction)",
                    Id = "1008",
                },

                new CodeSample()
                {
                    Name = "Smart Contracts: Events how to decode event from transaction receipt ",
                    Id = "1060"
                },

                 new CodeSample()
                 {
                     Name = "Smart Contracts: Events (Retrieving Events from Chain)",
                     Id = "1009",
                 },
                new CodeSample()
                {
                    Name = "Smart Contracts: Estimating Gas",
                    Id = "1010",
                },

                new CodeSample()
                {
                    Name = "Smart contracts: Signing offline Function / Contract Deployment messages",
                    Id = "1011",
                },

                new CodeSample()
                {
                    Name = "Smart contracts: Working with Structs",
                    Id = "1012",
                },

                new CodeSample()
                {
                    Name = "Smart contracts: Json input and output using complex structs",
                    Id = "1070",
                },

                new CodeSample()
                {
                    Name = "Smart contracts: Changing .Net Type on DTO's (e.g. byte[] to string)",
                    Id = "1052",
                },

                new CodeSample()
                {
                    Name = "ENS: Resolve address",
                    Id = "1055",
                },

                new CodeSample()
                {
                    Name = "ENS: Resolve url",
                    Id = "1056",
                },

                new CodeSample()
                {
                    Name = "ENS: Calculate rent price and commitment",
                    Id = "1054",
                },

                new CodeSample()
                {
                    Name = "ENS: Registration example",
                    Id = "1057",
                },

                new CodeSample()
                {
                    Name = "Signing: Sign a message and recover the signing address",
                    Id = "1013",
                },

                new CodeSample()
                {
                    Name = "ABI Encoding: Encoding using ABI Values, Parameters and Default values",
                    Id = "1015"
                },

                new CodeSample()
                {
                    Name = "ABI Encoding Packed: Encoding using ABI Values",
                    Id = "1016"
                },

                new CodeSample()
                {
                    Name = "ABI Encoding Packed: Encoding using parameters",
                    Id = "1017"
                },

                new CodeSample()
                {
                    Name = "ABI Encoding Packed: Encoding using default values",
                    Id = "1018",
                 },

                new CodeSample()
                {
                    Name = "Accounts: HD Wallets (Introduction)",
                    Id = "1043",
                },

                new CodeSample()
                {
                    Name = "Accounts: HD Wallets - Deriving accounts using indexes",
                    Id = "1041",
                },

                new CodeSample()
                {
                    Name = "Accounts: HD Wallets - Generating Mnemonics (seed words)",
                    Id = "1042",
                },

                new CodeSample()
                {
                    Name = "Accounts: How to use Chain Ids to prevent replay attacks",
                    Id = "1020",
                },

                new CodeSample()
                {
                    Name = "Key Store: Create Scrypt based KeyStore using custom params",
                    Id = "1021",
                },

                new CodeSample()
                {
                    Name = "Block Crawl Processing: Process block and cancel",
                    Id = "1022",
                },

                new CodeSample()
                {
                    Name = "Block Crawl Processing: Process blocks for a specific contract",
                    Id = "1023",
                },

                new CodeSample()
                {
                    Name = "Block Crawl Processing: Process blocks for a specific function",
                    Id = "1024",
                },

                new CodeSample()
                {
                    Name = "Block Crawl Processing: Custom Orchestrator",
                    Id = "1064",
                },

                new CodeSample()
                {
                    Name = "Block Crawl Processing: Full sample",
                    Id = "1025",
                },

                new CodeSample()
                {
                    Name = "Block Crawl Processing: With Block Progress Repository",
                    Id = "1026",
                },

                new CodeSample()
                {
                    Name = "Block Crawl Processing: Transaction criteria",
                    Id = "1027",
                },

                new CodeSample()
                {
                    Name = "Log Processing: Any contract any log",
                    Id = "1028",
                },

                new CodeSample()
                {
                    Name = "Log Processing: With Block Progress Repository",
                    Id = "1029",
                },


                new CodeSample()
                {
                    Name = "Log Processing: Any contract any log with criteria",
                    Id = "1030",
                },

                new CodeSample()
                {
                    Name = "Log Processing: Any contract many event async",
                    Id = "1031",
                },

                new CodeSample()
                {
                    Name = "Log Processing: One contract many event async",
                    Id = "1032",
                },

                new CodeSample()
                {
                    Name = "Log Processing: Any contract one event",
                    Id = "1033",
                },

                new CodeSample()
                {
                    Name = "Log Processing: Many contracts one event",
                    Id = "1034",
                },

                new CodeSample()
                {
                    Name = "Log Processing: One contract any log",
                    Id = "1035",
                },

                new CodeSample()
                {
                    Name = "Log Processing: One contract one event",
                    Id = "1036",
                },

                new CodeSample()
                {
                    Name = "Log Processing: One contract one event async",
                    Id = "1037",
                },

                new CodeSample()
                {
                    Name = "Log Processing: One contract one event with criteria",
                    Id = "1038",
                },

                new CodeSample()
                {
                    Name = "Log Processing: With In-Depth Configuration",
                    Id = "1040",
                },

                new CodeSample()
                {
                    Name = "Utilities: Address Utilities",
                    Id = "1039",
                },

                new CodeSample()
                {
                    Name = "Utilities: Common Type Conversion",
                    Id = "1051",
                },

                //new CodeSample()
                //{
                //    Name = "Wonka Rule Engine Preview: Loading external rules and pre validation",
                //    Id = "1044",
                //},

                new CodeSample()
                {
                    Name = "Azure Blockchain Service: Interacting with a Quorum member node",
                    Id = "1046"
                },

                new CodeSample()
                {
                    Name = "Azure Blockchain Service: Interacting in private with a Quorum member",
                    Id = "1047"
                },

               

               

               
            };

            foreach (var sample in samples)
            {
                sample.Language = CodeLanguage.CSharp;
            }

            return samples;
        }
    }
}
