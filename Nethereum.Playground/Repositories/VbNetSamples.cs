using System.Collections.Generic;

namespace Nethereum.Playground.Repositories
{
    public class VbNetSamples
    {
        public static List<CodeSample> GetSamples()
        {
            var samples = new List<CodeSample>
            {
                new CodeSample()
                {
                    Name="Chain information: Query ether account balance using Infura",
                    Code=
@"Imports System
Imports System.Text
Imports Nethereum.Hex.HexConvertors.Extensions
Imports System.Threading.Tasks
Imports Nethereum.Web3


Module Program
    Sub Main()
        'our entrypoint is RunAsync
    End Sub

    Public Async Function RunAsync() As Task
        ' This sample shows how to connect to Ethereum mainnet using Infura
        ' and check an account balance:

        ' We first need to generate an instance of web3, using INFURA's mainnet url and 
        ' our API key.
        ' For this sample, we’ll use a special API key `7238211010344719ad14a89db874158c`,
        ' but for your own project you’ll need your own key.
        Dim web3 = New Web3(""https://mainnet.infura.io/v3/7238211010344719ad14a89db874158c"")

		' Check the balance of one of the accounts provisioned in our chain, to do that, 
		' we can execute the GetBalance request asynchronously:
        Dim balance = Await web3.Eth.GetBalance.SendRequestAsync(""0xde0b295669a9fd93d5f28d9ec85e40f4cb697bae"")
        Console.WriteLine(""Balance of Ethereum Foundation's account: "" & balance.Value.ToString)
    End Function
End Module"
                },

                new CodeSample()
                {
                    Name="Chain information: Get block number, block, transaction and receipt using Infura",
                    Code=
@"Imports System
Imports System.Text
Imports Nethereum.Hex.HexConvertors.Extensions
Imports System.Threading.Tasks
Imports Nethereum.Web3
Imports Nethereum.RPC.Eth.Blocks
Imports Nethereum.Hex.HexTypes

Module EthRpcCalls_BlockNumber_Block_Transaction_Receipt
    Sub Main()
        'our entrypoint is RunAsync
    End Sub

    Public Async Function RunAsync() As Task
        ' Connecting to Ethereum mainnet using Infura
        Dim web3 = New Web3(""https://mainnet.infura.io/v3/7238211010344719ad14a89db874158c"")

	    ' Getting current block number  
        Dim blockNumber = Await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync()
        Console.WriteLine(""Current BlockNumber is: "" & blockNumber.Value.ToString)

		' Getting current block with transactions 
        Dim block = Await web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(New HexBigInteger(8257129))
        Console.WriteLine(""Block number: "" & block.Number.Value.ToString)
        Console.WriteLine(""Block hash: "" & block.BlockHash)
        Console.WriteLine(""Block no of transactions: "" & block.Transactions.Length)
        Console.WriteLine(""Block transaction 0 From: "" & block.Transactions(0).From)
        Console.WriteLine(""Block transaction 0 To: "" & block.Transactions(0).[To])
        Console.WriteLine(""Block transaction 0 Amount: "" & block.Transactions(0).Value.ToString)
        Console.WriteLine(""Block transaction 0 Hash: "" & block.Transactions(0).TransactionHash)


        Dim transaction = Await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(""0xb4729a0d8dd30e3070d0cb203090f2b792e029f6fa4629e96d2ebc1de13cb5c4"")
        Console.WriteLine(""Transaction From: "" & transaction.From)
        Console.WriteLine(""Transaction To: "" & transaction.[To])
        Console.WriteLine(""Transaction Amount: "" & transaction.Value.ToString)
        Console.WriteLine(""Transaction Hash: "" & transaction.TransactionHash)


        Dim transactionReceipt = Await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(""0xb4729a0d8dd30e3070d0cb203090f2b792e029f6fa4629e96d2ebc1de13cb5c4"")
        Console.WriteLine(""Transaction Hash: "" & transactionReceipt.TransactionHash)
        Console.WriteLine(""TransactionReceipt Logs: "" & transactionReceipt.Logs.Count)
    End Function
End Module"
                },

                new CodeSample()
                {
                    Name="Ether: Transfer Ether to an account",
                    Code=
@"Imports System
Imports System.Text
Imports Nethereum.Hex.HexConvertors.Extensions
Imports System.Threading.Tasks
Imports Nethereum.Web3
Imports Nethereum.Web3.Accounts

Module Program
    Sub Main()
        'our entrypoint is RunAsync
    End Sub

    Public Async Function RunAsync() As Task
        ' First let's create an account with our private key for the account address 
        Dim privateKey = ""0x7580e7fb49df1c861f0050fae31c2224c6aba908e116b8da44ee8cd927b990b0""
        Dim account = New Account(privateKey)
        Console.WriteLine(""Our account: "" & account.Address)
		
        ' Now let's create an instance of Web3 using our account pointing to our nethereum testchain
        Dim web3 = New Web3(account, ""http://testchain.nethereum.com:8545"")

	    ' Check the balance of the account we are going to send the Ether
        Dim balance = Await web3.Eth.GetBalance.SendRequestAsync(""0x13f022d72158410433cbd66f5dd8bf6d2d129924"")
        Console.WriteLine(""Receiver account balance before sending Ether: "" & balance.Value.ToString & "" Wei"")
        Console.WriteLine(""Receiver account balance before sending Ether: "" & Web3.Convert.FromWei(balance.Value) & "" Ether"")

		' Lets transfer 1.11 Ether
        Dim transaction = Await web3.Eth.GetEtherTransferService().TransferEtherAndWaitForReceiptAsync(""0x13f022d72158410433cbd66f5dd8bf6d2d129924"", 1.11D)

        balance = Await web3.Eth.GetBalance.SendRequestAsync(""0x13f022d72158410433cbd66f5dd8bf6d2d129924"")
        Console.WriteLine(""Receiver account balance after sending Ether: "" & balance.Value.ToString)
        Console.WriteLine(""Receiver account balance after sending Ether: "" & Web3.Convert.FromWei(balance.Value) & "" Ether"")
    End Function
End Module"
                },

                new CodeSample()
                {
                    Name="Smart Contracts: Query ERC20 Smart contract balance",
                    Code=
@"Imports System
Imports System.Numerics
Imports System.Threading.Tasks
Imports Nethereum.Web3
Imports Nethereum.ABI.FunctionEncoding.Attributes
Imports Nethereum.Contracts

Module Program
    Sub Main()
        'our entrypoint is RunAsync
    End Sub

    ' async to enable async task methods
    Public Async Function RunAsync() As Task
        ' Connecting to Ethereum mainnet using Infura
        Dim iweb3 = New Web3(""https://mainnet.infura.io/v3/7238211010344719ad14a89db874158c"")

        ' Setting the owner https://etherscan.io/tokenholdings?a=0x8ee7d9235e01e6b42345120b5d270bdb763624c7
        Dim balanceOfFunctionMessage As New BalanceOfFunction
        balanceOfFunctionMessage.Owner = ""0x8ee7d9235e01e6b42345120b5d270bdb763624c7""
        
        ' Getting the contract address
        Dim contractAddress = ""0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2""
        
        ' Creating a new query handler
        Dim balanceHandler = iweb3.Eth.GetContractQueryHandler(Of BalanceOfFunction)

        'Querying the Maker smart contract https://etherscan.io/address/0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2
        Dim balance = Await balanceHandler.QueryAsync(Of BigInteger)(contractAddress, balanceOfFunctionMessage)
        Console.WriteLine(balance.ToString)
    End Function
End Module

' The balance function message definition    
<[Function](""balanceOf"", ""uint256"")>
Public Class BalanceOfFunction
    Inherits FunctionMessage

    <Parameter(""address"", ""_owner"", 1)>
    Public Property Owner() As String
End Class"
                },

                new CodeSample()
                {
                    Name="Smart Contracts: Smart contract deployment",
                    Code=
@"Imports Nethereum.Web3
Imports Nethereum.ABI.FunctionEncoding.Attributes
Imports Nethereum.Contracts.CQS
Imports Nethereum.Util
Imports Nethereum.Web3.Accounts
Imports Nethereum.Hex.HexConvertors.Extensions
Imports Nethereum.Contracts
Imports Nethereum.Contracts.Extensions
Imports System
Imports System.Numerics
Imports System.Threading
Imports System.Threading.Tasks

Module SmartContracts_DeployingContract
    Sub Main()
        'our entrypoint is RunAsync
    End Sub

     ' To deploy a contract we will create a class inheriting from the ContractDeploymentMessage, 
     ' here we can include our compiled byte code and other constructor parameters.
     ' As we can see below the StandardToken deployment message includes the compiled bytecode 
     ' of the ERC20 smart contract and the constructor parameter with the ""totalSupply"" of tokens.
     ' Each parameter is described with an attribute Parameter, including its name ""totalSupply"", type ""uint256"" and order.

    Public Class StandardTokenDeployment
        Inherits ContractDeploymentMessage

        Public Shared BYTECODE As String = ""0x60606040526040516020806106f5833981016040528080519060200190919050505b80600160005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005081905550806000600050819055505b506106868061006f6000396000f360606040523615610074576000357c010000000000000000000000000000000000000000000000000000000090048063095ea7b31461008157806318160ddd146100b657806323b872dd146100d957806370a0823114610117578063a9059cbb14610143578063dd62ed3e1461017857610074565b61007f5b610002565b565b005b6100a060048080359060200190919080359060200190919050506101ad565b6040518082815260200191505060405180910390f35b6100c36004805050610674565b6040518082815260200191505060405180910390f35b6101016004808035906020019091908035906020019091908035906020019091905050610281565b6040518082815260200191505060405180910390f35b61012d600480803590602001909190505061048d565b6040518082815260200191505060405180910390f35b61016260048080359060200190919080359060200190919050506104cb565b6040518082815260200191505060405180910390f35b610197600480803590602001909190803590602001909190505061060b565b6040518082815260200191505060405180910390f35b600081600260005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060008573ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050819055508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925846040518082815260200191505060405180910390a36001905061027b565b92915050565b600081600160005060008673ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050541015801561031b575081600260005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060003373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000505410155b80156103275750600082115b1561047c5781600160005060008573ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505401925050819055508273ffffffffffffffffffffffffffffffffffffffff168473ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef846040518082815260200191505060405180910390a381600160005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060008282825054039250508190555081600260005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060003373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505403925050819055506001905061048656610485565b60009050610486565b5b9392505050565b6000600160005060008373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000505490506104c6565b919050565b600081600160005060003373ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050541015801561050c5750600082115b156105fb5781600160005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060008282825054039250508190555081600160005060008573ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505401925050819055508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef846040518082815260200191505060405180910390a36001905061060556610604565b60009050610605565b5b92915050565b6000600260005060008473ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060008373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005054905061066e565b92915050565b60006000600050549050610683565b9056""

        Public Sub New()
            MyBase.New(BYTECODE)
        End Sub

        <Parameter(""uint256"", ""totalSupply"")>
        Public Property TotalSupply As BigInteger
    End Class

    Public Async Function RunAsync() As Task
        ' Instantiating Web3 and the Account

        ' To create an instance of web3 we first provide the url of our testchain and the private key of our account. 
        ' When providing an Account instantiated with a  private key all our transactions will be signed “offline” by Nethereum.
        Dim url = ""http://testchain.nethereum.com:8545""
        Dim privateKey = ""0x7580e7fb49df1c861f0050fae31c2224c6aba908e116b8da44ee8cd927b990b0""
        Dim account = New Account(privateKey)
        Dim web3 = New Web3(account, url)

        ' Deploying the Contract
        ' The next step is to deploy our Standard Token ERC20 smart contract, in this scenario the total supply (number of tokens) is going to be 100,000.
        ' First we create an instance of the StandardTokenDeployment with the TotalSupply amount.

        Dim deploymentMessage = New StandardTokenDeployment With {
            .TotalSupply = 100000
        }

        ' Then we create a deployment handler using our contract deployment definition and simply deploy the contract using the deployment message. We are auto estimating the gas, getting the latest gas price and nonce so nothing else is set anything on the deployment message.
        ' Finally, we wait for the deployment transaction to be mined, and retrieve the contract address of the new contract from the receipt.
        Dim deploymentHandler = web3.Eth.GetContractDeploymentHandler(Of StandardTokenDeployment)()
        Dim transactionReceipt = Await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage)
        Dim contractAddress = transactionReceipt.ContractAddress
        Console.WriteLine(""Deployed Contract address is: "" & contractAddress)
    End Function
End Module"
                },

                new CodeSample()
                {
                    Name="Smart Contracts: Smart Contracts Deployment, Querying, Transactions, Nonces, Estimating Gas, Gas Price",
                    Code=
@"Imports Nethereum.Web3
Imports Nethereum.ABI.FunctionEncoding.Attributes
Imports Nethereum.Contracts.CQS
Imports Nethereum.Util
Imports Nethereum.Web3.Accounts
Imports Nethereum.Hex.HexConvertors.Extensions
Imports Nethereum.Contracts
Imports Nethereum.Contracts.Extensions
Imports System
Imports System.Numerics
Imports System.Threading
Imports System.Threading.Tasks

'********* Quick introduction to smart contracts integration with Nethereum  *******
'
'	Topics covered:
'   
'	 * Understanding how to create contract deployment, function and event definitions to interact with a smart contracts
'	 * Creating an account object using a private key, this will allow to sign transactions ""offline"".
'    * Deploying a smart contract (the sample provided is the standard ERC20 token contract)
'    * Making a call to a smart contract (in this scenario get the balance of an account)
'    * Sending a transaction to the smart contract (in this scenario transferring balance)
'    * Estimating the gas cost of a contract transaction
'    * Gas Price, Nonces and Sending Ether to smart contracts
'    * Retrieving the state of a smart contract from a previous block



'********* CONTRACT DEFINITION  *******

'*** Deployment message**** '
' To deploy a contract we will create a class inheriting from the ContractDeploymentMessage, 
' here we can include our compiled byte code and other constructor parameters.
' As we can see below the StandardToken deployment message includes the compiled bytecode 
' of the ERC20 smart contract and the constructor parameter with the “totalSupply” of tokens.
' Each parameter is described with an attribute Parameter, including its name ""totalSupply"", type ""uint256"" and order.

Module GetStartedSmartContracts
    Sub Main()
        'our entrypoint is RunAsync
    End Sub
    Public Class StandardTokenDeployment
        Inherits ContractDeploymentMessage

        Public Shared BYTECODE As String = ""0x60606040526040516020806106f5833981016040528080519060200190919050505b80600160005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005081905550806000600050819055505b506106868061006f6000396000f360606040523615610074576000357c010000000000000000000000000000000000000000000000000000000090048063095ea7b31461008157806318160ddd146100b657806323b872dd146100d957806370a0823114610117578063a9059cbb14610143578063dd62ed3e1461017857610074565b61007f5b610002565b565b005b6100a060048080359060200190919080359060200190919050506101ad565b6040518082815260200191505060405180910390f35b6100c36004805050610674565b6040518082815260200191505060405180910390f35b6101016004808035906020019091908035906020019091908035906020019091905050610281565b6040518082815260200191505060405180910390f35b61012d600480803590602001909190505061048d565b6040518082815260200191505060405180910390f35b61016260048080359060200190919080359060200190919050506104cb565b6040518082815260200191505060405180910390f35b610197600480803590602001909190803590602001909190505061060b565b6040518082815260200191505060405180910390f35b600081600260005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060008573ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050819055508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925846040518082815260200191505060405180910390a36001905061027b565b92915050565b600081600160005060008673ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050541015801561031b575081600260005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060003373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000505410155b80156103275750600082115b1561047c5781600160005060008573ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505401925050819055508273ffffffffffffffffffffffffffffffffffffffff168473ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef846040518082815260200191505060405180910390a381600160005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060008282825054039250508190555081600260005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060003373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505403925050819055506001905061048656610485565b60009050610486565b5b9392505050565b6000600160005060008373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000505490506104c6565b919050565b600081600160005060003373ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050541015801561050c5750600082115b156105fb5781600160005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060008282825054039250508190555081600160005060008573ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505401925050819055508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef846040518082815260200191505060405180910390a36001905061060556610604565b60009050610605565b5b92915050565b6000600260005060008473ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060008373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005054905061066e565b92915050565b60006000600050549050610683565b9056""

        Public Sub New()
            MyBase.New(BYTECODE)
        End Sub

        <Parameter(""uint256"", ""totalSupply"")>
        Public Property TotalSupply As BigInteger
    End Class


'*** FUNCTION MESSAGES **** '/

' We can call the functions of smart contract to query the state of a smart contract or do any computation, 
' which will not affect the state of the blockchain.

' To do so,  we will need to create a class which inherits from ""FunctionMessage"". 
' First we will decorate the class with a ""Function"" attribute, including the name and return type.
' Each parameter of the function will be a property of the class, each of them decorated with the ""Parameter"" attribute, 
' including the smart contract’s parameter name, type and parameter order.
' For the ERC20 smart contract, the ""balanceOf"" function definition, 
' provides the query interface to get the token balance of a given address. 
' As we can see this function includes only one parameter ""_owner"", of the type ""address"".

    <[Function](""balanceOf"", ""uint256"")>
    Public Class BalanceOfFunction
        Inherits FunctionMessage

        <Parameter(""address"", ""_owner"", 1)>
        Public Property Owner As String
    End Class


' Another type of smart contract function will be a transaction 
' that will change the state of the smart contract (or smart contracts).
' For example The ""transfer"" function definition for the ERC20 smart contract, 
' includes the parameters ""_to"", which is an address parameter as a string, and the ""_value"" 
' or TokenAmount we want to transfer.


' In a similar way to the ""balanceOf"" function, all the parameters include the solidity type, 
' the contract’s parameter name and parameter order.


' Note: When working with functions, it is very important to have the parameters types and function name correct 
'as all of these make the signature of the function.


    <[Function](""transfer"", ""bool"")>
    Public Class TransferFunction
        Inherits FunctionMessage

        <Parameter(""address"", ""_to"", 1)>
        Public Property [To] As String
        <Parameter(""uint256"", ""_value"", 2)>
        Public Property TokenAmount As BigInteger
    End Class


' Finally, smart contracts also have events. Events defined in smart contracts write in the blockchain log, 
' providing a way to retrieve further information when a smart contract interaction occurs.
' To create an Event definition, we need to create a class that inherits from IEventDTO, decorated with the Event attribute.
' The Transfer Event is similar to a Function: it  also includes parameters with name, order and type. 
' But also a boolean value indicating if the parameter is indexed or not.
' Indexed parameters will allow us later on to query the blockchain for those values.


    <[Event](""Transfer"")>
    Public Class TransferEventDTO
        Implements IEventDTO

        <Parameter(""address"", ""_from"", 1, True)>
        Public Property From As String
        <Parameter(""address"", ""_to"", 2, True)>
        Public Property [To] As String
        <Parameter(""uint256"", ""_value"", 3, False)>
        Public Property Value As BigInteger
    End Class

' ### Multiple return types or complex objects
' Functions of smart contracts can return one or multiple values in a single call. To decode the returned values, we use a FunctionOutputDTO.
' Function outputs are classes which are decorated with a FunctionOutput attribute and implement the interface IFunctionOutputDTO.
' An example of this is the following implementation that can be used to return the single value of the Balance on the ERC20 smart contract.

    <FunctionOutput>
    Public Class BalanceOfOutputDTO
        Implements IFunctionOutputDTO

        <Parameter(""uint256"", ""balance"", 1)>
        Public Property Balance As BigInteger
    End Class

' If we were going to return multiple values we could have something like:

    <FunctionOutput>
    Public Class BalanceOfOutputMultipleDTO
        Implements IFunctionOutputDTO

        <Parameter(""uint256"", ""balance1"", 1)>
        Public Property Balance1 As BigInteger
        <Parameter(""uint256"", ""balance2"", 2)>
        Public Property Balance2 As BigInteger
        <Parameter(""uint256"", ""balance3"", 3)>
        Public Property Balance3 As BigInteger
    End Class


' **** END CONTRACT DEFINITIONS *****

' *** THE MAIN PROGRAM ***
    Public Async Function RunAsync() As Task

        ' ### Instantiating Web3 and the Account
        ' To create an instance of web3 we first provide the url of our testchain and the private key of our account. 
        ' Here we are using http://testchain.nethereum.com:8545 which is our simple single node Nethereum testchain.
        ' When providing an Account instantiated with a  private key, all our transactions will be signed by Nethereum.

        Dim url = ""http://testchain.nethereum.com:8545""
        Dim privateKey = ""0x7580e7fb49df1c861f0050fae31c2224c6aba908e116b8da44ee8cd927b990b0""
        Dim account = New Account(privateKey)
        Dim web3 = New Web3(account, url)

        ' **** DEPLOYING THE SMART CONTRACT
        ' The next step is to deploy our Standard Token ERC20 smart contract, 
        'in this scenario the total supply (number of tokens) is going to be 100,000.

        ' First we create an instance of the StandardTokenDeployment with the TotalSupply amount.

        Dim deploymentMessage = New StandardTokenDeployment With {
            .TotalSupply = 100000
        }

        ' Then we create a deployment handler using our contract deployment definition and simply deploy the contract 
        ' using the deployment message. 
        ' We are auto estimating the gas, getting the latest gas price and nonce so nothing else is set on the deployment message.
        ' Finally, we wait for the deployment transaction to be mined, 
        ' and retrieve the contract address of the new contract from the receipt.

        Dim deploymentHandler = web3.Eth.GetContractDeploymentHandler(Of StandardTokenDeployment)()
        Dim transactionReceiptDeployment = Await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage)
        Dim contractAddress = transactionReceiptDeployment.ContractAddress
        Console.WriteLine(""Smart contract deployed at address:"" & contractAddress)

        ' *** INTERACTING WITH THE CONTRACT

        ' #### QUERING


        ' To retrieve the balance, we will create a QueryHandler and finally using our contract address 
        ' and message retrieve the balance amount.

        Dim balanceOfFunctionMessage = New BalanceOfFunction() With {
            .Owner = account.Address
        }
            Dim balanceHandler = web3.Eth.GetContractQueryHandler(Of BalanceOfFunction)()
        Dim balance = Await balanceHandler.QueryAsync(Of BigInteger)(contractAddress, balanceOfFunctionMessage)
        Console.WriteLine(""Balance of deployment owner address: "" & balance.ToString)

        ' When Quering retrieving multiple results, we can use this method instead

        Dim balanceOutput = Await balanceHandler.QueryDeserializingToObjectAsync(Of BalanceOfOutputDTO)(balanceOfFunctionMessage, contractAddress)


        ' #### Transfer
        ' Making a transfer will change the state of the blockchain, 
        ' so in this scenario we will need to create a TransactionHandler using the TransferFunction definition.

        ' In the transfer message, we will include the receiver address ""To"", and the ""TokenAmount"" to transfer.
        ' The final step is to Send the request, wait for the receipt to be ""mined"" and included in the blockchain.
        ' Another option will be to not wait (poll) for the transaction to be mined and just retrieve the transaction hash.
        Dim receiverAddress = ""0xde0B295669a9FD93d5F28D9Ec85E40f4cb697BAe""
        Dim transferHandler = web3.Eth.GetContractTransactionHandler(Of TransferFunction)()
        Dim transfer = New TransferFunction() With {
            .[To] = receiverAddress,
            .TokenAmount = 100
        }
        Dim transactionTransferReceipt = Await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, transfer)
        Console.WriteLine(""Transaction hash transfer is: "" & transactionTransferReceipt.TransactionHash)
        balance = Await balanceHandler.QueryAsync(Of BigInteger)(contractAddress, balanceOfFunctionMessage)
        Console.WriteLine(""Balance of deployment owner address after transfer: "" & balance.ToString)

        ' #### Querying previous state of the smart contract

        ' Another great feature of the Ethereum blockchain is the capability to retrieve the state 
        ' of a smart contract from a previous block.

        ' For example, we could get the balance of the owner at the time of deployment by using the block number 
        ' in which the contract was deployed we will get the 10000
        balanceOutput = Await balanceHandler.QueryDeserializingToObjectAsync(Of BalanceOfOutputDTO)(balanceOfFunctionMessage, contractAddress, New Nethereum.RPC.Eth.DTOs.BlockParameter(transactionReceiptDeployment.BlockNumber))
        Console.WriteLine(""Balance of deployment owner address from previous Block Number: "" & transactionReceiptDeployment.BlockNumber.Value.ToString & "" is: "" + balanceOutput.Balance.ToString)

        ' ##### Transferring Ether to a smart contract
        ' A function or deployment transaction can send Ether to the smart contract. The FunctionMessage and DeploymentMessage have the property ""AmountToSend"".
        ' So if the ""transfer"" function also accepts Ether, we will set it this way.

        transfer.AmountToSend = Nethereum.Web3.Web3.Convert.ToWei(1)

        ' The GasPrice is set in ""Wei"" which is the lowest unit in Ethereum, so in the scenario above we have converted 1 Ether to Wei.
        ' ### Gas Price
        ' Nethereum automatically sets the GasPrice if not provided by using the clients ""GasPrice"" call, which provides the average gas price from previous blocks.
        ' If you want to have more control over the GasPrice these can be set in both FunctionMessages and DeploymentMessages.

        transfer.GasPrice = Nethereum.Web3.Web3.Convert.ToWei(25, UnitConversion.EthUnit.Gwei)

        ' The GasPrice is set in ""Wei"" which is the lowest unit in Ethereum, so if we are used to the usual ""Gwei"" units, this will need to be converted using the Nethereum Convertion utilities.
        ' ### Estimating Gas
        ' Nethereum does an automatic estimation of the total gas necessary to make the function transaction by calling the ""EthEstimateGas"" internally with the ""CallInput"".
        ' If needed, this can be done manually, using the TransactionHandler and the ""transfer"" transaction FunctionMessage.
        Dim estimate = Await transferHandler.EstimateGasAsync(contractAddress, transfer)
        transfer.Gas = estimate.Value

        ' ### Nonces
        ' Each account transaction has a Nonce associated with it, this is the order and unique number for that transaction. This allows each transaction to be differentiated from each other, but also ensure transactions are processed in the same order.

        ' Nethereum calculates the Nonce automatically for all Transactions by retrieving the latest count of the transactions from the chain. Also internally manages at Account level an in memory counter on the nonces, to allow for situations in which we want to send multiple transactions before giving time to the Ethereum client to update its internal counter.
        ' Nevertheless there might be scenarios where we want to supply our Nonce, for example if we want to sign the transaction completely offline.
        transfer.Nonce = 2

        ' ### Signing a Function / Deployment message online / offline

        ' The TransactionHandler also provides a mechanism to sign the Function and Deployments messages, provided we use an Account and/or an ExternalAccount
        Dim signedTransaction1 = Await transferHandler.SignTransactionAsync(contractAddress, transfer)
        Console.WriteLine(""SignedTransaction is: "" & signedTransaction1)

        ' Nethereum internally calls the Ethereum client to set the GasPrice, Nonce and estimate the Gas, 
        ' so if we want to sign the transaction for the contract completely offline we will need to set those values before hand.
        transfer.Nonce = 2
        transfer.Gas = 21000
        transfer.GasPrice = Nethereum.Web3.Web3.Convert.ToWei(25, UnitConversion.EthUnit.Gwei)
        Dim signedTransaction2 = Await transferHandler.SignTransactionAsync(contractAddress, transfer)
        Console.WriteLine(""Full offline (no need for node) Signed Transaction (providing manually the nonce, gas and gas price) is: "" & signedTransaction2)
    End Function
End Module"
                },

                new CodeSample()
                {
                    Name="Smart Contracts: Events",
                    Code=
@"Imports System
Imports System.Text
Imports Nethereum.Hex.HexConvertors.Extensions
Imports System.Threading
Imports System.Threading.Tasks
Imports Nethereum.Web3
Imports Nethereum.Web3.Accounts
Imports Nethereum.ABI.FunctionEncoding.Attributes
Imports Nethereum.Util
Imports Nethereum.Contracts
Imports Nethereum.Contracts.Extensions
Imports Nethereum.Contracts.CQS
Imports System.Numerics

Module GettingStarted_Events
    ' Events in smart contracts write data to the transaction receipt logs, providing a way to get extra information
    ' about a smart contract transactions.

    ' A very good example Is the ""Transfer"" event in the ERC20 Standard Token contract.
    ' Everytime that a token transfer has ocurred, an event gets logged providing information of the ""sender"",
    ' ""receiver"" And the token amount. In this scenario we are only interested
    ' in the Deployment, Transfer function And Transfer Event of the ERC20 smart contract.
    '
    '_____________________________________________________________________________________________________
    '
    ' event Transfer(address indexed _from, address indexed _to, uint256 _value);
    '
    ' function transfer(address _to, uint256 _value) public returns (bool success) {
    '     require(balances[msg.sender] >= _value, ""Balance amount lower than amount requested"");
    '     balances[msg.sender] -= _value;
    '     balances[_to] += _value;
    '     emit Transfer(msg.sender, _to, _value);
    '     return true;
    '}
    '_____________________________________________________________________________________________________
    '
    ' 
    ' Above we can see, the event declaration with the different indexed parameters, these will allow us
    ' later on to ""filter"" for specific events.
    ' For example ,""Transfer"" events for a specific receiver address ""_to"".

    ' The Transfer event can be seen in the function prefixed with the “emit” keyword.



    ' To deploy a contract we will create a class inheriting from the ContractDeploymentMessage,
    ' here we can include our compiled byte code And other constructor parameters.

    ' As we can see below the StandardToken deployment message includes the compiled bytecode of the ERC20
    ' smart contract And the constructor parameter with the ""totalSupply"" of tokens.

    ' Each parameter Is described with an attribute Parameter, including its name
    ' ""totalSupply"", type ""uint256"" And order.

    '/**** START CONTRACT DEFINITION
    Public Class StandardTokenDeployment
        Inherits ContractDeploymentMessage

        Public Shared BYTECODE As String = ""0x60606040526040516020806106f5833981016040528080519060200190919050505b80600160005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005081905550806000600050819055505b506106868061006f6000396000f360606040523615610074576000357c010000000000000000000000000000000000000000000000000000000090048063095ea7b31461008157806318160ddd146100b657806323b872dd146100d957806370a0823114610117578063a9059cbb14610143578063dd62ed3e1461017857610074565b61007f5b610002565b565b005b6100a060048080359060200190919080359060200190919050506101ad565b6040518082815260200191505060405180910390f35b6100c36004805050610674565b6040518082815260200191505060405180910390f35b6101016004808035906020019091908035906020019091908035906020019091905050610281565b6040518082815260200191505060405180910390f35b61012d600480803590602001909190505061048d565b6040518082815260200191505060405180910390f35b61016260048080359060200190919080359060200190919050506104cb565b6040518082815260200191505060405180910390f35b610197600480803590602001909190803590602001909190505061060b565b6040518082815260200191505060405180910390f35b600081600260005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060008573ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050819055508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925846040518082815260200191505060405180910390a36001905061027b565b92915050565b600081600160005060008673ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050541015801561031b575081600260005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060003373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000505410155b80156103275750600082115b1561047c5781600160005060008573ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505401925050819055508273ffffffffffffffffffffffffffffffffffffffff168473ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef846040518082815260200191505060405180910390a381600160005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060008282825054039250508190555081600260005060008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060003373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505403925050819055506001905061048656610485565b60009050610486565b5b9392505050565b6000600160005060008373ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000505490506104c6565b919050565b600081600160005060003373ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600050541015801561050c5750600082115b156105fb5781600160005060003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060008282825054039250508190555081600160005060008573ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000828282505401925050819055508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef846040518082815260200191505060405180910390a36001905061060556610604565b60009050610605565b5b92915050565b6000600260005060008473ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005060008373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060005054905061066e565b92915050565b60006000600050549050610683565b9056""

        Public Sub New()
            MyBase.New(BYTECODE)
        End Sub

        <Parameter(""uint256"", ""totalSupply"")>
        Public Property TotalSupply As BigInteger
    End Class

    ' We can call the functions of smart contract to query the state of a smart contract Or do any computation,
    ' which will Not affect the state of the blockchain.

    ' To do so we will need to create a class which inherits from ""FunctionMessage"".
    ' First we will decorate the class with a ""Function"" attribute, including the name And return type.

    ' Each parameter of the the function will be a property of the class, each of them decorated with the
    ' ""Parameter"" attribute, including the smart contract name, type And parameter order.

    ' For the ERC20 smart contract, the ""balanceOf"" function definition, provides the query interface
    ' to get the token balance of a given address. As we can see this function includes only one parameter
    ' ""_owner"", of the type ""address"".
    <[Function](""balanceOf"", ""uint256"")>
    Public Class BalanceOfFunction
        Inherits FunctionMessage

        <Parameter(""address"", ""_owner"", 1)>
        Public Property Owner As String
    End Class


    ' Another type of smart contract function will be correspondent to a transaction that will change
    ' the state of the smart contract (Or smart contracts).

    ' For example The ""transfer"" function definition for the ERC20 smart contract, includes the parameters
    ' ""_to"" address parameter as a string, And the ""_value""
    ' Or TokenAmount we want to transfer.

    ' In a similar way to the ""balanceOf"" function, all the parameters include the solidity type,
    ' parameter name And parameter order.

    ' Note When working with functions, it Is very important to have the parameters types, And function name
    ' correct as all of these make the signature of the function.
    <[Function](""transfer"", ""bool"")>
    Public Class TransferFunction
        Inherits FunctionMessage

        <Parameter(""address"", ""_to"", 1)>
        Public Property [To] As String
        <Parameter(""uint256"", ""_value"", 2)>
        Public Property TokenAmount As BigInteger
    End Class


    ' Finally smart contracts also have events. Events in smart contracts write the blockchain log,
    ' providing a way to retrieve further information of any smart contract interaction occurred.

    ' To create an Event definition, we need to create a class that inherits from IEventDTO,
    ' decorated with the Event attribute.

    ' The Transfer Event, similar to the Function it also includes the parameters with the name,
    ' order And type. But also a boolean value indicating if the parameter Is indexed Or Not.

    ' Indexed parameters will allow us later on to query the blockchain for those values.
    <[Event](""Transfer"")>
    Public Class TransferEventDTO
        Implements IEventDTO

        <Parameter(""address"", ""_from"", 1, True)>
        Public Property From As String
        <Parameter(""address"", ""_to"", 2, True)>
        Public Property [To] As String
        <Parameter(""uint256"", ""_value"", 3, False)>
        Public Property Value As BigInteger
    End Class
    '**** END CONTRACT DEFINITION

    Sub Main()
        'our entrypoint is RunAsync
    End Sub

    Public Async Function RunAsync() As Task
        ' ### Instantiating Web3 And the Account

        ' To create an instance of web3 we first provide the url of our testchain And the private key of our account.
        ' When providing an Account instantiated with a  private key all our transactions will be signed
        ' ""offline"" by Nethereum.

        Dim url = ""http://testchain.nethereum.com:8545""
        Dim privateKey = ""0x7580e7fb49df1c861f0050fae31c2224c6aba908e116b8da44ee8cd927b990b0""
        Dim account = New Account(privateKey)
        Dim web3 = New Web3(account, url)

        ' ### Deploying the Contract

        ' The next step Is to deploy our Standard Token ERC20 smart contract, in this scenario the total supply
        ' (number of tokens) Is going to be 100,000.

        ' First we create an instance of the StandardTokenDeployment with the TotalSupply amount.
        Dim deploymentMessage = New StandardTokenDeployment With {
            .TotalSupply = 100000
        }

            ' Then we create a deployment handler using our contract deployment definition And simply deploy the
        ' contract using the deployment message. We are auto estimating the gas, getting the latest gas price
        ' And nonce so nothing else Is set anything on the deployment message.

        ' Finally, we wait for the deployment transaction to be mined, And retrieve the contract address of
        ' the New contract from the receipt.

        Dim deploymentHandler = web3.Eth.GetContractDeploymentHandler(Of StandardTokenDeployment)()
        Dim transactionReceipt = Await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage)
        Dim contractAddress = transactionReceipt.ContractAddress
        Console.WriteLine(""contractAddress is: "" & contractAddress)

        ' ### Transfer

        ' Once we have deployed the contract, we can execute our first transfer transaction.
        ' The transfer function will write to the log the transfer event.

        ' First we can create a TransactionHandler using the TrasferFunction definition And a
        ' TransferFunction message including the “receiverAddress” And the amount of tokens we want to send.

        ' Finally do the transaction transfer And wait for the receipt to be “mined”
        ' And included in the blockchain.

        Dim receiverAddress = ""0xde0B295669a9FD93d5F28D9Ec85E40f4cb697BAe""
        Dim transferHandler = web3.Eth.GetContractTransactionHandler(Of TransferFunction)()
        Dim transfer = New TransferFunction() With {
            .[To] = receiverAddress,
            .TokenAmount = 100
        }
        Dim transactionReceipt2 = Await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, transfer)

        ' ## Decoding the Event from the TransactionReceipt

        ' Event logs are part of the TransactionReceipts, so using the Transaction receipt from the previous
        ' transfer we can decode the TransferEvent using the extension method
        ' ""DecodeAllEvents<TransferEventDTO>()"".

        ' Note that this method returns an array of Decoded Transfer Events as opposed to a single value,
        ' because the receipt can include more than one event of the same signature.
        Dim transferEventOutput = transactionReceipt2.DecodeAllEvents(Of TransferEventDTO)()

        ' ## Contract Filters And Event Logs

        ' Another way to access the event logs of a smart contract Is to either get all changes of the logs
        ' (providing a filter message) Or create filters And retrieve changes which apply to our filter message
        ' periodically.                                  

        ' To access the logs, first of all, we need to create a transfer event handler for our contract address,
        ' And Event definition.(TransferEventDTO).
        Dim transferEventHandler = web3.Eth.GetEvent(Of TransferEventDTO)(contractAddress)

        ' Using the event handler, we can create a filter message for our transfer event using the default values.

        ' The default values for BlockParameters are Earliest And Latest, so when we retrieve the logs
        ' we will get all the transfer events from the first block to the latest block of this contract.
        Dim filterAllTransferEventsForContract = transferEventHandler.CreateFilterInput()

        ' Once we have created the message we can retrieve all the logs using the event And GetAllChanges.
        ' In this scenario, because we have made only one transfer, we will have only one Transfer Event.
        Dim allTransferEventsForContract = Await transferEventHandler.GetAllChanges(filterAllTransferEventsForContract)
        Console.WriteLine(""Transfer event TransactionHash : "" & allTransferEventsForContract(0).Log.TransactionHash)

        ' if we now make another Transfer to a different address
        Dim receiverAddress2 = ""0x3e0B295669a9FD93d5F28D9Ec85E40f4cb697BAe""
        Dim transfer2 = New TransferFunction() With {
            .[To] = receiverAddress2,
            .TokenAmount = 1000
        }
        Dim transactionReceipt3 = Await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, transfer2)

        ' Using the same filter input message And making another GetAllChanges call, we will now get
        ' the two Transfer Event logs.
        Dim allTransferEventsForContract2 = Await transferEventHandler.GetAllChanges(filterAllTransferEventsForContract)

        For i As Integer = 0 To 2 - 1
            Console.WriteLine(""Transfer event number : "" & i & "" - TransactionHash : "" & allTransferEventsForContract2((i)).Log.TransactionHash)
        Next


        ' Filter messages can limit the results (similar to block ranges) to the indexed parameters,
        ' for example we can create a filter for only our sender address And the receiver address.
        ' As a reminder our Event has as indexed parameters the ""_from"" address
        ' And ""_to"" address.

        Dim filterTransferEventsForContractReceiverAddress2 = transferEventHandler.CreateFilterInput(account.Address, receiverAddress2)
        Dim transferEventsForContractReceiverAddress2 = Await transferEventHandler.GetAllChanges(filterTransferEventsForContractReceiverAddress2)

        ' The order the filter values is based on the event parameters order, if we want to include all the transfers to the ""receiverAddress2"", the account address from will need to be set to null to be ignored.
        ' Note We are using the array format to allow for null input of the first parameter.
        Dim filterTransferEventsForContractAllReceiverAddress2 = transferEventHandler.CreateFilterInput(Nothing, {receiverAddress2})
        Dim transferEventsForContractAllReceiverAddress2 = Await transferEventHandler.GetAllChanges(filterTransferEventsForContractAllReceiverAddress2)

        ' Another scenario Is when you want to include multiple indexed values, for example transfers for
        ' ""receiverAddress1"" Or ""receiverAddress2"".
        ' Then you will need to use an array of the values you are interested.

        Dim filterTransferEventsForContractAllReceiverAddresses = transferEventHandler.CreateFilterInput(Nothing, {receiverAddress2, receiverAddress})
        Dim transferEventsForContractAllReceiverAddresses = Await transferEventHandler.GetAllChanges(filterTransferEventsForContractAllReceiverAddresses)

        ' ### Creating filters to retrieve periodic changes

        ' Another option Is to create filters that return only the changes occurred since we got the previous results.
        ' This eliminates the need of tracking the last block the events were checked And delegate this
        ' to the Ethereum client.

        ' Using the same filter message we created before we can create the filter And get the filterId.
        Dim filterIdTransferEventsForContractAllReceiverAddress2 = Await transferEventHandler.CreateFilterAsync(filterTransferEventsForContractAllReceiverAddress2)

        ' One thing to note, if  try to get the filter changes now, we will not get any results because
        ' the filter only returns the changes since creation.
        Dim result = Await transferEventHandler.GetFilterChanges(filterIdTransferEventsForContractAllReceiverAddress2)

        ' But, if we make another transfer using the same values
        Await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, transfer2)

        ' and execute get filter changes using the same filter id, we will get the event for the previous transfer.
        Dim result2 = Await transferEventHandler.GetFilterChanges(filterIdTransferEventsForContractAllReceiverAddress2)
        Console.WriteLine(""result2/TransactionHash: "" & result2(0).Log.TransactionHash)

        ' Executing the same again will return no results because no new transfers have occurred
        ' since the last execution of GetFilterChanges.
        Dim result3 = Await transferEventHandler.GetFilterChanges(filterIdTransferEventsForContractAllReceiverAddress2)
        Console.WriteLine(""result3/Transaction Count: "" & result3.Count)

        ' ## Events for all Contracts

        ' Different contracts can have and raise/log the same event with the same signature,
        ' a simple example is the multiple standard token ERC20 smart contracts that are part of Ethereum.
        ' There might be scenarios you want to capture all the Events for different contracts using a specific filter,
        ' for example all the transfers to an address.

        ' In Nethereum creating an Event (handler) without a contract address allows to create filters
        ' which are not attached to a specific contract.
        Dim transferEventHandlerAnyContract = web3.Eth.GetEvent(Of TransferEventDTO)()

        ' There is already a contract deployed in the chain, from the previous sample,
        ' so to demonstrate the access to events of multiple contracts we can deploy another standard token contract.
        Dim transactionReceiptNewContract = Await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage)
        Dim contractAddress2 = transactionReceiptNewContract.ContractAddress

