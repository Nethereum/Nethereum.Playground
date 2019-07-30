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
                      Name = "Accounts: Creating a new Account using Geth Personal Api",
                      Code = @"
using System;
using Nethereum.Web3; 
using System.Threading.Tasks;

public class Program
{

    static async Task Main(string[] args)
    {


// First, create a new instance of Web3:

        var web3 = new Web3(""http://testchain.nethereum.com:8545"");
        var account = await web3.Personal.NewAccount.SendRequestAsync(""password"");
        Console.WriteLine(""The address of the newly created account is: ""+ account);
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
" },

 new CodeSample()
                {
                    Name = "Smart Contract: deploying a contract",
                    Code = @"
using Nethereum.Web3;
using System;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.CQS;
using Nethereum.Util;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Contracts;
using Nethereum.Contracts.Extensions;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

public class BlockProcessing_WithTransactionCriteria
{

// To deploy a contract we will create a class inheriting from the ContractDeploymentMessage, here we can include our compiled byte code and other constructor parameters.

// As we can see below the StandardToken deployment message includes the compiled bytecode of the ERC20 smart contract and the constructor parameter with the “totalSupply” of tokens.

// Each parameter is described with an attribute Parameter, including its name ""totalSupply"", type ""uint256"" and order.

public class StandardTokenDeployment : ContractDeploymentMessage
{

            public static string BYTECODE = ""0x60606040526040516020806106f5833981016040528080519060200190919050505b80600160005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005081905550806000600050819055505b506106868061006f6000396000f360606040523615610074576000357c010000000000000000000000000000000000000000000000000000000090048063095ea7b31461008157806318160ddd146100b657806323b872dd146100d957806370a0823114610117578063a9059cbb14610143578063dd62ed3e1461017857610074565b61007f5b610002565b565b005b6100a060048080359060200190919080359060200190919050506101ad565b6040518082815260200191505060405180910390f35b6100c36004805050610674565b6040518082815260200191505060405180910390f35b6101016004808035906020019091908035906020019091908035906020019091905050610281565b6040518082815260200191505060405180910390f35b61012d600480803590602001909190505061048d565b6040518082815260200191505060405180910390f35b61016260048080359060200190919080359060200190919050506104cb565b6040518082815260200191505060405180910390f35b610197600480803590602001909190803590602001909190505061060b565b6040518082815260200191505060405180910390f35b600081600260005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060008573ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050819055508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925846040518082815260200191505060405180910390a36001905061027b565b92915050565b600081600160005060008673ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050541015801561031b575081600260005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060003373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000505410155b80156103275750600082115b1561047c5781600160005060008573ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505401925050819055508273ffffffffffffffffffffffffffffffffffffffff168473ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef846040518082815260200191505060405180910390a381600160005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060008282825054039250508190555081600260005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060003373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505403925050819055506001905061048656610485565b60009050610486565b5b9392505050565b6000600160005060008373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000505490506104c6565b919050565b600081600160005060003373ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050541015801561050c5750600082115b156105fb5781600160005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060008282825054039250508190555081600160005060008573ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505401925050819055508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef846040518082815260200191505060405180910390a36001905061060556610604565b60009050610605565b5b92915050565b6000600260005060008473ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060008373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005054905061066e565b92915050565b60006000600050549050610683565b9056"";

    public StandardTokenDeployment() : base(BYTECODE){}

    [Parameter(""uint256"", ""totalSupply"")]
    public BigInteger TotalSupply { get; set; }
}

    public static async Task Main()
    {

//  Instantiating Web3 and the Account

// A simple way to run this sample is to use one of the pre-configured private chains which can be found https://github.com/Nethereum/TestChains (Geth, Parity, Ganache) using the Account “0x12890d2cce102216644c59daE5baed380d84830c” with private key “0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7“, or alternatively use your own testchain with your own account / private key.

// To create an instance of web3 we first provide the url of our testchain and the private key of our account. When providing an Account instantiated with a  private key all our transactions will be signed “offline” by Nethereum.

var url = ""http://testchain.nethereum.com:8545"";
var privateKey = ""0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7"";
var account = new Account(privateKey);
var web3 = new Web3(account, url);

//  Deploying the Contract

// The next step is to deploy our Standard Token ERC20 smart contract, in this scenario the total supply (number of tokens) is going to be 100,000.

// First we create an instance of the StandardTokenDeployment with the TotalSupply amount.

var deploymentMessage = new StandardTokenDeployment
{
    TotalSupply = 100000
};

// Then we create a deployment handler using our contract deployment definition and simply deploy the contract using the deployment message. We are auto estimating the gas, getting the latest gas price and nonce so nothing else is set anything on the deployment message.

// Finally, we wait for the deployment transaction to be mined, and retrieve the contract address of the new contract from the receipt.

var deploymentHandler = web3.Eth.GetContractDeploymentHandler<StandardTokenDeployment>();
var transactionReceipt = await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage);
var contractAddress = transactionReceipt.ContractAddress;
Console.WriteLine(""Deployed Contract address is: ""+contractAddress);
    }

}
"
    },				


 new CodeSample()
                {
                    Name = "Smart Contracts",
                    Code = @"
using Nethereum.Web3;
using System;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.CQS;
using Nethereum.Util;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Contracts;
using Nethereum.Contracts.Extensions;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

public class GetStartedSmartContracts
{


// # Quick introduction to smart contracts integration with Nethereum

// This document is a Workbook, find more about workbooks' installation requirements  [here](https://docs.microsoft.com/en-us/xamarin/tools/workbooks/install).

// Documentation about Nethereum can be found at: <https://docs.nethereum.com>

// The purpose of this sample is the following:

// * Understanding how to create contract deployment, function and event definitions to interact with a smart contracts

// * Creating an account object using a private key, this will allow to sign transactions ""offline"".

// * Deploying a smart contract (the sample provided is the standard ERC20 token contract)

// * Making a call to a smart contract (in this scenario get the balance of an account)

// * Sending a transaction to the smart contract (in this scenario transferring balance)

// * Estimating the gas cost of a contract transaction

// * Gas Price, Nonces and Sending Ether to smart contracts

// * Signing online / offline transaction function messages and deployment messages

// * Extension methods for Deployment and Function messages

// * Retrieving the state of a smart contract from a previous block

// ### Pre-Conditions

// In this tutorial we are going to interact with the ERC20 standard token contract. The smart contract provides a standard way to create a new token, transfer it to another account and query the balance of any account. This standard interface allows the interoperability of smart contracts providing the same signature and applications that integrate with it.

// ![Constructor, transfer, balance and event of ERC20](https://github.com/Nethereum/Nethereum.Workbooks/raw/master/docs/screenshots/simpleERC20.png)

// ## Pre-requisites:

// Download the test chain matching your environment from https://github.com/Nethereum/Testchains

// Start a Geth chain ( **geth-clique-linux\_** **_geth-clique-windows_** **_geth-clique-mac_**) using startgeth.bat (Windows) or startgeth.sh (Mac/Linux). The chain is setup with the Proof of Authority consensus and will start the mining process immediately.

// Then, declare your namespaces and contract definition to interact with the smart contract. In this scenario we are only interested in the Deployment, Transfer function and BalanceOf Function of the ERC20 smart contract.


// To deploy a contract we will create a class inheriting from the ContractDeploymentMessage, here we can include our compiled byte code and other constructor parameters.

// As we can see below the StandardToken deployment message includes the compiled bytecode of the ERC20 smart contract and the constructor parameter with the “totalSupply” of tokens.

// Each parameter is described with an attribute Parameter, including its name ""totalSupply"", type ""uint256"" and order.


public class StandardTokenDeployment : ContractDeploymentMessage
{

            public static string BYTECODE = ""0x60606040526040516020806106f5833981016040528080519060200190919050505b80600160005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005081905550806000600050819055505b506106868061006f6000396000f360606040523615610074576000357c010000000000000000000000000000000000000000000000000000000090048063095ea7b31461008157806318160ddd146100b657806323b872dd146100d957806370a0823114610117578063a9059cbb14610143578063dd62ed3e1461017857610074565b61007f5b610002565b565b005b6100a060048080359060200190919080359060200190919050506101ad565b6040518082815260200191505060405180910390f35b6100c36004805050610674565b6040518082815260200191505060405180910390f35b6101016004808035906020019091908035906020019091908035906020019091905050610281565b6040518082815260200191505060405180910390f35b61012d600480803590602001909190505061048d565b6040518082815260200191505060405180910390f35b61016260048080359060200190919080359060200190919050506104cb565b6040518082815260200191505060405180910390f35b610197600480803590602001909190803590602001909190505061060b565b6040518082815260200191505060405180910390f35b600081600260005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060008573ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050819055508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925846040518082815260200191505060405180910390a36001905061027b565b92915050565b600081600160005060008673ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050541015801561031b575081600260005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060003373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000505410155b80156103275750600082115b1561047c5781600160005060008573ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505401925050819055508273ffffffffffffffffffffffffffffffffffffffff168473ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef846040518082815260200191505060405180910390a381600160005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060008282825054039250508190555081600260005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060003373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505403925050819055506001905061048656610485565b60009050610486565b5b9392505050565b6000600160005060008373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000505490506104c6565b919050565b600081600160005060003373ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050541015801561050c5750600082115b156105fb5781600160005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060008282825054039250508190555081600160005060008573ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505401925050819055508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef846040518082815260200191505060405180910390a36001905061060556610604565b60009050610605565b5b92915050565b6000600260005060008473ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060008373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005054905061066e565b92915050565b60006000600050549050610683565b9056"";

    public StandardTokenDeployment() : base(BYTECODE){}

    [Parameter(""uint256"", ""totalSupply"")]
    public BigInteger TotalSupply { get; set; }
}


// We can call the functions of smart contract to query the state of a smart contract or do any computation, which will not affect the state of the blockchain.

// To do so,  we will need to create a class which inherits from ""FunctionMessage"". First we will decorate the class with a ""Function"" attribute, including the name and return type.

// Each parameter of the function will be a property of the class, each of them decorated with the ""Parameter"" attribute, including the smart contract’s parameter name, type and parameter order.

// For the ERC20 smart contract, the ""balanceOf"" function definition, provides the query interface to get the token balance of a given address. As we can see this function includes only one parameter ""\_owner"", of the type ""address"".


[Function(""balanceOf"", ""uint256"")]
public class BalanceOfFunction : FunctionMessage
{
    [Parameter(""address"", ""_owner"", 1)]
    public string Owner { get; set; }
}


// Another type of smart contract function will be a transaction that will change the state of the smart contract (or smart contracts).

// For example The ""transfer"" function definition for the ERC20 smart contract, includes the parameters “\_to”, which is an address parameter as a string, and the “\_value” or TokenAmount we want to transfer.

// In a similar way to the ""balanceOf"" function, all the parameters include the solidity type, the contract’s parameter name and parameter order.

// Note: When working with functions, it is very important to have the parameters types and function name correct as all of these make the signature of the function.


[Function(""transfer"", ""bool"")]
public class TransferFunction : FunctionMessage
{
    [Parameter(""address"", ""_to"", 1)]
    public string To { get; set; }

    [Parameter(""uint256"", ""_value"", 2)]
    public BigInteger TokenAmount { get; set; }
}


// Finally, smart contracts also have events. Events defined in smart contracts write in the blockchain log, providing a way to retrieve further information when a smart contract interaction occurs.

// To create an Event definition, we need to create a class that inherits from IEventDTO, decorated with the Event attribute.

// The Transfer Event is similar to a Function: it  also includes parameters with name, order and type. But also a boolean value indicating if the parameter is indexed or not.

// Indexed parameters will allow us later on to query the blockchain for those values.


[Event(""Transfer"")]
public class TransferEventDTO : IEventDTO
{
    [Parameter(""address"", ""_from"", 1, true)]
    public string From { get; set; }

    [Parameter(""address"", ""_to"", 2, true)]
    public string To { get; set; }

    [Parameter(""uint256"", ""_value"", 3, false)]
    public BigInteger Value { get; set; }
}
// ### Multiple return types or complex objects

// Functions of smart contracts can return one or multiple values in a single call. To decode the returned values, we use a FunctionOutputDTO.

// Function outputs are classes which are decorated with a FunctionOutput attribute and implement the interface IFunctionOutputDTO.

// An example of this is the following implementation that can be used to return the single value of the Balance on the ERC20 smart contract.


 [FunctionOutput]
 public class BalanceOfOutputDTO : IFunctionOutputDTO
 {
      [Parameter(""uint256"", ""balance"", 1)]
      public BigInteger Balance { get; set; }
 }


// If we were going to return multiple values we could have something like:


 [FunctionOutput]
 public class BalanceOfOutputMultipleDTO : IFunctionOutputDTO
 {
      [Parameter(""uint256"", ""balance1"", 1)]
      public BigInteger Balance1 { get; set; }

      [Parameter(""uint256"", ""balance2"", 2)]
      public BigInteger Balance2 { get; set; }

      [Parameter(""uint256"", ""balance1"", 3)]
      public BigInteger Balance3 { get; set; }
 }



    public static async Task Main()
{    
// ### Instantiating Web3 and the Account

// A simple way to run this sample is to use one of the pre-configured private chains which can be found at [https://github.com/Nethereum/TestChains ](https://github.com/Nethereum/TestChains)(Geth, Parity, Ganache) using the Account “0x12890d2cce102216644c59daE5baed380d84830c” with private key “0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7“, or alternatively use your own testchain with your own account / private key.

// To create an instance of web3 we first provide the url of our testchain and the private key of our account. When providing an Account instantiated with a  private key, all our transactions will be signed by Nethereum.


var url = ""http://testchain.nethereum.com:8545"";
var privateKey = ""0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7"";
var account = new Account(privateKey);
var web3 = new Web3(account, url);


// ### Deploying the Contract

// The next step is to deploy our Standard Token ERC20 smart contract, in this scenario the total supply (number of tokens) is going to be 100,000.

// First we create an instance of the StandardTokenDeployment with the TotalSupply amount.


var deploymentMessage = new StandardTokenDeployment
{
    TotalSupply = 100000
};


// Then we create a deployment handler using our contract deployment definition and simply deploy the contract using the deployment message. We are auto estimating the gas, getting the latest gas price and nonce so nothing else is set anything on the deployment message.

// Finally, we wait for the deployment transaction to be mined, and retrieve the contract address of the new contract from the receipt.


var deploymentHandler = web3.Eth.GetContractDeploymentHandler<StandardTokenDeployment>();
var transactionReceipt = await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage);
var contractAddress = transactionReceipt.ContractAddress;


// ### Interacting with the Contract

// Once we have deployed the contract, we can start interacting with the contract.

// #### Querying

// To retrieve the balance of an address we can create an instance of the BalanceFunction message and set the parameter as our account ""Address"", since we are the ""owner"" of the Token, the full balance has been assigned to us.

// To retrieve the balance, we will create a QueryHandler and finally using our contract address and message retrieve the balance amount.

 var balanceOfFunctionMessage = new BalanceOfFunction()
 {
    Owner = account.Address,
};
var balanceHandler = web3.Eth.GetContractQueryHandler<BalanceOfFunction>();
var balance = await balanceHandler.QueryAsync<BigInteger>(contractAddress, balanceOfFunctionMessage);
Console.WriteLine(""Balance of address""+ balance);

// When Querying The chain we will use the following method instead:


var balanceOutput = await balanceHandler.QueryDeserializingToObjectAsync<BalanceOfOutputDTO>( balanceOfFunctionMessage, contractAddress);


// #### Querying previous state of the smart contract

// Another great feature of the Ethereum blockchain is the capability to retrieve the state of a smart contract from a previous block.

// For example, we could get the balance of the owner at the time of deployment by using the block number in which the contract was deployed.


balanceOutput = await balanceHandler.QueryDeserializingToObjectAsync<BalanceOfOutputDTO>( balanceOfFunctionMessage, contractAddress, new Nethereum.RPC.Eth.DTOs.BlockParameter(transactionReceipt.BlockNumber));
Console.WriteLine(""Transaction has been written in Block Number: ""+transactionReceipt.BlockNumber);

// #### Transfer

// Making a transfer will change the state of the blockchain, so in this scenario we will need to create a TransactionHandler using the TransferFunction definition.

// In the transfer message, we will include the receiver address ""To"", and the ""TokenAmount"" to transfer.

// The final step is to Send the request, wait for the receipt to be “mined” and included in the blockchain.

// Another option will be to not wait (poll) for the transaction to be mined and just retrieve the transaction hash.


var receiverAddress = ""0xde0B295669a9FD93d5F28D9Ec85E40f4cb697BAe"";
var transferHandler = web3.Eth.GetContractTransactionHandler<TransferFunction>();
var transfer = new TransferFunction()
{
    To = receiverAddress,
    TokenAmount = 100
};
transactionReceipt = await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, transfer);
Console.WriteLine(""Transaction hash is: ""+transactionReceipt.TransactionHash);

// ##### Transferring Ether to a smart contract

// A function or deployment transaction can send Ether to the smart contract. The FunctionMessage and DeploymentMessage have the property ""AmountToSend"".

// So if the ""transfer"" function also accepts Ether, we will set it this way.


transfer.AmountToSend = Nethereum.Web3.Web3.Convert.ToWei(1);


// The GasPrice is set in ""Wei"" which is the lowest unit in Ethereum, so in the scenario above we have converted 1 Ether to Wei.

// ### Gas Price

// Nethereum automatically sets the GasPrice if not provided by using the clients ""GasPrice"" call, which provides the average gas price from previous blocks.

// If you want to have more control over the GasPrice these can be set in both FunctionMessages and DeploymentMessages.


  transfer.GasPrice =  Nethereum.Web3.Web3.Convert.ToWei(25, UnitConversion.EthUnit.Gwei);


// The GasPrice is set in ""Wei"" which is the lowest unit in Ethereum, so if we are used to the usual ""Gwei"" units, this will need to be converted using the Nethereum Convertion utilities.

// ### Estimating Gas

// Nethereum does an automatic estimation of the total gas necessary to make the function transaction by calling the ""EthEstimateGas"" internally with the ""CallInput"".

// If needed, this can be done manually, using the TransactionHandler and the ""transfer"" transaction FunctionMessage.


 var estimate = await transferHandler.EstimateGasAsync(contractAddress, transfer);
 transfer.Gas = estimate.Value;


// ### Nonces

// Each account transaction has a Nonce associated with it, this is the order and unique number for that transaction. This allows each transaction to be differentiated from each other, but also ensure transactions are processed in the same order.

// Nethereum calculates the Nonce automatically for all Transactions by retrieving the latest count of the transactions from the chain. Also internally manages at Account level an in memory counter on the nonces, to allow for situations in which we want to send multiple transactions before giving time to the Ethereum client to update its internal counter.

// Nevertheless there might be scenarios where we want to supply our Nonce, for example if we want to sign the transaction completely offline.


transfer.Nonce = 2;


// ### Signing a Function / Deployment message online / offline

// The TransactionHandler also provides a mechanism to sign the Function and Deployments messages, provided we use an Account and/or an ExternalAccount


var signedTransaction1 = await transferHandler.SignTransactionAsync(contractAddress, transfer);

Console.WriteLine(""signedTransaction1 is: ""+signedTransaction1);

// Nethereum internally calls the Ethereum client to set the GasPrice, Nonce and estimate the Gas, so if we want to sign the transaction for the contract completely offline we will need to set those values before hand.


transfer.Nonce = 2;
transfer.Gas = 21000;
transfer.GasPrice =  Nethereum.Web3.Web3.Convert.ToWei(25, UnitConversion.EthUnit.Gwei);
var signedTransaction2 = await transferHandler.SignTransactionAsync(contractAddress, transfer);
Console.WriteLine(""signedTransaction2 is: ""+signedTransaction2);
                }
                }
"
                }				


            };
        }

    }

}
