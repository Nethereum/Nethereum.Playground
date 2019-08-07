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
                    Name = "Chain information: Query Ether account balance using Infura",
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
                    Name = "Chain information: Get Block number, Block, Transaction and Receipt using Infura",
                    Code = @"

using System;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.RPC.Eth.Blocks;
using Nethereum.Hex.HexTypes;

public class EthRpcCalls_BlockNumber_Block_Transaction_Receipt
{

    static async Task Main(string[] args)
    {
        //Connecting to Ethereum mainnet using Infura
      	var web3 = new Web3(""https://mainnet.infura.io/v3/7238211010344719ad14a89db874158c"");

				//Getting current block number  
        var blockNumber = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
        Console.WriteLine(""Current BlockNumber is: "" + blockNumber.Value);

				//Getting current block with transactions 
				var block = await web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(new HexBigInteger(8257129));
        Console.WriteLine(""Block number: ""+ block.Number.Value);
        Console.WriteLine(""Block hash: ""+ block.BlockHash);
				Console.WriteLine(""Block no of transactions:"" + block.Transactions.Length);
				Console.WriteLine(""Block transaction 0 From:"" + block.Transactions[0].From);
				Console.WriteLine(""Block transaction 0 To:"" + block.Transactions[0].To);
				Console.WriteLine(""Block transaction 0 Amount:"" + block.Transactions[0].Value);
				Console.WriteLine(""Block transaction 0 Hash:"" + block.Transactions[0].TransactionHash);

			  var transaction = await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(""0xb4729a0d8dd30e3070d0cb203090f2b792e029f6fa4629e96d2ebc1de13cb5c4"");
				Console.WriteLine(""Transaction From:"" + transaction.From);
				Console.WriteLine(""Transaction To:"" + transaction.To);
				Console.WriteLine(""Transaction Amount:"" + transaction.Value);
				Console.WriteLine(""Transaction Hash:"" + transaction.TransactionHash);


				 var transactionReceipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(""0xb4729a0d8dd30e3070d0cb203090f2b792e029f6fa4629e96d2ebc1de13cb5c4"");
				Console.WriteLine(""Transaction Hash:"" + transactionReceipt.TransactionHash);
				Console.WriteLine(""TransactionReceipt Logs:"" + transactionReceipt.Logs.Count);
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
                    Name = "Smart Contracts: Query ERC20 Smart contract balance",
                    Code = @"
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

                    Name = "Smart Contracts: Smart contract deployment",
                    Code = @"

using Nethereum.Web3;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.CQS;
using Nethereum.Util;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Contracts;
using Nethereum.Contracts.Extensions;
using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;


public class SmartContracts_DeployingContract
{

// To deploy a contract we will create a class inheriting from the ContractDeploymentMessage, 
// here we can include our compiled byte code and other constructor parameters.
// As we can see below the StandardToken deployment message includes the compiled bytecode 
// of the ERC20 smart contract and the constructor parameter with the “totalSupply” of tokens.
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

// To create an instance of web3 we first provide the url of our testchain and the private key of our account. 
// When providing an Account instantiated with a  private key all our transactions will be signed “offline” by Nethereum.

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

                    Name =
                        "Smart Contracts: Smart Contracts Deployment, Querying, Transactions, Nonces, Estimating Gas, Gas Price",

                    Code = @"


using Nethereum.Web3;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.CQS;
using Nethereum.Util;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Contracts;
using Nethereum.Contracts.Extensions;
using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;


public class GetStartedSmartContracts
{


/* Quick introduction to smart contracts integration with Nethereum

	Topics covered:
   
	 * Understanding how to create contract deployment, function and event definitions to interact with a smart contracts
	 * Creating an account object using a private key, this will allow to sign transactions ""offline"".
	 * Deploying a smart contract (the sample provided is the standard ERC20 token contract)
	 * Making a call to a smart contract (in this scenario get the balance of an account)
	 * Sending a transaction to the smart contract (in this scenario transferring balance)
	 * Estimating the gas cost of a contract transaction
	 * Gas Price, Nonces and Sending Ether to smart contracts
		* Retrieving the state of a smart contract from a previous block
*/



//********* CONTRACT DEFINITION  *******

//*** Deployment message**** //
// To deploy a contract we will create a class inheriting from the ContractDeploymentMessage, 
// here we can include our compiled byte code and other constructor parameters.
// As we can see below the StandardToken deployment message includes the compiled bytecode 
// of the ERC20 smart contract and the constructor parameter with the “totalSupply” of tokens.
// Each parameter is described with an attribute Parameter, including its name ""totalSupply"", type ""uint256"" and order.


		public class StandardTokenDeployment : ContractDeploymentMessage
		{
				public static string BYTECODE = ""0x60606040526040516020806106f5833981016040528080519060200190919050505b80600160005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005081905550806000600050819055505b506106868061006f6000396000f360606040523615610074576000357c010000000000000000000000000000000000000000000000000000000090048063095ea7b31461008157806318160ddd146100b657806323b872dd146100d957806370a0823114610117578063a9059cbb14610143578063dd62ed3e1461017857610074565b61007f5b610002565b565b005b6100a060048080359060200190919080359060200190919050506101ad565b6040518082815260200191505060405180910390f35b6100c36004805050610674565b6040518082815260200191505060405180910390f35b6101016004808035906020019091908035906020019091908035906020019091905050610281565b6040518082815260200191505060405180910390f35b61012d600480803590602001909190505061048d565b6040518082815260200191505060405180910390f35b61016260048080359060200190919080359060200190919050506104cb565b6040518082815260200191505060405180910390f35b610197600480803590602001909190803590602001909190505061060b565b6040518082815260200191505060405180910390f35b600081600260005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060008573ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050819055508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925846040518082815260200191505060405180910390a36001905061027b565b92915050565b600081600160005060008673ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050541015801561031b575081600260005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060003373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000505410155b80156103275750600082115b1561047c5781600160005060008573ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505401925050819055508273ffffffffffffffffffffffffffffffffffffffff168473ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef846040518082815260200191505060405180910390a381600160005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060008282825054039250508190555081600260005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060003373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505403925050819055506001905061048656610485565b60009050610486565b5b9392505050565b6000600160005060008373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000505490506104c6565b919050565b600081600160005060003373ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050541015801561050c5750600082115b156105fb5781600160005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060008282825054039250508190555081600160005060008573ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505401925050819055508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef846040518082815260200191505060405180910390a36001905061060556610604565b60009050610605565b5b92915050565b6000600260005060008473ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060008373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005054905061066e565b92915050565b60006000600050549050610683565b9056"";

				public StandardTokenDeployment() : base(BYTECODE){}

				[Parameter(""uint256"", ""totalSupply"")]
				public BigInteger TotalSupply { get; set; }

		}

//*** FUNCTION MESSAGES **** ///

// We can call the functions of smart contract to query the state of a smart contract or do any computation, 
// which will not affect the state of the blockchain.

// To do so,  we will need to create a class which inherits from ""FunctionMessage"". 
// First we will decorate the class with a ""Function"" attribute, including the name and return type.
// Each parameter of the function will be a property of the class, each of them decorated with the ""Parameter"" attribute, 
// including the smart contract’s parameter name, type and parameter order.
// For the ERC20 smart contract, the ""balanceOf"" function definition, 
// provides the query interface to get the token balance of a given address. 
// As we can see this function includes only one parameter ""\_owner"", of the type ""address"".


		[Function(""balanceOf"", ""uint256"")]
		public class BalanceOfFunction : FunctionMessage
		{
				[Parameter(""address"", ""_owner"", 1)]
				public string Owner { get; set; }
		}



// Another type of smart contract function will be a transaction 
// that will change the state of the smart contract (or smart contracts).
// For example The ""transfer"" function definition for the ERC20 smart contract, 
// includes the parameters “\_to”, which is an address parameter as a string, and the “\_value” 
// or TokenAmount we want to transfer.


// In a similar way to the ""balanceOf"" function, all the parameters include the solidity type, 
// the contract’s parameter name and parameter order.


// Note: When working with functions, it is very important to have the parameters types and function name correct 
//as all of these make the signature of the function.

		[Function(""transfer"", ""bool"")]
		public class TransferFunction : FunctionMessage
		{
				[Parameter(""address"", ""_to"", 1)]
				public string To { get; set; }

				[Parameter(""uint256"", ""_value"", 2)]
				public BigInteger TokenAmount { get; set; }

		}

// Finally, smart contracts also have events. Events defined in smart contracts write in the blockchain log, 
// providing a way to retrieve further information when a smart contract interaction occurs.
// To create an Event definition, we need to create a class that inherits from IEventDTO, decorated with the Event attribute.
// The Transfer Event is similar to a Function: it  also includes parameters with name, order and type. 
// But also a boolean value indicating if the parameter is indexed or not.
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
					
					[Parameter(""uint256"", ""balance3"", 3)]
					public BigInteger Balance3 { get; set; }
		}

//**** END CONTRACT DEFINITIONS ***** ///

///*** THE MAIN PROGRAM ***
		public static async Task Main()
		{    

// ### Instantiating Web3 and the Account
// To create an instance of web3 we first provide the url of our testchain and the private key of our account. 
// Here we are using http://testchain.nethereum.com:8545 which is our simple single node Nethereum testchain.
// When providing an Account instantiated with a  private key, all our transactions will be signed by Nethereum.

				var url = ""http://testchain.nethereum.com:8545"";
				var privateKey = ""0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7"";
				var account = new Account(privateKey);
				var web3 = new Web3(account, url);

// **** DEPLOYING THE SMART CONTRACT
// The next step is to deploy our Standard Token ERC20 smart contract, 
//in this scenario the total supply (number of tokens) is going to be 100,000.

// First we create an instance of the StandardTokenDeployment with the TotalSupply amount.

				var deploymentMessage = new StandardTokenDeployment
				{
						TotalSupply = 100000
				};

// Then we create a deployment handler using our contract deployment definition and simply deploy the contract 
// using the deployment message. 
// We are auto estimating the gas, getting the latest gas price and nonce so nothing else is set on the deployment message.
// Finally, we wait for the deployment transaction to be mined, 
// and retrieve the contract address of the new contract from the receipt.

				var deploymentHandler = web3.Eth.GetContractDeploymentHandler<StandardTokenDeployment>();
				var transactionReceiptDeployment = await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage);
				var contractAddress = transactionReceiptDeployment.ContractAddress;
				Console.WriteLine(""Smart contract deployed at address:"" + contractAddress);

// *** INTERACTING WITH THE CONTRACT

// #### QUERING


// To retrieve the balance, we will create a QueryHandler and finally using our contract address 
// and message retrieve the balance amount.

					var balanceOfFunctionMessage = new BalanceOfFunction()
					{
							Owner = account.Address,
					};

					var balanceHandler = web3.Eth.GetContractQueryHandler<BalanceOfFunction>();

					var balance = await balanceHandler.QueryAsync<BigInteger>(contractAddress, balanceOfFunctionMessage);

					Console.WriteLine(""Balance of deployment owner address: "" + balance);

// When Quering retrieving multiple results, we can use this method instead


					var balanceOutput = await balanceHandler.QueryDeserializingToObjectAsync<BalanceOfOutputDTO>( balanceOfFunctionMessage, contractAddress);




// #### Transfer
// Making a transfer will change the state of the blockchain, 
// so in this scenario we will need to create a TransactionHandler using the TransferFunction definition.

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

					var transactionTransferReceipt = await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, transfer);
					Console.WriteLine(""Transaction hash transfer is: ""+transactionTransferReceipt.TransactionHash);

					balance = await balanceHandler.QueryAsync<BigInteger>(contractAddress, balanceOfFunctionMessage);

					Console.WriteLine(""Balance of deployment owner address after transfer: "" + balance);

// #### Querying previous state of the smart contract

// Another great feature of the Ethereum blockchain is the capability to retrieve the state 
// of a smart contract from a previous block.

// For example, we could get the balance of the owner at the time of deployment by using the block number 
// in which the contract was deployed we will get the 10000


				balanceOutput = await balanceHandler.QueryDeserializingToObjectAsync<BalanceOfOutputDTO>( balanceOfFunctionMessage, contractAddress,
											 new Nethereum.RPC.Eth.DTOs.BlockParameter(transactionReceiptDeployment.BlockNumber));

				Console.WriteLine(""Balance of deployment owner address from previous Block Number: "" + transactionReceiptDeployment.BlockNumber + "" is: "" + balanceOutput.Balance);
				

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

				Console.WriteLine(""SignedTransaction is: ""+signedTransaction1);

// Nethereum internally calls the Ethereum client to set the GasPrice, Nonce and estimate the Gas, 
// so if we want to sign the transaction for the contract completely offline we will need to set those values before hand.


				transfer.Nonce = 2;

				transfer.Gas = 21000;

				transfer.GasPrice =  Nethereum.Web3.Web3.Convert.ToWei(25, UnitConversion.EthUnit.Gwei);

				var signedTransaction2 = await transferHandler.SignTransactionAsync(contractAddress, transfer);

				Console.WriteLine("" Full offline (no need for node) Signed Transaction (providing manually the nonce, gas and gas price) is: "" + signedTransaction2);

    }

}






"

                },

                 new CodeSample()
                {
                    Name = "Smart Contracts: Events",
                    Code = @"				
using System;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Threading;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Util;
using Nethereum.Contracts;
using Nethereum.Contracts.Extensions;
using Nethereum.Contracts.CQS;
using System.Numerics;

public class GettingStarted_Events
{

						// Events in smart contracts write data to the transaction receipt logs, providing a way to get extra information
						//about a smart contract transactions.

						// A very good example is the “Transfer” event in the ERC20 Standard Token contract.
						//Everytime that a token transfer has ocurred, an event gets logged providing information of the “sender”,
						// “receiver” and the token amount. In this scenario we are only interested
						// in the Deployment, Transfer function and Transfer Event of the ERC20 smart contract.
						//
						//_____________________________________________________________________________________________________
						//
						// event Transfer(address indexed _from, address indexed _to, uint256 _value);
						//
						// function transfer(address _to, uint256 _value) public returns (bool success) {
						//     require(balances[msg.sender] >= _value, ""Balance amount lower than amount requested"");
						//     balances[msg.sender] -= _value;
						//     balances[_to] += _value;
						//     emit Transfer(msg.sender, _to, _value);
						//     return true;
						//}
						//_____________________________________________________________________________________________________
						//
						// 
						// Above we can see, the event declaration with the different indexed parameters, these will allow us
						// later on to “filter” for specific events.
						// For example ,“Transfer” events for a specific receiver address “\_to”.

						// The Transfer event can be seen in the function prefixed with the “emit” keyword.



						// To deploy a contract we will create a class inheriting from the ContractDeploymentMessage,
						// here we can include our compiled byte code and other constructor parameters.

						// As we can see below the StandardToken deployment message includes the compiled bytecode of the ERC20
						// smart contract and the constructor parameter with the “totalSupply” of tokens.

						// Each parameter is described with an attribute Parameter, including its name
						// ""totalSupply"", type ""uint256"" and order.

						///**** START CONTRACT DEFINITION
						public class StandardTokenDeployment : ContractDeploymentMessage
						{

								public static string BYTECODE = ""0x60606040526040516020806106f5833981016040528080519060200190919050505b80600160005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005081905550806000600050819055505b506106868061006f6000396000f360606040523615610074576000357c010000000000000000000000000000000000000000000000000000000090048063095ea7b31461008157806318160ddd146100b657806323b872dd146100d957806370a0823114610117578063a9059cbb14610143578063dd62ed3e1461017857610074565b61007f5b610002565b565b005b6100a060048080359060200190919080359060200190919050506101ad565b6040518082815260200191505060405180910390f35b6100c36004805050610674565b6040518082815260200191505060405180910390f35b6101016004808035906020019091908035906020019091908035906020019091905050610281565b6040518082815260200191505060405180910390f35b61012d600480803590602001909190505061048d565b6040518082815260200191505060405180910390f35b61016260048080359060200190919080359060200190919050506104cb565b6040518082815260200191505060405180910390f35b610197600480803590602001909190803590602001909190505061060b565b6040518082815260200191505060405180910390f35b600081600260005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060008573ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050819055508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925846040518082815260200191505060405180910390a36001905061027b565b92915050565b600081600160005060008673ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050541015801561031b575081600260005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060003373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000505410155b80156103275750600082115b1561047c5781600160005060008573ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505401925050819055508273ffffffffffffffffffffffffffffffffffffffff168473ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef846040518082815260200191505060405180910390a381600160005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060008282825054039250508190555081600260005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060003373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505403925050819055506001905061048656610485565b60009050610486565b5b9392505050565b6000600160005060008373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000505490506104c6565b919050565b600081600160005060003373ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050541015801561050c5750600082115b156105fb5781600160005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060008282825054039250508190555081600160005060008573ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505401925050819055508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef846040518082815260200191505060405180910390a36001905061060556610604565b60009050610605565b5b92915050565b6000600260005060008473ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060008373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005054905061066e565b92915050565b60006000600050549050610683565b9056"";

								public StandardTokenDeployment() : base(BYTECODE){}

								[Parameter(""uint256"", ""totalSupply"")]
								public BigInteger TotalSupply { get; set; }
						}


						// We can call the functions of smart contract to query the state of a smart contract or do any computation,
						// which will not affect the state of the blockchain.

						// To do so we will need to create a class which inherits from ""FunctionMessage"".
						// First we will decorate the class with a ""Function"" attribute, including the name and return type.

						// Each parameter of the the function will be a property of the class, each of them decorated with the
						// ""Parameter"" attribute, including the smart contract name, type and parameter order.

						// For the ERC20 smart contract, the ""balanceOf"" function definition, provides the query interface
						// to get the token balance of a given address. As we can see this function includes only one parameter
						// ""\_owner"", of the type ""address"".


						[Function(""balanceOf"", ""uint256"")]
						public class BalanceOfFunction : FunctionMessage
						{
								[Parameter(""address"", ""_owner"", 1)]
								public string Owner { get; set; }
						}


						// Another type of smart contract function will be correspondent to a transaction that will change
						// the state of the smart contract (or smart contracts).

						// For example The ""transfer"" function definition for the ERC20 smart contract, includes the parameters
						// “\_to” address parameter as a string, and the “\_value”
						// or TokenAmount we want to transfer.

						// In a similar way to the ""balanceOf"" function, all the parameters include the solidity type,
						// parameter name and parameter order.

						// Note: When working with functions, it is very important to have the parameters types, and function name
						// correct as all of these make the signature of the function.


						[Function(""transfer"", ""bool"")]
						public class TransferFunction : FunctionMessage
						{
								[Parameter(""address"", ""_to"", 1)]
								public string To { get; set; }

								[Parameter(""uint256"", ""_value"", 2)]
								public BigInteger TokenAmount { get; set; }
						}


						// Finally smart contracts also have events. Events in smart contracts write the blockchain log,
						// providing a way to retrieve further information of any smart contract interaction occurred.

						// To create an Event definition, we need to create a class that inherits from IEventDTO,
						// decorated with the Event attribute.

						// The Transfer Event, similar to the Function it also includes the parameters with the name,
						// order and type. But also a boolean value indicating if the parameter is indexed or not.

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
						
					///**** END CONTRACT DEFINITION
					

					public static async Task Main(string[] args)
					{

						// ### Instantiating Web3 and the Account

						// To create an instance of web3 we first provide the url of our testchain and the private key of our account.
						// When providing an Account instantiated with a  private key all our transactions will be signed
						// “offline” by Nethereum.


						var url = ""http://testchain.nethereum.com:8545"";
						var privateKey = ""0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7"";
						var account = new Account(privateKey);
						var web3 = new Web3(account, url);


						// ### Deploying the Contract

						// The next step is to deploy our Standard Token ERC20 smart contract, in this scenario the total supply
						// (number of tokens) is going to be 100,000.

						// First we create an instance of the StandardTokenDeployment with the TotalSupply amount.


						var deploymentMessage = new StandardTokenDeployment
						{
								TotalSupply = 100000
						};


						// Then we create a deployment handler using our contract deployment definition and simply deploy the
						// contract using the deployment message. We are auto estimating the gas, getting the latest gas price
						// and nonce so nothing else is set anything on the deployment message.

						// Finally, we wait for the deployment transaction to be mined, and retrieve the contract address of
						// the new contract from the receipt.


						var deploymentHandler = web3.Eth.GetContractDeploymentHandler<StandardTokenDeployment>();
						var transactionReceipt = await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage);
						var contractAddress = transactionReceipt.ContractAddress;
						Console.WriteLine(""contractAddress is: "" + contractAddress);

						// ### Transfer

						// Once we have deployed the contract, we can execute our first transfer transaction.
						// The transfer function will write to the log the transfer event.

						// First we can create a TransactionHandler using the TrasferFunction definition and a
						// TransferFunction message including the “receiverAddress” and the amount of tokens we want to send.

						// Finally do the transaction transfer and wait for the receipt to be “mined”
						// and included in the blockchain.


						var receiverAddress = ""0xde0B295669a9FD93d5F28D9Ec85E40f4cb697BAe"";
						var transferHandler = web3.Eth.GetContractTransactionHandler<TransferFunction>();
						var transfer = new TransferFunction()
						{
								To = receiverAddress,
								TokenAmount = 100
						};
						var transactionReceipt2 = await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, transfer);


						// ## Decoding the Event from the TransactionReceipt

						// Event logs are part of the TransactionReceipts, so using the Transaction receipt from the previous
						// transfer we can decode the TransferEvent using the extension method
						// “DecodeAllEvents<TransferEventDTO>()”.

						// Note that this method returns an array of Decoded Transfer Events as opposed to a single value,
						// because the receipt can include more than one event of the same signature.


						var transferEventOutput = transactionReceipt2.DecodeAllEvents<TransferEventDTO>();


						// ## Contract Filters and Event Logs

						// Another way to access the event logs of a smart contract is to either get all changes of the logs
						// (providing a filter message) or create filters and retrieve changes which apply to our filter message
						// periodically.                                  \
						// \
						// To access the logs, first of all, we need to create a transfer event handler for our contract address,
						// and Evend definition.(TransferEventDTO).


						var transferEventHandler = web3.Eth.GetEvent<TransferEventDTO>(contractAddress);


						// Using the event handler, we can create a filter message for our transfer event using the default values.

						// The default values for BlockParameters are Earliest and Latest, so when we retrieve the logs
						// we will get all the transfer events from the first block to the latest block of this contract.


						var filterAllTransferEventsForContract = transferEventHandler.CreateFilterInput();


						// Once we have created the message we can retrieve all the logs using the event and GetAllChanges.
						// In this scenario, because we have made only one transfer, we will have only one Transfer Event.


						var allTransferEventsForContract = await transferEventHandler.GetAllChanges(filterAllTransferEventsForContract);

								Console.WriteLine(""Transfer event TransactionHash : ""+ allTransferEventsForContract[0].Log.TransactionHash);

						// If we now make another Transfer to a different address


						var receiverAddress2 = ""0x3e0B295669a9FD93d5F28D9Ec85E40f4cb697BAe"";
						var transfer2 = new TransferFunction()
						{
								To = receiverAddress2,
								TokenAmount = 1000
						};
						var transactionReceipt3 = await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, transfer2);


						// Using the same filter input message and making another GetAllChanges call, we will now get
						// the two Transfer Event logs.


						var allTransferEventsForContract2 = await transferEventHandler.GetAllChanges(filterAllTransferEventsForContract);

						for (int i = 0; i < 2; i++)
						{
								Console.WriteLine(""Transfer event number : ""+ i +"" - TransactionHash : ""+ allTransferEventsForContract2[(i)].Log.TransactionHash);
						}

						// Filter messages can limit the results (similar to block ranges) to the indexed parameters,
						// for example we can create a filter for only our sender address AND the receiver address.
						// As a reminder our Event has as indexed parameters the “\_from” address
						// and “\_to” address.


						var filterTransferEventsForContractReceiverAddress2 = transferEventHandler.CreateFilterInput(account.Address, receiverAddress2);
						var transferEventsForContractReceiverAddress2 = await transferEventHandler.GetAllChanges(filterTransferEventsForContractReceiverAddress2);


						// The order the filter values is based on the event parameters order, if we want to include all the transfers to the “receiverAddress2”, the account address from will need to be set to null to be ignored.

						// Note: We are using the array format to allow for null input of the first parameter.


						var filterTransferEventsForContractAllReceiverAddress2 = transferEventHandler.CreateFilterInput(null, new []{receiverAddress2});
						var transferEventsForContractAllReceiverAddress2 = await transferEventHandler.GetAllChanges(filterTransferEventsForContractAllReceiverAddress2);


						// Another scenario is when you want to include multiple indexed values, for example transfers for
						// “receiverAddress1” OR “receiverAddress2”.
						// Then you will need to use an array of the values you are interested.


						var filterTransferEventsForContractAllReceiverAddresses = transferEventHandler.CreateFilterInput(null, new []{receiverAddress2, receiverAddress});
						var transferEventsForContractAllReceiverAddresses = await transferEventHandler.GetAllChanges(filterTransferEventsForContractAllReceiverAddresses);


						// ### Creating filters to retrieve periodic changes

						// Another option is to create filters that return only the changes occurred since we got the previous results.
						// This eliminates the need of tracking the last block the events were checked and delegate this
						// to the Ethereum client.

						// Using the same filter message we created before we can create the filter and get the filterId.


						var filterIdTransferEventsForContractAllReceiverAddress2  = await transferEventHandler.CreateFilterAsync(filterTransferEventsForContractAllReceiverAddress2);


						// One thing to note, if  try to get the filter changes now, we will not get any results because
						// the filter only returns the changes since creation.


						var result = await transferEventHandler.GetFilterChanges(filterIdTransferEventsForContractAllReceiverAddress2);


						// But, if we make another transfer using the same values


						await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, transfer2);


						// and execute get filter changes using the same filter id, we will get the event for the previous transfer.


						var result2 = await transferEventHandler.GetFilterChanges(filterIdTransferEventsForContractAllReceiverAddress2);
						Console.WriteLine(""result2/TransactionHash: "" + result2[0].Log.TransactionHash);


						// Executing the same again will return no results because no new transfers have occurred
						// since the last execution of GetFilterChanges.


						var result3 = await transferEventHandler.GetFilterChanges(filterIdTransferEventsForContractAllReceiverAddress2);

						Console.WriteLine(""result3/TransactionHash: "" + result3);

						// ## Events for all Contracts

						// Different contracts can have and raise/log the same event with the same signature,
						// a simple example is the multiple standard token ERC20 smart contracts that are part of Ethereum.
						// There might be scenarios you want to capture all the Events for different contracts using a specific filter,
						// for example all the transfers to an address.

						// In Nethereum creating an Event (handler) without a contract address allows to create filters
						// which are not attached to a specific contract.


						var transferEventHandlerAnyContract = web3.Eth.GetEvent<TransferEventDTO>();


						// There is already a contract deployed in the chain, from the previous sample,
						// so to demonstrate the access to events of multiple contracts we can deploy another standard token contract.


						var transactionReceiptNewContract = await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage);
						var contractAddress2 = transactionReceiptNewContract.ContractAddress;


						// and make another transfer using this new contract and the same receiver address.


						await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress2, transfer);


						// Creating a default filter input, and getting all changes, will retrieve all the transfer events
						// for all contracts.


						var filterAllTransferEventsForAllContracts = transferEventHandlerAnyContract.CreateFilterInput();
						var allTransferEventsForContract3 = await transferEventHandlerAnyContract.GetAllChanges(filterAllTransferEventsForAllContracts);


						// If we want to retrieve only all the transfers to the “receiverAddress”,
						// we can create the same filter as before ,including only the second indexed parameter (“to”). This will return the Transfers only to this address for both contracts.


						var filterTransferEventsForAllContractsReceiverAddress2 = transferEventHandlerAnyContract.CreateFilterInput(null, new[]{receiverAddress});
						var result4 = await transferEventHandlerAnyContract.GetAllChanges(filterTransferEventsForAllContractsReceiverAddress2);


						for (int i = 0; i < 2; i++)
						{
								Console.WriteLine(""Transfer event number : ""+ i +"" - TransactionHash : ""+ result4[(i)].Log.TransactionHash);
						}

    }

}					
	
