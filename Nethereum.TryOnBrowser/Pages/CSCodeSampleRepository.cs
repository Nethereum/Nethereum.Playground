﻿namespace Nethereum.TryOnBrowser.Pages
{
    public class CodeSampleRepository
    {
        public CodeSample[] GetCodeSamples()
        {
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
    Name = "Getting current block number",
        Code = @"
using System;
using Nethereum.Web3; 
using System.Threading.Tasks;

public class Program
{

    static async Task Main(string[] args)
    {
    var web3 = new Web3(""http://testchain.nethereum.com:8545"");
    var blockNumber = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
    Console.WriteLine(blockNumber.Value);
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
}
                "
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
}
                "
                }					
            };
        }

    }

}
