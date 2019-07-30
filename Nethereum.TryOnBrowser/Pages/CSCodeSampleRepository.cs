using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Nethereum.TryOnBrowser.Pages
{

    public class GithubContent
    {
        public string Name { get; set; }
        public string Path { get; set; }

    }
    public class CodeSampleRepository
    {
        private HttpClient _httpClient;
        private const string _githubPath = "nethereum/nethereum/nethereum.playground";

        private const string _githubContentUrl =
            "https://api.github.com/repos/Nethereum/Nethereum.Playground/contents/samples/csharp";

        private const string _githubFileUrl =
            "https://raw.githubusercontent.com/Nethereum/Nethereum.Playground/master/samples/csharp/";

        private List<CodeSample> _codeSamples;

        public CodeSampleRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _codeSamples = new List<CodeSample>();
        }

        public async Task<CodeSample[]> GetCodeSamples()
        {
            //var contents = await _httpClient.GetJsonAsync<GithubContent[]>(_githubContentUrl);
            //Console.WriteLine(contents.Length);
            //Console.WriteLine(contents[0].Name);

            
            //foreach (var content in contents)
            //{
            //    //var code = await _httpClient.GetStringAsync(_githubFileUrl + content.Name);
                    
            //    _codeSamples.Add(new CodeSample()
            //    {
            //        Name = content.Name,
            //       // Code = code   
            //    });
            //}

            //return _codeSamples.ToArray();
            return new CodeSample[]
            {

				new CodeSample()
                {
                    Name = "Ether: Query account balance using INFURA",
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

		// This sample shows how to connect to Ethereum mainnet using Infura
		// and check an account balance:

		// We first need to generate an instance of web3, using INFURA's mainnet url and 
		// our API key.
		// For this sample, we’ll use a special API key `7238211010344719ad14a89db874158c`,
		// but for your own project you’ll need your own key.
		var web3 = new Web3(""https://mainnet.infura.io/v3/7238211010344719ad14a89db874158c"");

		// Check the balance of one of the accounts provisioned in our chain, to do that, 
		// we can execute the GetBalance request asynchronously:
		var balance = await web3.Eth.GetBalance.SendRequestAsync(""0xde0b295669a9fd93d5f28d9ec85e40f4cb697bae"");
		Console.WriteLine(""Balance of Ethereum Foundation's account: "" + balance.Value);

    }

}
                "
                },
				
				
new CodeSample()
                {
                    Name = "Ether: Transfer Ether to an account",
                    Code = @"				
				
using System;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

public class Program
{

    static async Task Main(string[] args)
    {
			//First let's create an account with our private key for the account address 
			var privateKey = ""0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7"";
			var account = new Account(privateKey);
			Console.WriteLine(""Our account: "" + account.Address);
			//Now let's create an instance of Web3 using our account pointing to our nethereum testchain
			var web3 = new Web3(account, ""http://testchain.nethereum.com:8545"");

			// Check the balance of the account we are going to send the Ether
			var balance = await web3.Eth.GetBalance.SendRequestAsync(""0x13f022d72158410433cbd66f5dd8bf6d2d129924"");
			Console.WriteLine(""Receiver account balance before sending Ether: "" + balance.Value + "" Wei"");
			Console.WriteLine(""Receiver account balance before sending Ether: "" + Web3.Convert.FromWei(balance.Value) + "" Ether"");

			// Lets transfer 1.11 Ether
			var transaction = await web3.Eth.GetEtherTransferService()
                .TransferEtherAndWaitForReceiptAsync(""0x13f022d72158410433cbd66f5dd8bf6d2d129924"", 1.11m);

			balance = await web3.Eth.GetBalance.SendRequestAsync(""0x13f022d72158410433cbd66f5dd8bf6d2d129924"");
			Console.WriteLine(""Receiver account balance after sending Ether: "" + balance.Value);
			Console.WriteLine(""Receiver account balance after sending Ether: "" + Web3.Convert.FromWei(balance.Value) + "" Ether"");

    }

}	
"
                },			
				
                new CodeSample()
                {
                    Name="Smart Contracts: Query ERC20 Smart contract balance",
                    Code= @"
using System;
using System.Numerics;
using System.Threading.Tasks;	
using Nethereum.Web3;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;


public class Program
{
    // The balance function message definition    
    [Function(""balanceOf"", ""uint256"")]
    public class BalanceOfFunction : FunctionMessage
    {
        [Parameter(""address"", ""_owner"", 1)]
        public string Owner { get; set; }
    }

    //async Task Main to enable async methods

    static async Task Main(string[] args)
    {
        //Connecting to Ethereum mainnet using Infura
        var web3 = new Web3(""https://mainnet.infura.io/v3/7238211010344719ad14a89db874158c"");
        
        //Setting the owner https://etherscan.io/tokenholdings?a=0x8ee7d9235e01e6b42345120b5d270bdb763624c7
        var balanceOfMessage = new BalanceOfFunction() { Owner = ""0x8ee7d9235e01e6b42345120b5d270bdb763624c7"" };
        
        //Creating a new query handler
        var queryHandler = web3.Eth.GetContractQueryHandler<BalanceOfFunction>();
        
        //Querying the Maker smart contract https://etherscan.io/address/0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2
        var balance = await queryHandler.QueryAsync<BigInteger>(""0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2"", balanceOfMessage).ConfigureAwait(false);
        Console.WriteLine(balance.ToString());
    }

}

"
                },
				

                new CodeSample()
                {
                    Name = "Signing: Sign a message and recover the signing address",
                    Code = @"
using System;
using System.Text;
using System.Collections.Generic;
using Nethereum.Util;
using Nethereum.Signer;
using Nethereum.Hex.HexConvertors.Extensions;

public class Program
{

    public static void Main()
    {

        var address = ""0x12890d2cce102216644c59dae5baed380d84830c"";
        Console.WriteLine(address);
        var msg1 = ""wee test message 18/09/2017 02:55PM"";
        var privateKey = ""0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7"";
        var signer1 = new EthereumMessageSigner();
        var signature1 = signer1.EncodeUTF8AndSign(msg1, new EthECKey(privateKey));
        Console.WriteLine(signature1);
        var addressRec1 = signer1.EncodeUTF8AndEcRecover(msg1, signature1);
        Console.WriteLine(addressRec1);
    }

}

"
                },


				new CodeSample()
                {
                    Name = "Ether: Unit conversion between Ether and Wei",
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
		// This sample shows how to convert units of Ether.

		// We first need to generate an instance of web3:
		var web3 = new Web3(""http://testchain.nethereum.com:8545"");

		// Let's now check the balance of the Ethereum Foundation (just because we can).
		var balance = await web3.Eth.GetBalance.SendRequestAsync(""0xde0b295669a9fd93d5f28d9ec85e40f4cb697bae"");
		Console.WriteLine(""Balance of Ethereum Foundation's account: "" + balance.Value);

		// By default, the returned value is in Wei (the lowest unit of value), 
		// not necessarily easy to read unless you’re really talented at Maths.
		// To make the return value more human friendly, we can convert the balance 
		// to Ether using the conversion utility’s ""**FromWei**"" method:
		var balanceInEther = Web3.Convert.FromWei(balance.Value);
		Console.WriteLine(""Balance of Ethereum Foundation's account in Ether: "" + balanceInEther);

		// We can even “counter convert” the balance back to wei using the “**ToWei**” 
		// method (this has no other purpose than demonstrating the method, of course):
		var backToWei = Web3.Convert.ToWei(balanceInEther);
		Console.WriteLine(""Balance of Ethereum Foundation's account back in Wei: "" + backToWei);
	}

}
                "
                },
				
				new CodeSample()
                {
                    Name = "ABI Encoding Packed: Encoding using ABI Values",
                    Code = @"
using System;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Util;
using Nethereum.ABI;

public class AbiEncodePacked_UsingABIValue
{
    static void Main(string[] args)
    {
					 var abiEncode = new ABIEncode();
            
            var result = abiEncode.GetSha3ABIEncodedPacked(new ABIValue(""address"", ""0x407D73d8a49eeb85D32Cf465507dd71d507100c1""))
                    .ToHex();

						Console.WriteLine(""Encoded address: "" + result);

            result = abiEncode.GetSha3ABIEncodedPacked(new ABIValue(""bytes"",
                    ""0x407D73d8a49eeb85D32Cf465507dd71d507100c1"".HexToByteArray())).ToHex();
						
						Console.WriteLine(""Encoded bytes: "" + result);
            //bytes32 it is a 32 bytes array so it will be padded with 00 values
            result = 
                abiEncode.GetSha3ABIEncodedPacked(new ABIValue(""bytes32"",
                    ""0x407D73d8a49eeb85D32Cf465507dd71d507100c1"".HexToByteArray())).ToHex();
						
						Console.WriteLine(""Encoded bytes32: "" + result);
							

            //web3.utils.soliditySha3({t: 'string', v: 'Hello!%'}, {t: 'int8', v:-23}, {t: 'address', v: '0x85F43D8a49eeB85d32Cf465507DD71d507100C1d'});
            result =
                abiEncode.GetSha3ABIEncodedPacked(
                    new ABIValue(""string"", ""Hello!%""), new ABIValue(""int8"", -23),
                    new ABIValue(""address"", ""0x85F43D8a49eeB85d32Cf465507DD71d507100C1d"")).ToHex();
						Console.WriteLine(""Encoded Hello!%, -23 and address 0x85F43D8a49eeB85d32Cf465507DD71d507100C1d: "" + result);
            
    }
}
" },
				
                new CodeSample()
                {
                    Name = "ABI Encoding Packed: Encoding using parameters",
                    Code = @"

using System;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Util;
using Nethereum.ABI;

public class AbiEncodePacked_UsingParams
{

		public class TestParamsInput
		{
				[Parameter(""string"", 1)] public string First { get; set; }
				[Parameter(""int8"", 2)] public int Second { get; set; }
				[Parameter(""address"", 3)] public string Third { get; set; }
		}

    static void Main(string[] args)
    {
				var abiEncode = new ABIEncode();
				var result = abiEncode.GetSha3ABIParamsEncodedPacked(new TestParamsInput()
						{First = ""Hello!%"", Second = -23, Third = ""0x85F43D8a49eeB85d32Cf465507DD71d507100C1d""});
				Console.WriteLine(""Result: "" + result.ToHex(true));

    }
}


"
                },
				
				
                new CodeSample()
                {
                    Name = "ABI Encoding Packed: Encoding using default values",
                    Code = @"

using System;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Util;
using Nethereum.ABI;

public class AbiEncodePacked_UsingDefaultValues
{
    static void Main(string[] args)
    {
					var abiEncode = new ABIEncode();
            var result = abiEncode.GetSha3ABIEncodedPacked(234564535,
                ""0xfff23243"".HexToByteArray(), true, -10);
						Console.WriteLine(""Encoded 234564535, 0xfff23243, true and -10:"" + result.ToHex());

            var result2 = abiEncode.GetSha3ABIEncodedPacked(""Hello!%"");
						Console.WriteLine(""Encoded Hello!%:"" + result2.ToHex());
          
            var result3 = abiEncode.GetSha3ABIEncodedPacked(234);
            Console.WriteLine(""Encoded 234:"" + result2.ToHex());
    }
}

"
                },
				
				
				
				                new CodeSample()
                {
                    Name = "Accounts: HD Wallets",
                    Code = @"
using System;
using System.Numerics;
using Nethereum.Web3; 
using Nethereum.Web3.Accounts; 
using Nethereum.Util; 
using System.Threading.Tasks;
using NBitcoin;
using Nethereum.Hex.HexConvertors.Extensions; 
using Nethereum.HdWallet;

public class Program
{

    static async Task Main(string[] args)
    {

		// This samples shows how to create a Hd Wallet and recover an address
		// From it.

		// Initiating a HD Wallet requires a list of words and a password as arguments, 
		// in this first case, we will use an arbitrary sequence of words.

		string Words = ""ripple scissors kick mammal hire column oak again sun offer wealth tomorrow wagon turn fatal"";
		string Password1 = ""password"";
		var wallet1 = new Wallet(Words, Password1);
		for (int i = 0; i < 10; i++)
		{
			var account = wallet1.GetAccount(i); 
			Console.WriteLine(""Account index : ""+ i +"" - Address : ""+ account.Address +"" - Private key : ""+ account.PrivateKey);
		}

		// An HD Wallet is deterministic, it will derive the same unlimited number of addresses 
		// given the same seed (password+wordlist).
		// All the created accounts can be loaded in a Web3 instance and used as any other account, 
		// we can for instance check the balance of one of them:

		var account1 = new Wallet(Words, Password1).GetAccount(0);
		Console.WriteLine(""account1 address is: ""+account1.Address);
		var web3 = new Web3(account1,""http://testchain.nethereum.com:8545"");
		var balance = await web3.Eth.GetBalance.SendRequestAsync(account1.Address);
		Console.WriteLine(""account1 balance is: ""+balance.Value);
		// Transfering ether using an HD Wallet

		// The process of transferring Ether using a HD Wallet is the same as 
		// using a standalone account

		var toAddress = ""0x13f022d72158410433cbd66f5dd8bf6d2d129924"";
		var transaction = await web3.Eth.GetEtherTransferService().TransferEtherAndWaitForReceiptAsync(toAddress, 2.11m, 2);
		// Generating mnemonics

		// Our friends at NBitcoin offer a very convenient way to generate a backup seed sentence:

		Mnemonic mnemo = new Mnemonic(Wordlist.English, WordCount.Twelve);

		string Password2 = ""password2"";
		var wallet2 = new Wallet(mnemo.ToString(), Password2);
		var account2 = wallet2.GetAccount(0);

		// Retrieving the account using the mnemonic backup seed words

		// A backup seed sentence is a human friendly way to recover all the generated addresses, 
		// since Hd Wallets generate addresses deterministically, we can now regenerate them at 
		// anytime using our seed sentence and retrieve them using an index number.

		// In the below example, we will use our backup seed words to retrieve an account. 
		// The first step will be to declare our word list:

		var backupSeed = mnemo.ToString();

		// Now using the backup seed, the password and the address index (\`0\`) 
		// we can create the HD wallet and retrieve our account. The account contains 
		// the private key to sign our transactions. We can generate and re-generate addresses 
		// based on an index number from the same seed.

		var wallet3 = new Wallet(backupSeed, Password2);
		var recoveredAccount = wallet3.GetAccount(0);
		Console.WriteLine(""recoveredAccount address is: ""+recoveredAccount.Address);
    }

}
                "
                },

                new CodeSample()
                {
                    Name = "Accounts: Chain-IDs, accounts and web3",
                    Code = @"
using System;
using System.Numerics;
using Nethereum.Web3; 
using Nethereum.Signer; 
using Nethereum.Web3.Accounts; 
using Nethereum.Util; 
using Nethereum.Hex.HexConvertors.Extensions; 
using Nethereum.RPC.Eth.DTOs; 
using Nethereum.Hex.HexTypes;
using System.Threading.Tasks;

public class Program
{

    static async Task Main(string[] args)
    {

		// Chain ID management for replay attack protection

		// This sample explains what a replay attack is and how Nethereum allows you to protect your code against them.

		// Replay Attack: Ethereum makes it possible to send the same transaction across
		// different chains, hence the term ""replay attack"". For instance, it is possible
		// to issue a fund transfer on a testchain and then perform the same transfer over
		// the MainNet (with real funds). This vulnerability is due to the fact that the
		// same accounts can exist in any Ethereum chain, protected by the same privateKey.

		// To counteract this issue, an Ethereum fix was implemented (the improvement name 
		//is [EIP155](https://github.com/Nethereum/Nethereum.Workbooks/issues/10)) allowing 
		// the insertion of the ChainID data in signed transactions. Thanks to this 
		// improvement it is now possible to force a transaction to only run on a specific 
		//chain by including its ID when signed.

		//   Quick playground setup

		// First, let's download the test chain matching your environment from
		// <https://github.com/Nethereum/Testchains>

		// Start a Geth chain (geth-clique-linux/, geth-clique-windows/ or geth-clique-mac/) 
		// using **_startgeth.bat_** (Windows) or **_startgeth.sh_** (Mac/Linux). 
		// The chain is setup with the Proof of Authority consensus and will start 
		// the mining process immediately.

		// The preconfigured chainIds can be found in Nethereum.Signer.Chain:

		//     public enum Chain
		//     {
		//         MainNet = 1,
		//         Morden = 2,
		//         Ropsten = 3,
		//         Rinkeby = 4,
		//         RootstockMainNet = 30,
		//         RootstockTestNet = 31,
		//         Kovan = 42,
		//         ClassicMainNet = 61,
		//         ClassicTestNet = 62,
		//         Private = 1337
		//     }
		// }

		// To configure the chainId in geth, edit the genesis as follows (example configuration):

		//  ""config"": {
		//    ""chainID"": 444444444500,
		//    ""homesteadBlock"": 0,
		//    ""eip150Block"": 0,
		//    ""eip150Hash"": ""0x0000000000000000000000000000000000000000000000000000000000000000"",
		//    ""eip155Block"": 0,
		//    ""eip158Block"": 0,
		//	""byzantiumBlock"": 0,
		//	""constantinopleBlock"": 0,
		//	""petersburgBlock"": 0,
		//    ""daoForkSupport"": true,
		//    ""clique"": {
		//      ""period"": 1,
		//      ""epoch"": 30000
		//    }

		// To sign a transaction using the ChainID attribute, we need to create an instance 
		//of the ""Account"" object using our private key and ChainID as arguments.

		// First, we need to declare our private key:

		var privatekey = ""0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7"";

		// Then we can create an Account instance as follows, using the chainId 
		// from the MainNet:

		var account = new Account(privatekey, Chain.MainNet);

		// or just using our custom chainId as such:

		account =  new Account(privatekey, 444444444500);

		// For this sample we will use our custom chainId already set in our testnet 444444444500.

		// We now can create a new instance of Web3 using the account configured with the 
		//chainId. Internally the TransactionManager will use this chainId to sign all 
		// transactions.

		var web3 = new Web3(account,""http://testchain.nethereum.com:8545"");

		// Let's use it in a simple example, for example the transfer of Ether. 

		var toAddress = ""0x13f022d72158410433cbd66f5dd8bf6d2d129924"";

		// First let's convert 1 Ether to Wei.

		var wei = Web3.Convert.ToWei(1);
		 Console.WriteLine(""1 Ether converted in Wei = "" + wei);

		// And then use the TransactionManager to make the transfer and wait for the receipt.

		 var transactionReceipt = await web3.TransactionManager.TransactionReceiptService.SendRequestAndWaitForReceiptAsync(
					   new TransactionInput() {From = account.Address, To = toAddress, Value = new HexBigInteger(wei)}, null);
		Console.WriteLine(""Transaction Hash = "" + transactionReceipt.TransactionHash);
		// Finally, we can see that the receivers address balance has incresed by 1 Ether

		 var balance = await web3.Eth.GetBalance.SendRequestAsync(""0x13f022d72158410433cbd66f5dd8bf6d2d129924"");
		 var amountInEther = Web3.Convert.FromWei(balance.Value);
		 Console.WriteLine(""Balance of recipient account  = ""+ amountInEther);

	}

}
                "
                },
				
							
                new CodeSample()
                {
                    Name = "Block Crawl Processing: Process block and cancel",
                    Code = @"
using Nethereum.BlockchainProcessing.Processor;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class BlockProcessing_Cancellation
{

    /// <summary>
    /// Demonstrates how to cancel processing
    /// </summary>
    public static async Task Main(string[] args)
    {
        var blocks = new List<BlockWithTransactions>();

        var web3 = new Web3(""https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c"");

        //if we need to stop the processor mid execution - call cancel on the token
        var cancellationTokenSource = new CancellationTokenSource();

        //create our processor
        var processor = web3.Processing.Blocks.CreateBlockProcessor(steps =>
        {
            // inject handler
            // cancel after the first block is processsed
            steps.BlockStep.AddSynchronousProcessorHandler(b => 
            { 
                blocks.Add(b); 
                cancellationTokenSource.Cancel(); 
            });
        });

        //crawl the required block range
        await processor.ExecuteAsync(
            cancellationToken: cancellationTokenSource.Token, 2830144);

        Console.WriteLine($""Expected 1 block, actual block count: {blocks.Count}"");
    }

}
                "
                },
				
 new CodeSample()
                {
                    Name = "Block Crawl Processing: Process blocks for a specific contract",
                    Code = @"
using Nethereum.BlockchainProcessing.Processor;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

public class BlockProcessing_ForASpecificContract
{
    /// <summary>
    /// Involving a specific contract
    /// </summary>
    /// <returns></returns>
    public static async Task Main()
    {
        var transactions = new List<TransactionReceiptVO>();
        var filterLogs = new List<FilterLogVO>();

        var web3 = new Web3(""https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c"");

        const string ContractAddress = ""0x5534c67e69321278f5258f5bebd5a2078093ec19"";

        //create our processor
        var processor = web3.Processing.Blocks.CreateBlockProcessor(steps => {
            //for performance we add criteria before we have the receipt to prevent unecessary data retrieval
            //we only want to retrieve receipts if the tx was sent to the contract
            steps.TransactionStep.SetMatchCriteria(t => t.Transaction.IsTo(ContractAddress));
            steps.TransactionReceiptStep.AddSynchronousProcessorHandler(tx => transactions.Add(tx));
            steps.FilterLogStep.AddSynchronousProcessorHandler(l => filterLogs.Add(l));
        });

        //if we need to stop the processor mid execution - call cancel on the token
        var cancellationToken = new CancellationToken();
        //crawl the blocks
        await processor.ExecuteAsync(
            toBlockNumber: new BigInteger(2830145),
            cancellationToken: cancellationToken,
            startAtBlockNumberIfNotProcessed: new BigInteger(2830144));

        Console.WriteLine($""Transactions. Expected: 2, Actual: {transactions.Count}"");
        Console.WriteLine($""Logs. Expected: 8, Actual: {filterLogs.Count}"");
    }

}
                "
                },

 new CodeSample()
                {
                    Name = "Block Crawl Processing: Process blocks for a specific function",
                    Code = @"
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.BlockchainProcessing.Processor;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

public class BlockProcessing_ForASpecificFunction
{

    [Function(""buyApprenticeChest"")]
    public class BuyApprenticeFunction : FunctionMessage
    {
        [Parameter(""uint256"", ""_region"", 1)]
        public BigInteger Region { get; set; }
    }

    public static async Task Main(string[] args)
    {
        var transactions = new List<TransactionReceiptVO>();
        var filterLogs = new List<FilterLogVO>();

        var web3 = new Web3(""https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c"");

        //create our processor
        var processor = web3.Processing.Blocks.CreateBlockProcessor(steps => {
                
            //match the to address and function signature
            steps.TransactionStep.SetMatchCriteria(t => 
                t.Transaction.IsTo(""0xc03cdd393c89d169bd4877d58f0554f320f21037"") && 
                t.Transaction.IsTransactionForFunctionMessage<BuyApprenticeFunction>());

            steps.TransactionReceiptStep.AddSynchronousProcessorHandler(tx => transactions.Add(tx));
            steps.FilterLogStep.AddSynchronousProcessorHandler(l => filterLogs.Add(l));
        });

        //if we need to stop the processor mid execution - call cancel on the token
        var cancellationToken = new CancellationToken();
        //crawl the blocks
        await processor.ExecuteAsync(
            toBlockNumber: new BigInteger(3146684),
            cancellationToken: cancellationToken,
            startAtBlockNumberIfNotProcessed: new BigInteger(3146684));

        Console.WriteLine($""Transactions. Expected: 1, Actual: {transactions.Count}"");
        Console.WriteLine($""Logs. Expected: 1, Actual: {filterLogs.Count}"");
    }
}"
                },	

 new CodeSample()
                {
                    Name = "Block Crawl Processing: Full sample",
                    Code = @"
using Nethereum.BlockchainProcessing.Processor;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

public class BlockProcessing_StartHere
{
    /// <summary>
    /// Crawl the chain for a block range and injest the data
    /// </summary>
    public static async Task Main(string[] args)
    {
        var blocks = new List<BlockWithTransactions>();
        var transactions = new List<TransactionReceiptVO>();
        var contractCreations = new List<ContractCreationVO>();
        var filterLogs = new List<FilterLogVO>();

        var web3 = new Web3(""https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c"");

        //create our processor
        var processor = web3.Processing.Blocks.CreateBlockProcessor(steps =>
        {
            // inject handler for each step
            steps.BlockStep.AddSynchronousProcessorHandler(b => blocks.Add(b));
            steps.TransactionReceiptStep.AddSynchronousProcessorHandler(tx => transactions.Add(tx));
            steps.ContractCreationStep.AddSynchronousProcessorHandler(cc => contractCreations.Add(cc));
            steps.FilterLogStep.AddSynchronousProcessorHandler(l => filterLogs.Add(l));
        });

        //if we need to stop the processor mid execution - call cancel on the token
        var cancellationToken = new CancellationToken();

        //crawl the required block range
        await processor.ExecuteAsync(
            toBlockNumber: new BigInteger(2830145),
            cancellationToken: cancellationToken,
            startAtBlockNumberIfNotProcessed: new BigInteger(2830144));

        Console.WriteLine($""Blocks.  Expected: 2, Found: {blocks.Count}"");
        Console.WriteLine($""Transactions.  Expected: 25, Found: {transactions.Count}"");
        Console.WriteLine($""Contract Creations.  Expected: 5, Found: {contractCreations.Count}"");

        Log(transactions, contractCreations, filterLogs);
    }

    private static void Log(
        List<TransactionReceiptVO> transactions, 
        List<ContractCreationVO> contractCreations, 
        List<FilterLogVO> filterLogs)
    {
        Console.WriteLine(""Sent From"");
        foreach (var fromAddressGrouping in transactions.GroupBy(t => t.Transaction.From).OrderByDescending(g => g.Count()))
        {
            var logs = filterLogs.Where(l => fromAddressGrouping.Any((a) => l.Transaction.TransactionHash == a.TransactionHash));

            Console.WriteLine($""From: {fromAddressGrouping.Key}, Tx Count: {fromAddressGrouping.Count()}, Logs: {logs.Count()}"");
        }

        Console.WriteLine(""Sent To"");
        foreach (var toAddress in transactions
            .Where(t => !t.Transaction.IsToAnEmptyAddress())
            .GroupBy(t => t.Transaction.To)
            .OrderByDescending(g => g.Count()))
        {
            var logs = filterLogs.Where(l => toAddress.Any((a) => l.Transaction.TransactionHash == a.TransactionHash));

            Console.WriteLine($""To: {toAddress.Key}, Tx Count: {toAddress.Count()}, Logs: {logs.Count()}"");
        }

        Console.WriteLine(""Contracts Created"");
        foreach (var contractCreated in contractCreations)
        {
            var tx = transactions.Count(t => t.Transaction.IsTo(contractCreated.ContractAddress));
            var logs = filterLogs.Count(l => transactions.Any(t => l.Transaction.TransactionHash == t.TransactionHash));

            Console.WriteLine($""From: {contractCreated.ContractAddress}, Tx Count: {tx}, Logs: {logs}"");
        }
    }
}"
                },

 new CodeSample()
                {
                    Name = "Block Crawl Processing: Transaction criteria",
                    Code = @"
using Nethereum.BlockchainProcessing.Processor;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

public class BlockProcessing_WithTransactionCriteria
{
    /// <summary>
    /// Process transactions matching specific criteria
    /// </summary>
    /// <returns></returns>
    public static async Task Main()
    {
        var transactions = new List<TransactionReceiptVO>();
        var filterLogs = new List<FilterLogVO>();

        var web3 = new Web3(""https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c"");

        //create our processor
        var processor = web3.Processing.Blocks.CreateBlockProcessor(steps => {
            steps.TransactionStep.SetMatchCriteria(t => t.Transaction.IsFrom(""0x1cbff6551b8713296b0604705b1a3b76d238ae14""));
            steps.TransactionReceiptStep.AddSynchronousProcessorHandler(tx => transactions.Add(tx));
            steps.FilterLogStep.AddSynchronousProcessorHandler(l => filterLogs.Add(l));
        });

        //if we need to stop the processor mid execution - call cancel on the token
        var cancellationToken = new CancellationToken();
        //crawl the blocks
        await processor.ExecuteAsync(
            toBlockNumber: new BigInteger(2830145),
            cancellationToken: cancellationToken,
            startAtBlockNumberIfNotProcessed: new BigInteger(2830144));

        Console.WriteLine($""Transactions. Expected: 2, Actual: {transactions.Count}"");
        Console.WriteLine($""Logs. Expected: 4, Actual: {filterLogs.Count}"");
    }

}"
                },

 new CodeSample()
                {
                    Name = "Log Processing: Any contract any log",
                    Code = @"
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

public class LogProcessing_AnyContractAnyLog
{
    public static async Task Main()
    {
        var logs = new List<FilterLog>();

        var web3 = new Web3(""https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c"");

        //create our processor to retrieve transfers
        var processor = web3.Processing.Logs.CreateProcessor(log => logs.Add(log));

        //if we need to stop the processor mid execution - call cancel on the token
        var cancellationToken = new CancellationToken();

        //crawl the required block range
        await processor.ExecuteAsync(
            toBlockNumber: new BigInteger(3146690),
            cancellationToken: cancellationToken,
            startAtBlockNumberIfNotProcessed: new BigInteger(3146684));

        Console.WriteLine($""Expected 65 logs. Logs found: {logs.Count}."");
    }

}"

},


 new CodeSample()
                {
                    Name = "Log Processing: Any contract any log with criteria",
                    Code = @"
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.BlockchainProcessing.Processor;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

public class LogProcessing_AnyContractAnyLogWithCriteria
{
    public static async Task Main()
    {
        var logs = new List<FilterLog>();

        var web3 = new Web3(""https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c"");

        var processor = web3.Processing.Logs.CreateProcessor(
            action: log => logs.Add(log), 
            criteria: log => log.Removed == false);

        //if we need to stop the processor mid execution - call cancel on the token
        var cancellationToken = new CancellationToken();

        //crawl the required block range
        await processor.ExecuteAsync(
            toBlockNumber: new BigInteger(3146690),
            cancellationToken: cancellationToken,
            startAtBlockNumberIfNotProcessed: new BigInteger(3146684));

        Console.WriteLine($""Expected 65 logs. Logs found: {logs.Count}."");
    }

}"

},

 new CodeSample()
                {
                    Name = "Log Processing: Any contract many event async",
                    Code = @"
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.BlockchainProcessing.Processor;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;		

public class LogProcessing_AnyContractManyEventAsync
{
    [Event(""Transfer"")]
    public class TransferEvent: IEventDTO
    {
        [Parameter(""address"", ""_from"", 1, true)]
        public string From { get; set; }

        [Parameter(""address"", ""_to"", 2, true)]
        public string To { get; set; }

        [Parameter(""uint256"", ""_value"", 3, false)]
        public BigInteger Value { get; set; }
    }

    [Event(""Transfer"")]
    public class Erc721TransferEvent
    {
        [Parameter(""address"", ""_from"", 1, true)]
        public string From { get; set; }

        [Parameter(""address"", ""_to"", 2, true)]
        public string To { get; set; }

        [Parameter(""uint256"", ""_value"", 3, true)]
        public BigInteger Value { get; set; }
    }
        
    public static async Task Main(string[] args)
    {
        var erc20transferEventLogs = new List<EventLog<TransferEvent>>();
        var erc721TransferEventLogs = new List<EventLog<Erc721TransferEvent>>();

        var web3 = new Web3(""https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c"");

        var erc20TransferHandler = new EventLogProcessorHandler<TransferEvent>(
            eventLog => erc20transferEventLogs.Add(eventLog));

        var erc721TransferHandler = new EventLogProcessorHandler<Erc721TransferEvent>(
            eventLog => erc721TransferEventLogs.Add(eventLog)); 

        var processingHandlers = new ProcessorHandler<FilterLog>[] {
            erc20TransferHandler, erc721TransferHandler};

        //create our processor to retrieve transfers
        //restrict the processor to Transfers for a specific contract address
        var processor = web3.Processing.Logs.CreateProcessor(processingHandlers);

        //if we need to stop the processor mid execution - call cancel on the token
        var cancellationToken = new CancellationToken();

        //crawl the required block range
        await processor.ExecuteAsync(
            toBlockNumber: new BigInteger(3146690),
            cancellationToken: cancellationToken,
            startAtBlockNumberIfNotProcessed: new BigInteger(3146684));

        Console.WriteLine($""Expected 13 ERC20 transfers. Logs found: {erc20transferEventLogs.Count}."");
        Console.WriteLine($""Expected 3 ERC721 transfers. Logs found: {erc721TransferEventLogs.Count}."");
    }
}"

				},

new CodeSample()
                {
                    Name = "Log Processing: One contract many event async",
                    Code = @"

using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.BlockchainProcessing.Processor;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;				
					
public class LogProcessing_OneContractManyEventsAsync
{
    [Event(""Transfer"")]
    public class TransferEvent: IEventDTO
    {
        [Parameter(""address"", ""_from"", 1, true)]
        public string From { get; set; }

        [Parameter(""address"", ""_to"", 2, true)]
        public string To { get; set; }

        [Parameter(""uint256"", ""_value"", 3, false)]
        public BigInteger Value { get; set; }
    }

    [Event(""Approval"")]
    public class ApprovalEventDTO : IEventDTO
    {
        [Parameter(""address"", ""_owner"", 1, true)]
        public virtual string Owner { get; set; }
        [Parameter(""address"", ""_spender"", 2, true)]
        public virtual string Spender { get; set; }
        [Parameter(""uint256"", ""_value"", 3, false)]
        public virtual BigInteger Value { get; set; }
    }

    public static async Task Main(string[] args)
    {
        var erc20transferEventLogs = new List<EventLog<TransferEvent>>();
        var approvalEventLogs = new List<EventLog<ApprovalEventDTO>>();

        var web3 = new Web3(""https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c"");

        var erc20TransferHandler = new EventLogProcessorHandler<TransferEvent>(
            eventLog => erc20transferEventLogs.Add(eventLog));

        var erc721TransferHandler = new EventLogProcessorHandler<ApprovalEventDTO>(
            eventLog => approvalEventLogs.Add(eventLog)); 

        var processingHandlers = new ProcessorHandler<FilterLog>[] {
            erc20TransferHandler, erc721TransferHandler};

        var contractFilter = new NewFilterInput { 
            Address = new []{ ""0x9EDCb9A9c4d34b5d6A082c86cb4f117A1394F831"" } };

        //create our processor to retrieve transfers
        //restrict the processor to Transfers for a specific contract address
        var processor = web3.Processing.Logs.CreateProcessor(
            logProcessors: processingHandlers, filter: contractFilter);

        //if we need to stop the processor mid execution - call cancel on the token
        var cancellationToken = new CancellationToken();

        //crawl the required block range
        await processor.ExecuteAsync(
            toBlockNumber: new BigInteger(3621716),
            cancellationToken: cancellationToken,
            startAtBlockNumberIfNotProcessed: new BigInteger(3621715));

        Console.WriteLine($""Expected 2 ERC20 transfers. Logs found: {erc20transferEventLogs.Count}."");
        Console.WriteLine($""Expected 1 Approval. Logs found: {approvalEventLogs.Count}."");
    }
}" },

new CodeSample()
                {
                    Name = "Log Processing: Any contract one event",
                    Code = @"

using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

public class LogProcessing_AnyContractOneEvent
{
    [Event(""Transfer"")]
    public class TransferEvent: IEventDTO
    {
        [Parameter(""address"", ""_from"", 1, true)]
        public string From { get; set; }

        [Parameter(""address"", ""_to"", 2, true)]
        public string To { get; set; }

        [Parameter(""uint256"", ""_value"", 3, false)]
        public BigInteger Value { get; set; }
    }

    public async static Task Main(string[] args)
    {
        var transferEventLogs = new List<EventLog<TransferEvent>>();

        var web3 = new Web3(""https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c"");

        //create our processor to retrieve transfers
        //restrict the processor to Transfers
        var processor = web3.Processing.Logs.CreateProcessor<TransferEvent>(
            tfr => transferEventLogs.Add(tfr));

        //if we need to stop the processor mid execution - call cancel on the token
        var cancellationToken = new CancellationToken();

        //crawl the required block range
        await processor.ExecuteAsync(
            toBlockNumber: new BigInteger(3146690),
            cancellationToken: cancellationToken,
            startAtBlockNumberIfNotProcessed: new BigInteger(3146684));

        Console.WriteLine($""Expected 13 transfers. Logs found: {transferEventLogs.Count}."");
    }


}" },

new CodeSample()
                {
                    Name = "Log Processing: Many contracts one event",
                    Code = @"

using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

public class LogProcessing_ManyContractsOneEvent
{
    [Event(""Transfer"")]
    public class TransferEvent: IEventDTO
    {
        [Parameter(""address"", ""_from"", 1, true)]
        public string From { get; set; }

        [Parameter(""address"", ""_to"", 2, true)]
        public string To { get; set; }

        [Parameter(""uint256"", ""_value"", 3, false)]
        public BigInteger Value { get; set; }
    }

    public static async Task Main(string[] args)
    {
        var transferEventLogs = new List<EventLog<TransferEvent>>();

        var web3 = new Web3(""https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c"");

        var contractAddresses = new [] { ""0x109424946d5aa4425b2dc1934031d634cdad3f90"", ""0x16c45b25c4817bdedfce770f795790795c9505a6"" };

        //create our processor to retrieve transfers
        //restrict the processor to Transfers for a specific contract address
        var processor = web3.Processing.Logs.CreateProcessorForContracts<TransferEvent>(contractAddresses, tfr => transferEventLogs.Add(tfr));

        //if we need to stop the processor mid execution - call cancel on the token
        var cancellationToken = new CancellationToken();

        //crawl the required block range
        await processor.ExecuteAsync(
            toBlockNumber: new BigInteger(3146690),
            cancellationToken: cancellationToken,
            startAtBlockNumberIfNotProcessed: new BigInteger(3146684));

        Console.WriteLine($""Expected 5 transfers. Logs found: {transferEventLogs.Count}."");
    }

  
}
" },

new CodeSample()
                {
                    Name = "Log Processing: One contract any log",
                    Code = @"
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

public class LogProcessing_OneContractAnyLog
{        
    public static async Task Main()
    {
        var logs = new List<FilterLog>();

        var web3 = new Web3(""https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c"");

        //create our processor to retrieve transfers
        var processor = web3.Processing.Logs.CreateProcessorForContract(
            ""0x109424946d5aa4425b2dc1934031d634cdad3f90"", log => logs.Add(log));

        //if we need to stop the processor mid execution - call cancel on the token
        var cancellationToken = new CancellationToken();

        //crawl the required block range
        await processor.ExecuteAsync(
            toBlockNumber: new BigInteger(3146690),
            cancellationToken: cancellationToken,
            startAtBlockNumberIfNotProcessed: new BigInteger(3146684));

        Console.WriteLine($""Expected 4 logs. Logs found: {logs.Count}."");
    }
}
" },

new CodeSample()
                {
                    Name = "Log Processing: One contract one event",
                    Code = @"
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

public class LogProcessing_OneContractOneEvent
{
    [Event(""Transfer"")]
    public class TransferEvent: IEventDTO
    {
        [Parameter(""address"", ""_from"", 1, true)]
        public string From { get; set; }

        [Parameter(""address"", ""_to"", 2, true)]
        public string To { get; set; }

        [Parameter(""uint256"", ""_value"", 3, false)]
        public BigInteger Value { get; set; }
    }

    public static async Task Main(string[] args)
    {
        var transferEventLogs = new List<EventLog<TransferEvent>>();

        var web3 = new Web3(""https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c"");

        //create our processor to retrieve transfers
        //restrict the processor to Transfers for a specific contract address
        var processor = web3.Processing.Logs.CreateProcessorForContract<TransferEvent>(
            ""0x109424946d5aa4425b2dc1934031d634cdad3f90"", 
            tfr => transferEventLogs.Add(tfr));

        //if we need to stop the processor mid execution - call cancel on the token
        var cancellationToken = new CancellationToken();

        //crawl the required block range
        await processor.ExecuteAsync(
            toBlockNumber: new BigInteger(3146690),
            cancellationToken: cancellationToken,
            startAtBlockNumberIfNotProcessed: new BigInteger(3146684));

        Console.WriteLine($""Expected 1 Log. Logs found: {transferEventLogs.Count}."");
    }

}
" },

new CodeSample()
                {
                    Name = "Log Processing: One contract one event async",
                    Code = @"
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

public class LogProcessing_OneContractOneEventAsync
{
    [Event(""Transfer"")]
    public class TransferEvent: IEventDTO
    {
        [Parameter(""address"", ""_from"", 1, true)]
        public string From { get; set; }

        [Parameter(""address"", ""_to"", 2, true)]
        public string To { get; set; }

        [Parameter(""uint256"", ""_value"", 3, false)]
        public BigInteger Value { get; set; }
    }
        
    public static async Task Main(string[] args)
    {
        var transferEventLogs = new List<EventLog<TransferEvent>>();

        var web3 = new Web3(""https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c"");

        Task StoreLogAsync(EventLog<TransferEvent> eventLog)
        {
            transferEventLogs.Add(eventLog);
            return Task.CompletedTask;
        }

        //create our processor to retrieve transfers
        //restrict the processor to Transfers for a specific contract address
        var processor = web3.Processing.Logs.CreateProcessorForContract<TransferEvent>(
            ""0x109424946d5aa4425b2dc1934031d634cdad3f90"", StoreLogAsync);

        //if we need to stop the processor mid execution - call cancel on the token
        var cancellationToken = new CancellationToken();

        //crawl the required block range
        await processor.ExecuteAsync(
            toBlockNumber: new BigInteger(3146690),
            cancellationToken: cancellationToken,
            startAtBlockNumberIfNotProcessed: new BigInteger(3146684));

        Console.WriteLine($""Expected 1 Log. Logs found: {transferEventLogs.Count}."");
    }

}
" },

new CodeSample()
                {
                    Name = "Log Processing: One contract one event with criteria",
                    Code = @"
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

public class LogProcessing_OneContractOneEventWithCriteria
{
    [Event(""Transfer"")]
    public class TransferEvent: IEventDTO
    {
        [Parameter(""address"", ""_from"", 1, true)]
        public string From { get; set; }

        [Parameter(""address"", ""_to"", 2, true)]
        public string To { get; set; }

        [Parameter(""uint256"", ""_value"", 3, false)]
        public BigInteger Value { get; set; }
    }
        
    public static async Task Main(string[] args)
    {
        var transferEventLogs = new List<EventLog<TransferEvent>>();

        var web3 = new Web3(""https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c"");

        //create our processor to retrieve transfers
        //restrict the processor to Transfers for a specific contract address
        var processor = web3.Processing.Logs.CreateProcessorForContract<TransferEvent>(
            ""0x109424946d5aa4425b2dc1934031d634cdad3f90"", 
            action: tfr => transferEventLogs.Add(tfr),
            criteria: tfr => tfr.Event.Value > 0);

        //if we need to stop the processor mid execution - call cancel on the token
        var cancellationToken = new CancellationToken();

        //crawl the required block range
        await processor.ExecuteAsync(
            toBlockNumber: new BigInteger(3146690),
            cancellationToken: cancellationToken,
            startAtBlockNumberIfNotProcessed: new BigInteger(3146684));

        Console.WriteLine($""Expected 1 Log. Logs found: {transferEventLogs.Count}."");
    }

}
" }




				

            };
        }

    }

}
