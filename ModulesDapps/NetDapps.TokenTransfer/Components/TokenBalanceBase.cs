using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.JsonRpc.Client;
using Nethereum.Web3;

namespace NetDapps.ERC20Token.Components
{
    public class TokenBalanceBase: ComponentBase
    {
        public string RpcUrl { get; set; }
        public string ContractAddress { get; set; }
        public string AccountAddress { get; set; } 
        public decimal Balance { get; set; }

        [Inject]
        public HttpClient Client { get; set; }

        [Function("balanceOf", "uint256")]
        public class BalanceOfFunction : FunctionMessage
        {
            [Nethereum.ABI.FunctionEncoding.Attributes.Parameter("address", "_owner", 1)]
            public string Owner { get; set; }
        }

        protected override void OnInit()
        {
            RpcUrl = "https://mainnet.infura.io/v3/7238211010344719ad14a89db874158c";
            ContractAddress = "0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2";
            AccountAddress = "0x8ee7d9235e01e6b42345120b5d270bdb763624c7";
            base.OnInit();
        }

        protected async Task GetTokenBalance()
        {
            var web3 = new Web3(RpcUrl);
            var balanceOfMessage = new BalanceOfFunction() { Owner = AccountAddress };

            //Creating a new query handler
            var queryHandler = web3.Eth.GetContractQueryHandler<BalanceOfFunction>();

            //Querying the Maker smart contract https://etherscan.io/address/0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2
            var balance = await queryHandler.QueryAsync<BigInteger>(ContractAddress, balanceOfMessage).ConfigureAwait(false);
            Balance = Web3.Convert.FromWei(balance);
        }

    }
}
