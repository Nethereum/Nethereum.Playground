namespace Nethereum.TryOnBrowser.Pages
{
    public class VBCodeSampleRepository
    {
        public CodeSample[] GetCodeSamples()
        {
            return new CodeSample[]
            {


                new CodeSample()
                {
                    Name="Query ERC20 Smart contract balance",
                    Code= @"
Imports System
Imports System.Numerics
Imports System.Threading.Tasks
Imports Nethereum.Web3
Imports Nethereum.ABI.FunctionEncoding.Attributes
Imports Nethereum.Contracts

Module Program
    Sub Main()
        'our entrypoint is RunAsync
    End Sub

    Public Async Function RunAsync() As Task
        Dim iweb3 = New Web3(""https://mainnet.infura.io/v3/7238211010344719ad14a89db874158c"")

        Dim contractAddress = ""0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2""
        Dim balanceOfFunctionMessage As New BalanceOfFunction
        balanceOfFunctionMessage.Owner = ""0x8ee7d9235e01e6b42345120b5d270bdb763624c7""

        Dim balanceHandler = iweb3.Eth.GetContractQueryHandler(Of BalanceOfFunction)
        Dim balance = Await balanceHandler.QueryAsync(Of BigInteger)(contractAddress, balanceOfFunctionMessage)
        Console.WriteLine(balance.ToString)
    End Function
End Module

<[Function](""balanceOf"", ""uint256"")>
Public Class BalanceOfFunction
    Inherits FunctionMessage

    <Parameter(""address"", ""_owner"", 1)>
    Public Property Owner() As String
End Class
"
                },

                new CodeSample()
                {
                    Name = "Message signing",
                    Code = @"
Imports System
Imports Nethereum.Signer

Module Program
    Sub Main()
        Dim address = ""0x12890d2cce102216644c59dae5baed380d84830c""
        Console.WriteLine(address)
        Dim msg1 = ""wee test message 18/09/2017 02:55PM""
        Dim privateKey = ""0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7""
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
        }

    }

}