"},
                new CodeSample()
                {
                    Name = "Smart Contracts: Estimating Gas",
                    Code = @"

using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Contracts.CQS;
using Nethereum.Util;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Contracts.Extensions;

        //# Estimating the cost of a transaction with Nethereum

        //Documentation about Nethereum can be found at: <https://docs.nethereum.com>

        //The purpose of this sample is to estimate the gas cost of a simple transaction and modify the assigned 
        //values of `gas` and `gasprice`. To do so, we'll use a sample based on deploying a token contract and 
        // performing transactions with this contract.

        //## Ethereum and Gas: a primer

        //Gas is the pricing system used for running a transaction or contract in Ethereum.
        //The gas system is not very different from the use of kW-h for measuring electricity home use. 
        //One difference from actual energy market is that the originator of the transaction sets the price of gas,
        //which the miner can accept or not, this causes an emergence of a market around gas. You can see the evolution
        //of the price of gas at: <https://etherscan.io/chart/gasprice>.

        //The gas price per transaction or per contract is set up to deal with the Turing Complete nature of Ethereum 
        //and its EVM (Ethereum Virtual Machine Code) – the idea is to prevent infinite loops. If there is not enough 
        //Ether in the account to perform the transaction or message then it is considered invalid. The idea is to stop
        //denial of service attacks from infinite loops, encourage efficiency in the code – and to make an attacker pay 
        //for the resources they use, from bandwidth through to CPU calculations through to storage.

        //Here are the terms needed to define the **gas** cost of a transaction:

        //* **Gas limit** refers to the maximum amount of gas you’re willing to spend on a particular transaction.

        //* **Gas price** refers to the amount of Ether you’re willing to pay for every unit of gas, and is usually
        //measured in “Gwei”.

        //It would be difficult to send transaction without an idea of their cost in gas, fortunately Ethereum provides
        //ways to obtain a gas estimate prior to sending a transaction.

        //The following article explains how to anticipate the cost of an unsent transaction by returning an 
        //estimation.

        //##### A word of caution

        //Because of the Turing completeness of the EVM, it is easy to write functions that will take different code 
        //paths with wildly different gas costs. For example, a function could choose to take different code paths 
        //according to the value of some global state variable. The real code path taken in the function is not known 
        //until transaction execution time. Therefore the gas estimate can only give an approximation of the actual 
        //cost of a transaction.

public class GettingStarted_EstimatingGas
{
// Let's start by deploying a contract to which we'll send transactions. We do this by creating a class 
// inheriting from the ContractDeploymentMessage, 
// here we can include our compiled byte code and other constructor parameters.
// As we can see below the StandardToken deployment message includes the compiled bytecode 
// of the ERC20 smart contract and the constructor parameter with the “totalSupply” of tokens.
// Each parameter is described with an attribute Parameter, including its name ""totalSupply"", type ""uint256"" and order.

    public class StandardTokenDeployment : ContractDeploymentMessage
    {

                public static string BYTECODE = ""0x60606040526040516020806106f5833981016040528080519060200190919050505b80600160005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005081905550806000600050819055505b506106868061006f6000396000f360606040523615610074576000357c010000000000000000000000000000000000000000000000000000000090048063095ea7b31461008157806318160ddd146100b657806323b872dd146100d957806370a0823114610117578063a9059cbb14610143578063dd62ed3e1461017857610074565b61007f5b610002565b565b005b6100a060048080359060200190919080359060200190919050506101ad565b6040518082815260200191505060405180910390f35b6100c36004805050610674565b6040518082815260200191505060405180910390f35b6101016004808035906020019091908035906020019091908035906020019091905050610281565b6040518082815260200191505060405180910390f35b61012d600480803590602001909190505061048d565b6040518082815260200191505060405180910390f35b61016260048080359060200190919080359060200190919050506104cb565b6040518082815260200191505060405180910390f35b610197600480803590602001909190803590602001909190505061060b565b6040518082815260200191505060405180910390f35b600081600260005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060008573ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050819055508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925846040518082815260200191505060405180910390a36001905061027b565b92915050565b600081600160005060008673ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050541015801561031b575081600260005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060003373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000505410155b80156103275750600082115b1561047c5781600160005060008573ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505401925050819055508273ffffffffffffffffffffffffffffffffffffffff168473ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef846040518082815260200191505060405180910390a381600160005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060008282825054039250508190555081600260005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060003373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505403925050819055506001905061048656610485565b60009050610486565b5b9392505050565b6000600160005060008373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000505490506104c6565b919050565b600081600160005060003373ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050541015801561050c5750600082115b156105fb5781600160005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060008282825054039250508190555081600160005060008573ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505401925050819055508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef846040518082815260200191505060405180910390a36001905061060556610604565b60009050610605565b5b92915050565b6000600260005060008473ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060008373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005054905061066e565b92915050565b60006000600050549050610683565b9056"";

        public StandardTokenDeployment() : base(BYTECODE){}

        [Parameter(""uint256"", ""totalSupply"")]
        public BigInteger TotalSupply { get; set; }
    }
    // We now create the classes and function that will allow us to interact with the deployed contract

    [Function(""transfer"", ""bool"")]
    public class TransferFunction : FunctionMessage
    {
        [Parameter(""address"", ""_to"", 1)]
        public string To { get; set; }

        [Parameter(""uint256"", ""_value"", 2)]
        public BigInteger TokenAmount { get; set; }
    }
        
    public static async Task Main(string[] args)
    {
    //  Let's create the deployment message 
        var deploymentMessage = new StandardTokenDeployment
        {
            TotalSupply = 100000
        };


        //### Setting up sender account

        //Let's declare our private key and address as variables (we'll use the address to send the transaction) and 
        //use them to create a new account:

        var privateKey = ""0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7"";
        var account = new Nethereum.Web3.Accounts.Account(privateKey);


        //### Web3

        //Web3 provides a simple interaction wrapper with Ethereum clients. To create an instance of Web3, we need to 
        //supply our Account and the RPC uri of the Ethereum client. In this scenario we will only use the Account, as 
        //we will be interacting with Nethereum's testchain on the default RPC uri 
        //""http://testchain.nethereum.com:8545"" 

        var web3 = new Web3(account, ""http://testchain.nethereum.com:8545"");
        var deploymentHandler = web3.Eth.GetContractDeploymentHandler<StandardTokenDeployment>();
        var transactionReceipt1 = await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage);
        string contractAddress = transactionReceipt1.ContractAddress;
        Console.WriteLine($""Contract address: {contractAddress}."");
        Console.WriteLine($""transactionReceipt1's TransactionHash: {transactionReceipt1.TransactionHash}."");

        //## Transfering token

        //Making a transfer will change the state of the blockchain, so in this scenario we will need to create 
        //a TransactionHandler using the TransferFunction definition.

        //In the transfer message, we will include the receiver address `To`, and the `TokenAmount` to transfer.

        //The final step is to Send the request, wait for the receipt to be “mined” and included in the blockchain.


        var receiverAddress = ""0xde0B295669a9FD93d5F28D9Ec85E40f4cb697BAe"";
        var transferHandler = web3.Eth.GetContractTransactionHandler<TransferFunction>();
        var transfer = new TransferFunction()
        {
            To = receiverAddress,
            TokenAmount = 100
        };
        var transactionReceipt2 = await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, transfer);
        Console.WriteLine($""transactionReceipt2's TransactionHash: {transactionReceipt2.TransactionHash}."");

        //### Gas Price

        //Nethereum automatically sets the GasPrice if not provided by using the clients ""GasPrice"" call, which 
        //provides the average gas price from previous blocks.

        //If you want to have more control over the GasPrice these can be set in both `FunctionMessages` and 
        //`DeploymentMessages`.

        //The GasPrice is set in ""Wei"" which is the lowest unit in Ethereum, so if we are used to the usual ""Gwei"" 
        //units, this will need to be converted using the Nethereum Conversion utilities.


        transfer.GasPrice =  Nethereum.Web3.Web3.Convert.ToWei(25, UnitConversion.EthUnit.Gwei);
        Console.WriteLine($""transfer.GasPrice set for transaction: {transfer.GasPrice}."");


        //### Estimating Gas

        //Nethereum does an automatic estimation of the total gas necessary to make the function transaction by calling
        // the `EthEstimateGas` internally with the ""CallInput"".

        //If needed, this can be done manually, using the TransactionHandler and the ""transfer"" transaction 
        //FunctionMessage.


        var estimate = await transferHandler.EstimateGasAsync(contractAddress, transfer);
        Console.WriteLine($""Future transaction gas estimate: {estimate}."");
         transfer.Gas = estimate.Value;


        //Now the transaction will have the correct amount of `gas` at the right `gasprice`:


        var transactionReceipt3 = await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, transfer);
        var receiptHash = transactionReceipt3.TransactionHash;
        Console.WriteLine($""transactionReceipt3's TransactionHash: {transactionReceipt3.TransactionHash}."");
    }

}
"
                },

                new CodeSample()
                {
                    Name = "Smart contracts: Signing offline Function / Contract Deployment messages",
                    Code = @"

using Nethereum.Web3;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.CQS;
using Nethereum.Util;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Contracts;
using Nethereum.Contracts.Extensions;
using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;


public class FunctionMessage_Signing
{


//********* CONTRACT DEFINITION  *******

		[Function(""transfer"", ""bool"")]
		public class TransferFunction : FunctionMessage
		{
				[Parameter(""address"", ""_to"", 1)]
				public string To { get; set; }

				[Parameter(""uint256"", ""_value"", 2)]
				public BigInteger TokenAmount { get; set; }

		}



//**** END CONTRACT DEFINITIONS ***** ///

///*** THE MAIN PROGRAM ***
		public static async Task Main()
		{    

// ### Instantiating Web3 and the Account
// To create an instance of web3 we first provide the url of our testchain and the private key of our account. 
// Here we are using http://testchain.nethereum.com:8545 which is our simple single node Nethereum testchain.
// When providing an Account instantiated with a  private key, all our transactions will be signed by Nethereum.

				var url = ""http://testchain.nethereum.com:8545"";
				var privateKey = ""0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7"";
				var account = new Account(privateKey);
				var web3 = new Web3(account, url);

// ### Signing a Function / Deployment message online / offline
				var contractAddress = ""0xde0B295669a9FD93d5F28D9Ec85E40f4cb697BAe"";
				var receiverAddress = ""0x1245695669a9FD93d5F28D9Ec85E40f4cb697BAe"";
				var transferHandler = web3.Eth.GetContractTransactionHandler<TransferFunction>();

				var transfer = new TransferFunction()
				{
							To = receiverAddress,
							TokenAmount = 100
				};

				//If we don't provide the nonce, gas, etc Nethereum needs to connect to a node retrieve the information, 
				//so signing is not fully offline
				var signedTransaction1 = await transferHandler.SignTransactionAsync(contractAddress, transfer);

				Console.WriteLine(""Signed Transaction using node to estimate gas, gas price and get nonce: "" + signedTransaction1);

				// Nethereum internally calls the Ethereum client to set the GasPrice, Nonce and estimate the Gas, 
				// so if we want to sign the transaction for the contract completely offline we will need to set those values before hand.

				transfer.Nonce = 2;

				transfer.Gas = 21000;

				transfer.GasPrice =  Nethereum.Web3.Web3.Convert.ToWei(25, UnitConversion.EthUnit.Gwei);

				var signedTransaction2 = await transferHandler.SignTransactionAsync(contractAddress, transfer);

				Console.WriteLine(""Signed transaction Fully offline (no need for node, providing manually the nonce, gas and gas price) is: "" + signedTransaction2);

    }

}




"
                },

                new CodeSample()
                {
                    Name = "Smart contracts: Working with Structs",
                    Code = @"

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.ABI.Model;
using Nethereum.Contracts;
using Nethereum.Web3.Accounts;
using Nethereum.Web3;
using Nethereum.Hex.HexConvertors.Extensions;


public class SmartContracts_Structs
{

//This sample demonstrates how to interact with solidity Structs in Nethereum

/*

pragma solidity ^0.5.8;
pragma experimental ABIEncoderV2;

contract StructsSamplePO
{
        mapping(uint => PurchaseOrder) purchaseOrders;
        event PurchaseOrderChanged(address sender, PurchaseOrder purchaseOrder);
        event PurchaseOrdersChanged(PurchaseOrder[] purchaseOrder);
        event LineItemsAdded(address sender, uint purchaseOrderId, LineItem[] lineItem);

        struct PurchaseOrder {
            uint256 id;
            LineItem[] lineItem;
            uint256 customerId;
        }

        struct LineItem {
            uint256 id;
            uint256 productId;
            uint256 quantity;
            string description;
        }

        function SetPurchaseOrder(PurchaseOrder memory purchaseOrder) public {
            PurchaseOrder storage purchaseOrderTemp = purchaseOrders[purchaseOrder.id];
            purchaseOrderTemp.id = purchaseOrder.id;

            purchaseOrderTemp.customerId = purchaseOrder.customerId;
            for (uint x = 0; x < purchaseOrder.lineItem.length; x++)
            {
                purchaseOrderTemp.lineItem.push(purchaseOrder.lineItem[x]);
            }
            emit PurchaseOrderChanged(msg.sender, purchaseOrder);
        }

        function SetPurchaseOrders(PurchaseOrder[] memory purchaseOrder) public {
            for (uint i = 0; i < purchaseOrder.length; i ++)
            {
                SetPurchaseOrder(purchaseOrder[i]);
            }
             emit PurchaseOrdersChanged(purchaseOrder);
        }

        function GetPurchaseOrder(uint id) public view returns (PurchaseOrder memory purchaseOrder) {
           return purchaseOrders[id];
        }

        function AddLineItems(uint id, LineItem[] memory lineItem) public {
            for (uint x = 0; x < lineItem.length; x++)
            {
                purchaseOrders[id].lineItem.push(lineItem[x]);
            }
            emit LineItemsAdded(msg.sender, id, lineItem);
            emit PurchaseOrderChanged(msg.sender, purchaseOrders[id]);
        }
}
*/

//********* CONTRACT DEFINITION  *******

//***Structs just contain the Parameter attributes, same as Function Messages, Events or Deployment messages***//
  public class PurchaseOrder
    {
        [Parameter(""uint256"", ""id"", 1)]
        public virtual BigInteger Id { get; set; }
        [Parameter(""tuple[]"", ""lineItem"", 2)]
        public virtual List<LineItem> LineItem { get; set; }
        [Parameter(""uint256"", ""customerId"", 3)]
        public virtual BigInteger CustomerId { get; set; }
    }

	 public class LineItem 
    {
        [Parameter(""uint256"", ""id"", 1)]
        public virtual BigInteger Id { get; set; }
        [Parameter(""uint256"", ""productId"", 2)]
        public virtual BigInteger ProductId { get; set; }
        [Parameter(""uint256"", ""quantity"", 3)]
        public virtual BigInteger Quantity { get; set; }
        [Parameter(""string"", ""description"", 4)]
        public virtual string Description { get; set; }
    }

//*** Deployment message**** //

	 public class StructsSamplePODeployment : ContractDeploymentMessage
    {
        public static string BYTECODE = ""608060405234801561001057600080fd5b50610e96806100206000396000f3fe608060405234801561001057600080fd5b506004361061004c5760003560e01c80631a9542af14610051578063815c844d14610066578063cc0b4b021461008f578063f79eb4a2146100a2575b600080fd5b61006461005f3660046107a7565b6100b5565b005b610079610074366004610819565b610120565b6040516100869190610d16565b60405180910390f35b61006461009d366004610837565b610273565b6100646100b03660046107e4565b610391565b60005b81518110156100e5576100dd8282815181106100d057fe5b6020026020010151610391565b6001016100b8565b507f63d0df058c364c605130a4550879b03d3814f0ba56c550569be936f3c2d7a2f5816040516101159190610d05565b60405180910390a150565b61012861046b565b6000828152602081815260408083208151606081018352815481526001820180548451818702810187019095528085529195929486810194939192919084015b8282101561025a5783829060005260206000209060040201604051806080016040529081600082015481526020016001820154815260200160028201548152602001600382018054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156102425780601f1061021757610100808354040283529160200191610242565b820191906000526020600020905b81548152906001019060200180831161022557829003601f168201915b50505050508152505081526020019060010190610168565b5050505081526020016002820154815250509050919050565b60005b8151811015610307576000808481526020019081526020016000206001018282815181106102a057fe5b60209081029190910181015182546001818101808655600095865294849020835160049093020191825582840151908201556040820151600282015560608201518051929391926102f7926003850192019061048c565b5050600190920191506102769050565b507f13fdaebbac9da33d495b4bd32c83e33786a010730713d20c5a8ef70ca576be6533838360405161033b93929190610cd8565b60405180910390a17f88ab28750130223a530a1325799e7ef636cd4c7a60d350c38c45316082fdbbf833600080858152602001908152602001600020604051610385929190610cb8565b60405180910390a15050565b805160009081526020819052604080822083518155908301516002820155905b8260200151518110156104395781600101836020015182815181106103d257fe5b6020908102919091018101518254600181810180865560009586529484902083516004909302019182558284015190820155604082015160028201556060820151805192939192610429926003850192019061048c565b5050600190920191506103b19050565b507f88ab28750130223a530a1325799e7ef636cd4c7a60d350c38c45316082fdbbf83383604051610385929190610c98565b60405180606001604052806000815260200160608152602001600081525090565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f106104cd57805160ff19168380011785556104fa565b828001600101855582156104fa579182015b828111156104fa5782518255916020019190600101906104df565b5061050692915061050a565b5090565b61052491905b808211156105065760008155600101610510565b90565b600082601f83011261053857600080fd5b813561054b61054682610d4e565b610d27565b81815260209384019390925082018360005b838110156105895781358601610573888261069c565b845250602092830192919091019060010161055d565b5050505092915050565b600082601f8301126105a457600080fd5b81356105b261054682610d4e565b81815260209384019390925082018360005b8381101561058957813586016105da888261069c565b84525060209283019291909101906001016105c4565b600082601f83011261060157600080fd5b813561060f61054682610d4e565b81815260209384019390925082018360005b8381101561058957813586016106378882610723565b8452506020928301929190910190600101610621565b600082601f83011261065e57600080fd5b813561066c61054682610d6f565b9150808252602083016020830185838301111561068857600080fd5b610693838284610de3565b50505092915050565b6000608082840312156106ae57600080fd5b6106b86080610d27565b905060006106c68484610796565b82525060206106d784848301610796565b60208301525060406106eb84828501610796565b604083015250606082013567ffffffffffffffff81111561070b57600080fd5b6107178482850161064d565b60608301525092915050565b60006060828403121561073557600080fd5b61073f6060610d27565b9050600061074d8484610796565b825250602082013567ffffffffffffffff81111561076a57600080fd5b61077684828501610527565b602083015250604061078a84828501610796565b60408301525092915050565b80356107a181610e3c565b92915050565b6000602082840312156107b957600080fd5b813567ffffffffffffffff8111156107d057600080fd5b6107dc848285016105f0565b949350505050565b6000602082840312156107f657600080fd5b813567ffffffffffffffff81111561080d57600080fd5b6107dc84828501610723565b60006020828403121561082b57600080fd5b60006107dc8484610796565b6000806040838503121561084a57600080fd5b60006108568585610796565b925050602083013567ffffffffffffffff81111561087357600080fd5b61087f85828601610593565b9150509250929050565b60006108958383610b15565b9392505050565b60006108958383610b70565b60006108958383610be8565b6108bd81610dcc565b82525050565b60006108ce82610da9565b6108d88185610db7565b9350836020820285016108ea85610d97565b8060005b8581101561092457848403895281516109078582610889565b945061091283610d97565b60209a909a01999250506001016108ee565b5091979650505050505050565b600061093c82610da9565b6109468185610db7565b93508360208202850161095885610d97565b8060005b8581101561092457848403895281516109758582610889565b945061098083610d97565b60209a909a019992505060010161095c565b600061099d82610dad565b6109a78185610db7565b9350836020820285016109b985610d9d565b8060005b85811015610924578484038952816109d5858261089c565b94506109e083610db1565b60209a909a01999250506001016109bd565b60006109fd82610da9565b610a078185610db7565b935083602082028501610a1985610d97565b8060005b858110156109245784840389528151610a3685826108a8565b9450610a4183610d97565b60209a909a0199925050600101610a1d565b6000610a5e82610da9565b610a688185610db7565b9350610a78818560208601610def565b610a8181610e32565b9093019392505050565b600081546001811660008114610aa85760018114610ace57610b0d565b607f6002830416610ab98187610db7565b60ff1984168152955050602085019250610b0d565b60028204610adc8187610db7565b9550610ae785610d9d565b60005b82811015610b0657815488820152600190910190602001610aea565b8701945050505b505092915050565b80516000906080840190610b298582610c8f565b506020830151610b3c6020860182610c8f565b506040830151610b4f6040860182610c8f565b5060608301518482036060860152610b678282610a53565b95945050505050565b80546000906080840190610b8381610e1f565b610b8d8682610c8f565b50506001830154610b9d81610e1f565b610baa6020870182610c8f565b50506002830154610bba81610e1f565b610bc76040870182610c8f565b50600384018583036060870152610bde8382610a8b565b9695505050505050565b80516000906060840190610bfc8582610c8f565b5060208301518482036020860152610c148282610931565b9150506040830151610c296040860182610c8f565b509392505050565b80546000906060840190610c4481610e1f565b610c4e8682610c8f565b50600184018583036020870152610c658382610992565b92505060028401549050610c7881610e1f565b610c856040870182610c8f565b5090949350505050565b6108bd81610524565b60408101610ca682856108b4565b81810360208301526107dc8184610be8565b60408101610cc682856108b4565b81810360208301526107dc8184610c31565b60608101610ce682866108b4565b610cf36020830185610c8f565b8181036040830152610b6781846108c3565b6020808252810161089581846109f2565b602080825281016108958184610be8565b60405181810167ffffffffffffffff81118282101715610d4657600080fd5b604052919050565b600067ffffffffffffffff821115610d6557600080fd5b5060209081020190565b600067ffffffffffffffff821115610d8657600080fd5b506020601f91909101601f19160190565b60200190565b60009081526020902090565b5190565b5490565b60040190565b90815260200190565b6001600160a01b031690565b60006107a18260006107a18260006107a182610dc0565b82818337506000910152565b60005b83811015610e0a578181015183820152602001610df2565b83811115610e19576000848401525b50505050565b60006107a1610e2d83610524565b610524565b601f01601f191690565b610e4581610524565b8114610e5057600080fd5b5056fea365627a7a72305820233abbd537dd6f0267ec14bc152db4f17324d7dfe198b50abe244381df453eb46c6578706572696d656e74616cf564736f6c634300050a0040"";
        public StructsSamplePODeployment() : base(BYTECODE) { }
        public StructsSamplePODeployment(string byteCode) : base(byteCode) { }

    }

//*** FUNCTION MESSAGES **** ///

  	[Function(""SetPurchaseOrders"")]
    public class SetPurchaseOrdersFunction : FunctionMessage
    {
        [Parameter(""tuple[]"", ""purchaseOrder"", 1)]
        public virtual List<PurchaseOrder> PurchaseOrder { get; set; }
    }

		[Function(""GetPurchaseOrder"", typeof(GetPurchaseOrderOutputDTO))]
    public class GetPurchaseOrderFunction : FunctionMessage
    {
        [Parameter(""uint256"", ""id"", 1)]
        public virtual BigInteger Id { get; set; }
		}

		[Function(""AddLineItems"")]
    public class AddLineItemsFunction : FunctionMessage
    {
        [Parameter(""uint256"", ""id"", 1)]
        public virtual BigInteger Id { get; set; }
        [Parameter(""tuple[]"", ""lineItem"", 2)]
        public virtual List<LineItem> LineItem { get; set; }
    }

		[Function(""SetPurchaseOrder"")]
    public class SetPurchaseOrderFunction : FunctionMessage
    {
        [Parameter(""tuple"", ""purchaseOrder"", 1)]
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }

		[Event(""PurchaseOrderChanged"")]
    public class PurchaseOrderChangedEventDTO : IEventDTO
    {
        [Parameter(""address"", ""sender"", 1, false )]
        public virtual string Sender { get; set; }
        [Parameter(""tuple"", ""purchaseOrder"", 2, false )]
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }

		[Event(""PurchaseOrdersChanged"")]
    public class PurchaseOrdersChangedEventDTO : IEventDTO
    {
        [Parameter(""tuple[]"", ""purchaseOrder"", 1, false )]
        public virtual List<PurchaseOrder> PurchaseOrder { get; set; }
    }

		[Event(""LineItemsAdded"")]
    public class LineItemsAddedEventDTO : IEventDTO
    {
        [Parameter(""address"", ""sender"", 1, false )]
        public virtual string Sender { get; set; }
        [Parameter(""uint256"", ""purchaseOrderId"", 2, false )]
        public virtual BigInteger PurchaseOrderId { get; set; }
        [Parameter(""tuple[]"", ""lineItem"", 3, false )]
        public virtual List<LineItem> LineItem { get; set; }
    }

		[FunctionOutput]
    public class GetPurchaseOrderOutputDTO : IFunctionOutputDTO 
    {
        [Parameter(""tuple"", ""purchaseOrder"", 1)]
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }

//**** END CONTRACT DEFINITIONS ***** ///

///*** THE MAIN PROGRAM ***
		public static async Task Main()
		{    

// ### Instantiating Web3 and the Account
// To create an instance of web3 we first provide the url of our testchain and the private key of our account. 
// Here we are using http://testchain.nethereum.com:8545 which is our simple single node Nethereum testchain.
// When providing an Account instantiated with a  private key, all our transactions will be signed by Nethereum.

				var url = ""http://testchain.nethereum.com:8545"";
				var privateKey = ""0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7"";
				var account = new Account(privateKey);
				var web3 = new Web3(account, url);

// **** DEPLOYING THE SMART CONTRACT
			 var deploymentReceipt = await web3.Eth.GetContractDeploymentHandler<StructsSamplePODeployment>()
                .SendRequestAndWaitForReceiptAsync();

// *** Creating new Purchase Order 
			 var purchaseOrder = new PurchaseOrder();
            purchaseOrder.CustomerId = 1000;
            purchaseOrder.Id = 1;
            purchaseOrder.LineItem = new List<LineItem>();
            purchaseOrder.LineItem.Add(new LineItem() { Id = 1, ProductId = 100, Quantity = 2, Description = ""hello1"" });
            purchaseOrder.LineItem.Add(new LineItem() { Id = 2, ProductId = 200, Quantity = 3, Description = ""hello2""});

            var contractHandler = web3.Eth.GetContractHandler(deploymentReceipt.ContractAddress);

						var receiptSending = await contractHandler.SendRequestAndWaitForReceiptAsync(new SetPurchaseOrderFunction() { PurchaseOrder = purchaseOrder });


						var eventPurchaseOrder = contractHandler.GetEvent<PurchaseOrderChangedEventDTO>();
            var eventOutputs = eventPurchaseOrder.DecodeAllEventsForEvent(receiptSending.Logs);
            var purchaseOrderResult = eventOutputs[0].Event.PurchaseOrder;

						Console.WriteLine(""Getting PO from Transaction Receipt Event Log:"");
            Console.WriteLine(""PO Id: "" + purchaseOrderResult.Id);
            Console.WriteLine(""Customer Id: "" +  purchaseOrderResult.CustomerId);
            Console.WriteLine(""Line item Id: "" +  purchaseOrderResult.LineItem[0].Id);
            Console.WriteLine(""Line item ProductId: "" + purchaseOrderResult.LineItem[0].ProductId);
            Console.WriteLine(""Line item Quantity: "" + purchaseOrderResult.LineItem[0].Quantity);
            Console.WriteLine(""Line item Description: "" + purchaseOrderResult.LineItem[0].Description);
						
						var query = await contractHandler.QueryDeserializingToObjectAsync<GetPurchaseOrderFunction, GetPurchaseOrderOutputDTO>(new GetPurchaseOrderFunction() { Id = 1 });

            purchaseOrderResult = query.PurchaseOrder;
						Console.WriteLine(""Getting PO from Get Purchase Order Function:"");
            Console.WriteLine(""PO Id: "" + purchaseOrderResult.Id);
            Console.WriteLine(""Customer Id: "" +  purchaseOrderResult.CustomerId);
            Console.WriteLine(""Line item Id: "" +  purchaseOrderResult.LineItem[0].Id);
            Console.WriteLine(""Line item ProductId: "" + purchaseOrderResult.LineItem[0].ProductId);
            Console.WriteLine(""Line item Quantity: "" + purchaseOrderResult.LineItem[0].Quantity);
            Console.WriteLine(""Line item Description: "" + purchaseOrderResult.LineItem[0].Description);


						var lineItems = new List<LineItem>();
            lineItems.Add(new LineItem() { Id = 3, ProductId = 300, Quantity = 2, Description = ""hello3"" });
            lineItems.Add(new LineItem() { Id = 4, ProductId = 400, Quantity = 3, Description = ""hello4"" });

            var lineItemsFunction = new AddLineItemsFunction() { Id = 1, LineItem = lineItems };

					  receiptSending = await contractHandler.SendRequestAndWaitForReceiptAsync(new AddLineItemsFunction() { Id = 1, LineItem = lineItems });

						//Getting line items added from the receipt
            var lineItemsEvent = contractHandler.GetEvent<LineItemsAddedEventDTO>();
            var lineItemsLogs = lineItemsEvent.DecodeAllEventsForEvent(receiptSending.Logs);
						Console.WriteLine(""Line items added:"" + lineItemsLogs.Count);
						//
            query = await contractHandler.QueryDeserializingToObjectAsync<GetPurchaseOrderFunction, GetPurchaseOrderOutputDTO>(new GetPurchaseOrderFunction() { Id = 1 });
            purchaseOrderResult = query.PurchaseOrder;
						Console.WriteLine(""Getting PO from Get Purchase Order Function new Line Item:"");
            Console.WriteLine(""PO Id: "" + purchaseOrderResult.Id);
            Console.WriteLine(""Customer Id: "" +  purchaseOrderResult.CustomerId);
            Console.WriteLine(""Line item Id: "" +  purchaseOrderResult.LineItem[2].Id);
            Console.WriteLine(""Line item ProductId: "" + purchaseOrderResult.LineItem[2].ProductId);
            Console.WriteLine(""Line item Quantity: "" + purchaseOrderResult.LineItem[2].Quantity);
            Console.WriteLine(""Line item Description: "" + purchaseOrderResult.LineItem[2].Description);

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
		var web3 = new Web3(""https://mainnet.infura.io/v3/7238211010344719ad14a89db874158c"");

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
                    Name = "ABI Encoding: Encoding using ABI Values, Parameters and Default values",
                    Code = @"

using System;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Util;
using Nethereum.ABI;

public class AbiEncode_AbiValue_Parameters_Default
{
	public class TestParamsInput
	{
			[Parameter(""string"", 1)]
			public string First { get; set; }
			[Parameter(""int256"", 2)]
			public int Second { get; set; }
			[Parameter(""string"", 3)]
			public string Third { get; set; }
	}

    static void Main(string[] args)
    {
            
		var abiEncode = new ABIEncode();
		var result = abiEncode.GetABIEncoded(new ABIValue(""string"", ""hello""), new ABIValue(""int"", 69),
						new ABIValue(""string"", ""world"")).ToHex();

		Console.WriteLine(""Encoded hello, 69 and world using ABIValue: "" + result);


		result = abiEncode.GetABIEncoded(""1"", ""2"", ""3"").ToHex();

		Console.WriteLine(""Encoded 1, 2, 3 strings using  default convertor: "" + result);

		result = abiEncode.GetABIParamsEncoded(new TestParamsInput(){First = ""hello"", Second = 69, Third = ""world""}).ToHex();
		
		Console.WriteLine(""Encoded hello, 69 and world using Parameter attributes: "" + result);
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
"
                },

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
                    Name = "Key Store: Create Scrypt based KeyStore using custom params",
                    Code = @"

using System;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.KeyStore.Model;
public class Program
{

    static void Main(string[] args)
    {

				var keyStoreService = new Nethereum.KeyStore.KeyStoreScryptService();
				// lower cost than default N == 262144 as this is using wasm, the lower the easier to compute but also easier to crack 
				var scryptParams = new ScryptParams {Dklen = 32, N = 32, R = 1, P = 8};
				var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
				Console.WriteLine(""Private key:"" + ecKey.GetPrivateKey());
				var password = ""testPassword"";
				// encrypting using our custome scrypt params
				var keyStore = keyStoreService.EncryptAndGenerateKeyStore(password, ecKey.GetPrivateKeyAsBytes(), ecKey.GetPublicAddress(), scryptParams);
				var json = keyStoreService.SerializeKeyStoreToJson(keyStore);
				Console.WriteLine(json);
				//decrypting our key
				var key = keyStoreService.DecryptKeyStoreFromJson(password, json);
				Console.WriteLine(""Private key decrypted:"" + key.ToHex(true));

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
}"
                },

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


}"
                },

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
"
                },

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
"
                },

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
"
                },

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
"
                },

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
"
                },

            };

            foreach(var sample in samples)
            {
                sample.Language = CodeLanguage.CSharp;
            }

            return samples;
        }
    }
}