        'and make another transfer using this new contract and the same receiver address.
        Await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress2, transfer)

        ' Creating a default filter input, and getting all changes, will retrieve all the transfer events
        ' for all contracts.
        Dim filterAllTransferEventsForAllContracts = transferEventHandlerAnyContract.CreateFilterInput()
        Dim allTransferEventsForContract3 = Await transferEventHandlerAnyContract.GetAllChanges(filterAllTransferEventsForAllContracts)

        ' If we want to retrieve only all the transfers to the ""receiverAddress"",
        ' we can create the same filter as before ,including only the second indexed parameter (""to""). This will return the Transfers only to this address for both contracts.
        Dim filterTransferEventsForAllContractsReceiverAddress2 = transferEventHandlerAnyContract.CreateFilterInput(Nothing, {receiverAddress})
        Dim result4 = Await transferEventHandlerAnyContract.GetAllChanges(filterTransferEventsForAllContractsReceiverAddress2)

        For i As Integer = 0 To 2 - 1
            Console.WriteLine(""Transfer event number : "" & i & "" - TransactionHash : "" & result4((i)).Log.TransactionHash)
        Next
    End Function
End Module
"
                },

                new CodeSample()
                {
                    Name = "Message signing",
                    Code = 
@"Imports System
Imports Nethereum.Signer

Module Program
    Sub Main()
        Dim address = ""0x94618601FE6cb8912b274E5a00453949A57f8C1e""
        Console.WriteLine(address)
        Dim msg1 = ""wee test message 18/09/2017 02:55PM""
        Dim privateKey = ""0x7580e7fb49df1c861f0050fae31c2224c6aba908e116b8da44ee8cd927b990b0""
        Dim signer1 = New EthereumMessageSigner()
        Dim signature1 = signer1.EncodeUTF8AndSign(msg1, New EthECKey(privateKey))
        Console.WriteLine(signature1)
        Dim addressRec1 = signer1.EncodeUTF8AndEcRecover(msg1, signature1)
        Console.WriteLine(addressRec1)
    End Sub
End Module
"
                },

                new CodeSample()
                {
                    Name = "Create new private key / account",
                    Code = @"
Imports System
Imports System.Text
Imports System.Collections.Generic
Imports Nethereum.Util
Imports Nethereum.Signer
Imports Nethereum.Hex.HexConvertors.Extensions

Module Program
    Sub Main()
       'note this uses SecureRandom
       Dim ecKey = Nethereum.Signer.EthECKey.GenerateKey()
       Dim privateKey = ecKey.GetPrivateKeyAsBytes().ToHex()
       Console.WriteLine(privateKey)
       Dim address = ecKey.GetPublicAddress()
       Console.WriteLine(address)
    End Sub
End Module
                "
                }

            };

            foreach (var sample in samples)
            {
                sample.Language = CodeLanguage.VbNet;
            }

            return samples;
        }
    }
}