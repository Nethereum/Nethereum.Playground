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
using Nethereum.RPC.Eth;
using Nethereum.Web3;

namespace NetDapps.Maker.Components
{/*
    RpcUrl = "https://mainnet.infura.io/v3/7238211010344719ad14a89db874158c";
            ContractAddress = "0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2";
            AccountAddress = "0x8ee7d9235e01e6b42345120b5d270bdb763624c7";
            */
    public class RpcQuery : ComponentBase
    {
        [Microsoft.AspNetCore.Components.Parameter] public string RpcUrl { get; set; }
  
    }

    public class ContractQuery : RpcQuery
    {
        [Microsoft.AspNetCore.Components.Parameter] public string ContractAddress { get; set; }

    }

    public class TokenBalanceBase : RpcQuery
    {
        [Microsoft.AspNetCore.Components.Parameter] public string MakerContractAddress { get; set; }
        [Microsoft.AspNetCore.Components.Parameter] public string DaiContractAddress { get; set; }
        [Microsoft.AspNetCore.Components.Parameter] public string PethContractAddress { get; set; }
        [Microsoft.AspNetCore.Components.Parameter] public string WethContractAddress { get; set; }

        public string AccountAddress { get; set; }
        public decimal DaiBalance { get; set; }
        public decimal MkrBalance { get; set; }
        public decimal PethBalance { get; set; }
        public decimal WethBalance { get; set; }
        public decimal EthBalance { get; set; }

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
            base.OnInit();
        }

        protected async Task GetTokensBalance()
        {
            var web3 = new Web3(RpcUrl);
            DaiBalance = await GetTokenBalance(DaiContractAddress, web3);
            MkrBalance = await GetTokenBalance(MakerContractAddress, web3);
            PethBalance = await GetTokenBalance(PethContractAddress, web3);
            WethBalance = await GetTokenBalance(WethContractAddress, web3);
            EthBalance =  await GetEthBalance(web3);
        }

        protected async Task<decimal> GetTokenBalance(string contractAddress, Web3 web3)
        {
            var balanceOfMessage = new BalanceOfFunction() { Owner = AccountAddress };
            var queryHandler = web3.Eth.GetContractQueryHandler<BalanceOfFunction>();
            var balance = await queryHandler.QueryAsync<BigInteger>(contractAddress, balanceOfMessage).ConfigureAwait(false);
            return Web3.Convert.FromWei(balance);
        }

        protected async Task<decimal> GetEthBalance( Web3 web3)
        {
            var balance = await web3.Eth.GetBalance.SendRequestAsync(AccountAddress);
            return Web3.Convert.FromWei(balance);
        }

    }
}
