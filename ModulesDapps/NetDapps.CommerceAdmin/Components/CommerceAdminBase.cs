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
using NetDapps.CommerceAdmin.Contracts.WalletBuyer;
using NetDapps.CommerceAdmin.Contracts.Funding;
using NetDapps.CommerceAdmin.Contracts.WalletSeller;

namespace NetDapps.CommerceAdmin.Components
{
    public class CommerceAdminBase : ComponentBase
    {
        public string WalletBuyerAddress { get; set; }
        public string WalletBuyerBalance { get; set; }
        public string WalletSellerAddress { get; set; }
        public string WalletSellerBalance { get; set; }
        public string FundingAddress { get; set; }
        public string FundingBalance { get; set; }
        public string TokenAddress { get; set; }
        public string TokenSymbol { get; set; }
        public string RpcUrl { get; set; }

        [Inject]
        public HttpClient Client { get; set; }

        [Function("balanceOf", "uint256")]
        public class BalanceOfFunction : FunctionMessage
        {
            [Nethereum.ABI.FunctionEncoding.Attributes.Parameter("address", "_owner", 1)]
            public string Owner { get; set; }
        }

        protected override void OnInitialized()
        {
            RpcUrl = "https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c";
            WalletBuyerAddress = "0x3a61a411F11444768a6e8CCf9AE242180098bBF7";
            WalletSellerAddress = "0x40e8E2E98908d8E5B32CAF2C9a637d6911b50Ed8";
            FundingAddress = "0x3c474Fc3995AaF851A3d8803A685D358E3BedE35";
            TokenAddress = "0x7bbd0d72e59ede7f2a65abde9539aa006682c741";

            WalletBuyerBalance = "0";
            WalletSellerBalance = "0";
            FundingBalance = "0";

            base.OnInitialized();
        }

        protected async Task GetTokenBalance()
        {
            //WalletBuyerBalance = "...";
            //WalletSellerBalance = "...";
            //FundingBalance = "...";

            var web3 = new Web3(RpcUrl);
            //var balanceOfMessage = new BalanceOfFunction() { Owner = AccountAddress };

            ////Creating a new query handler
            //var queryHandler = web3.Eth.GetContractQueryHandler<BalanceOfFunction>();

            ////Querying the Maker smart contract https://etherscan.io/address/0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2
            //var balance = await queryHandler.QueryAsync<BigInteger>(ContractAddress, balanceOfMessage).ConfigureAwait(false);
            //Balance = Web3.Convert.FromWei(balance);

            WalletBuyerBalance = "1700.00 DAI";
            WalletSellerBalance = "15400.00 DAI";
            FundingBalance = "300.00 DAI";
        }

    }
}
