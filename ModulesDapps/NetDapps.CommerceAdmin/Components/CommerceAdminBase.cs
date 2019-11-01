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
using NetDapps.CommerceAdmin.Contracts.Erc20;
using NetDapps.CommerceAdmin.Contracts.WalletBuyer;
using NetDapps.CommerceAdmin.Contracts.Funding;
using NetDapps.CommerceAdmin.Contracts.WalletSeller;
using NetDapps.CommerceAdmin.Contracts.BusinessPartnerStorage;

namespace NetDapps.CommerceAdmin.Components
{
    public class CommerceAdminBase : ComponentBase
    {
        public string RpcUrl { get; set; }
        public string WalletBuyerAddress { get; set; }
        public string WalletBuyerBalance { get; private set; }
        public string WalletBuyerDesc { get; private set; }

        public string WalletSellerAddress { get; set; }
        public string WalletSellerBalance { get; private set; }
        public string WalletSellerDesc { get; private set; }

        public string FundingAddress { get; set; }
        public string FundingBalance { get; private set; }
        public string FundingDesc { get; private set; }

        public string TokenAddress { get; set; }
        public string TokenSymbol { get; private set; }
        public string TokenName { get; private set; }
        public int TokenDecimals { get; private set; }

        public string BusinessPartnersContractAddress { get; set; }

        [Inject]
        public HttpClient Client { get; set; }

        protected override void OnInitialized()
        {
            RpcUrl = "https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c";
            WalletBuyerAddress = "0x3a61a411F11444768a6e8CCf9AE242180098bBF7";
            WalletSellerAddress = "0x40e8E2E98908d8E5B32CAF2C9a637d6911b50Ed8";
            FundingAddress = "0x3c474Fc3995AaF851A3d8803A685D358E3BedE35";
            TokenAddress = "0x7bbd0d72e59ede7f2a65abde9539aa006682c741";
            BusinessPartnersContractAddress = "0x9e1bb06a16bc8e98f3b61df667bdf785ead3c124";
            WalletBuyerBalance = "0";
            WalletSellerBalance = "0";
            FundingBalance = "0";
            WalletBuyerDesc = "";
            WalletSellerDesc = "";
            FundingDesc = "";
            TokenSymbol = "";
            TokenName = "";
            TokenDecimals = 0;

            base.OnInitialized();
        }

        protected async Task RefreshAll()
        {
            string waiting = "...";
            WalletBuyerBalance = waiting;
            WalletSellerBalance = waiting;
            FundingBalance = waiting;
            WalletBuyerDesc = waiting;
            WalletSellerDesc = waiting;
            FundingDesc = waiting;

            await GetTokenDetails();
            await GetAllBalances();
            await GetAllDescs();
        }

        private async Task GetAllBalances()
        {
            var web3 = new Web3(RpcUrl);

            var wbs = new WalletBuyerService(web3, WalletBuyerAddress);
            var wbsBal = await wbs.GetTokenBalanceOwnedByThisQueryAsync(TokenAddress);
            WalletBuyerBalance = $"{wbsBal.ToString()} {TokenSymbol}";

            var fs = new FundingService(web3, FundingAddress);
            var fsBal = await fs.GetBalanceOfThisQueryAsync(TokenAddress);
            FundingBalance = $"{fsBal.ToString()} {TokenSymbol}";

            var wss = new WalletSellerService(web3, WalletSellerAddress);
            var wssBal = await wss.GetTokenBalanceOwnedByThisQueryAsync(TokenAddress);
            WalletSellerBalance = $"{wssBal.ToString()} {TokenSymbol}";
        }

        private async Task GetAllDescs()
        {
            var web3 = new Web3(RpcUrl);

            // Buyer 
            // use wallet to get system id, then bp storage to get system desc
            var wbs = new WalletBuyerService(web3, WalletBuyerAddress);
            var wbsSysId = await wbs.SystemIdQueryAsync();
            var bpss = new BusinessPartnerStorageService(web3, BusinessPartnersContractAddress);
            WalletBuyerDesc = await bpss.GetSystemDescriptionQueryAsync(wbsSysId);
            
            // Seller
            var wss = new WalletSellerService(web3, WalletSellerAddress);
            var wssSysId = await wss.SystemIdQueryAsync();
            WalletSellerDesc = await bpss.GetSystemDescriptionQueryAsync(wssSysId);

            // Funding doesnt have an owner            
            FundingDesc = "";
        }

        private async Task GetTokenDetails()
        {
            var web3 = new Web3(RpcUrl);
            var contractHandler = web3.Eth.GetContractHandler(TokenAddress);
            TokenSymbol = await contractHandler.QueryAsync<SymbolFunction, string>();            
            TokenName = await contractHandler.QueryAsync<NameFunction, string>();
            TokenDecimals = await contractHandler.QueryAsync<DecimalsFunction, int>();                        
        }
    }
}
