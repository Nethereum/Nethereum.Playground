using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Contracts.Standards.ERC20;
using Nethereum.Contracts.Standards.ERC20.TokenList;
using Nethereum.JsonRpc.Client;
using Nethereum.Util;
using Nethereum.Web3;

namespace NetDapps.ERC20Token.Components
{
    public class TokenBalanceBase: ComponentBase
    {
        public string RpcUrl { get; set; }
        public string ContractAddresses { get; set; }
        public string AccountAddresses { get; set; } 
        public List<TokenOwnerInfo> TokenOwnerBalances { get; set; }

        private List<Token> Tokens { get; set; }

        [Inject]
        public HttpClient Client { get; set; }

        protected override void OnInitialized()
        {
            RpcUrl = "https://mainnet.infura.io/v3/ddd5ed15e8d443e295b696c0d07c8b02";
            ContractAddresses = "0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606eB48, 0xdAC17F958D2ee523a2206206994597C13D831ec7";
            AccountAddresses = "0xde0B295669a9FD93d5F28D9Ec85E40f4cb697BAe, 0x0a59649758aa4d66e25f08dd01271e891fe52199";
            base.OnInitialized();
        }

        protected async Task GetTokenBalance()
        {
            var web3 = new Web3(RpcUrl);
            if (Tokens == null)
            {
                Tokens = await new TokenListService().LoadFromUrl(TokenListSources.ONE_INCH);
            }

            var contractAddressesArray = ContractAddresses.Split(',');
            contractAddressesArray = contractAddressesArray.Select(x => x.Trim()).ToArray();

            var accountAddressesArray = AccountAddresses.Split(',');
            accountAddressesArray = accountAddressesArray.Select(x => x.Trim()).ToArray();
            //Creating a new query handler
            var tokenOwnerBalances = await web3.Eth.ERC20.GetAllTokenBalancesUsingMultiCallAsync(accountAddressesArray, contractAddressesArray);
            TokenOwnerBalances = new List<TokenOwnerInfo>();
            foreach (var tokenOwnerBalance in tokenOwnerBalances)
            {
                var tokenOwnerInfo = TokenOwnerBalances.FirstOrDefault(x =>
                    x.Token.Address.IsTheSameAddress(tokenOwnerBalance.ContractAddress));
                if (tokenOwnerInfo == null)
                {
                    tokenOwnerInfo = new TokenOwnerInfo();
                    tokenOwnerInfo.Token =
                        Tokens.FirstOrDefault(x => x.Address.IsTheSameAddress(tokenOwnerBalance.ContractAddress));
                    if (tokenOwnerInfo.Token == null)
                    {
                        tokenOwnerInfo.Token = new Token() {Address = tokenOwnerBalance.ContractAddress};
                    }
                    TokenOwnerBalances.Add(tokenOwnerInfo);
                }
                tokenOwnerInfo.OwnersBalances.Add(tokenOwnerBalance);
            }
        }
    }
}
