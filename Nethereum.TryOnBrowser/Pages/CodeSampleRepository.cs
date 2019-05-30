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
                }

            };
        }

    }

}

