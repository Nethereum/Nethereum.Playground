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
using NetDapps.CommerceAdmin.Contracts.BusinessPartnerStorage;

namespace NetDapps.CommerceAdmin.Components
{
    public class CommerceAdminBase : ComponentBase
    {
        public string WalletBuyerAddress { get; set; }
        public string WalletBuyerBalance { get; set; }
        public string WalletBuyerDesc { get; set; }

        public string WalletSellerAddress { get; set; }
        public string WalletSellerBalance { get; set; }
        public string WalletSellerDesc { get; set; }

        public string FundingAddress { get; set; }
        public string FundingBalance { get; set; }
        public string FundingDesc { get; set; }

        public string TokenAddress { get; set; }
        public string TokenSymbol { get; set; }
        public string RpcUrl { get; set; }

        [Inject]
        public HttpClient Client { get; set; }

        private int _tokenDecimals;

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

            WalletBuyerDesc = "";
            WalletSellerDesc = "";
            FundingDesc = "";

            base.OnInitialized();
        }

        protected async Task RefreshAll()
        {
            await GetTokenDetails();
            await GetAllBalances();
            await GetAllDescs();
        }

        private async Task GetAllBalances() 
        {
            var web3 = new Web3(RpcUrl);
            
            var wbs = new WalletBuyerService(web3, WalletBuyerAddress);
            var wbsBal = await wbs.GetTokenBalanceOwnedByThisQueryAsync(TokenAddress);
            WalletBuyerBalance = wbsBal.ToString();

            var fs = new FundingService(web3, FundingAddress);
            var fsBal = await fs.GetBalanceOfThisQueryAsync(TokenAddress);
            FundingBalance = fsBal.ToString();

            var wss = new WalletSellerService(web3, WalletSellerAddress);
            var wssBal = await wss.GetTokenBalanceOwnedByThisQueryAsync(TokenAddress);
            WalletSellerBalance = wssBal.ToString();

        }

        private async Task GetAllDescs()
        {
            await Task.Delay(1);
            WalletBuyerDesc = "Omni Consumer Products";
            WalletSellerDesc = "Solyent Corporation";
            FundingDesc = "";
        }

        private async Task GetTokenDetails()
        {
            var web3 = new Web3(RpcUrl);

            await Task.Delay(1);
            TokenSymbol = "DAI";
            _tokenDecimals = 2;
        }
    }
}
