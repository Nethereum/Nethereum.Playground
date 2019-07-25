namespace Nethereum.TryOnBrowser.Pages
{
    public class CodeSampleRepository
    {
        public CodeSample[] GetCodeSamples()
        {
            return new CodeSample[]
            {


                new CodeSample()
                {
                    Name="Query ERC20 Smart contract balance",
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
                    Name = "Message signing",
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
                    Name = "Create new private key / account",
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
       //note this uses SecureRandom
       var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
       var privateKey = ecKey.GetPrivateKeyAsBytes().ToHex();
       Console.WriteLine(privateKey);
       var address = ecKey.GetPublicAddress();
       Console.WriteLine(address);
    }

}
                "
                },

                new CodeSample()
                {
                    Name = "Connecting to INFURA",
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
                    Name = "Converting units",
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
var web3 = new Web3(""
var web3 = new Web3(""http://13.69.185.76:8545"");

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
var BackToWei = Web3.Convert.ToWei(balanceInEther);
Console.WriteLine(""Balance of Ethereum Foundation's account back in Wei: "" + BackToWei);
    }

}
                "
                },

                new CodeSample()
                {
                    Name = "Managing Chain-IDs",
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

var web3 = new Web3(account);

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
                    Name = "Managing HdWallets",
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

// An Hd Wallet is deterministic, it will derive the same unlimited number of addresses 
// given the same seed (password+wordlist).
// All the created accounts can be loaded in a Web3 instance and used as any other account, 
// we can for instance check the balance of one of them:

var account1 = new Wallet(Words, Password1).GetAccount(0);
Console.WriteLine(""account1 address is: ""+account1.Address);
var web3 = new Web3(account1);
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
                }

            };
        }

    }

}
