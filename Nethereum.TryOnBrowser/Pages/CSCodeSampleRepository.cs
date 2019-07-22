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
//Connecting to Ethereum mainnet using Infura
var web3 = new Web3(""https://mainnet.infura.io/v3/7238211010344719ad14a89db874158c"");

// Check the balance of one of the accounts provisioned in our chain, to do that, we can execute the GetBalance request asynchronously:
// By default, the returned value is in Wei (the lowest unit of value), not necessarily easy to read unless you’re really talented at Maths:
var balance = await web3.Eth.GetBalance.SendRequestAsync(""0xde0b295669a9fd93d5f28d9ec85e40f4cb697bae"");
Console.WriteLine(""Balance of Ethereum Foundation's account: "" + balance.Value);

// To make the return value more human friendly, we can convert the balance to Ether using the conversion utility’s ""**FromWei**"" method:

var balanceInEther = Web3.Convert.FromWei(balance.Value);
Console.WriteLine(""Balance of Ethereum Foundation's account in Ether: "" + balanceInEther);

// We can even “counter convert” the balance back to wei using the “**ToWei**” method (this has no other purpose than demonstrating the method, of course):
var BackToWei = Web3.Convert.ToWei(balanceInEther);
Console.WriteLine(""Balance of Ethereum Foundation's account back in Wei: "" + BackToWei);
    }

}
                "
                },

                new CodeSample()
                {
                    Name = "Sending transactions",
                    Code = @"
using System;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Web3.Accounts.Managed;
using Nethereum.Hex.HexTypes;

public class Program
{

    static async Task Main(string[] args)
    {
// We can now create a new instance of Web3:

var web3 = new Web3();

// Sending a transaction

// To send a transaction you will either manage your account and sign the raw transaction locally, or the account will be managed by the client (Parity / Geth), requiring to send the password at the time of sending a transaction or unlock the account before hand.

// In **`Nethereum.Web3`**, to simplify the process, there are two types of accounts that you can use. An ""Account"" object or a **`ManagedAccount`** object. Both store the account information required to send a transaction, private key, or password.

// At the time of sending a transaction, the right method to deliver the transaction will be chosen. If using Nethereum **`TransactionManager`**, deploying a contract or using a contract function, the transaction will either be signed offline using the private key or a **`personal\_sendTransaction`** message will be sent using the password.

// The below explains how to: 

// Sending a transaction with an `Account` object

// Here is how to set up a new account by creating an `account` object instance:

var privateKey = ""0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7"";
var account = new Account(privateKey);

var toAddress = ""0x12890D2cce102216644c59daE5baed380d84830c"";
var transaction = await web3.TransactionManager.SendTransactionAsync(account.Address, toAddress, new Nethereum.Hex.HexTypes.HexBigInteger(1));

// Sending a transaction with a `ManagedAccount` object

// As said earlier: Nethereum's managed accounts are maintained by the Ethereum client (geth/parity), allowing to automatic sign transactions and to manage the account's private key securely:

var senderAddress = ""0x12890d2cce102216644c59daE5baed380d84830c"";
var addressTo2 = ""0x13f022d72158410433cbd66f5dd8bf6d2d129924"";
var password = ""password"";
var account2 = new ManagedAccount(senderAddress, password);
var web3 = new Web3(account);

// We can now perform our transaction using the **`TransactionManager`**, the signing will be made automatically by the Ethereum client in use:

var transaction2 = await web3.TransactionManager.SendTransactionAsync(account2.Address, addressTo2, new HexBigInteger(20));
    }

}
                "
                }

            };
        }

    }

}
