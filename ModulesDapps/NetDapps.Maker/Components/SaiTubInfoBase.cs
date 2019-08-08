using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using NetDapps.Maker.Contracts.SaiTup.ContractDefinition;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Eth;
using Nethereum.Web3;

namespace NetDapps.Maker.Components
{
    
    public class SaiTubInfoBase : ContractQuery
    {

        public decimal TotalCDPDebt { get; set; }
        public decimal BackingCollateral { get; set; }
        public decimal RawCollateral { get; set; }


        [Inject]
        public HttpClient Client { get; set; }

        protected override void OnInit()
        {
            base.OnInit();
        }

        protected override Task OnInitAsync()
        {
            return GetInfo();
        }

        protected async Task GetInfo()
        {
            var web3 = new Web3(RpcUrl);
            TotalCDPDebt = await GeDin(web3);
            BackingCollateral = await GetAir(web3);
            RawCollateral = await GetPie(web3);
        }

        protected async Task<decimal> GeDin(Web3 web3)
        {
            //Creating a new query handler
            var queryHandler = web3.Eth.GetContractQueryHandler<DinFunction>();
            var din = await queryHandler.QueryAsync<BigInteger>(ContractAddress).ConfigureAwait(false);
            return Web3.Convert.FromWei(din);
        }

        protected async Task<decimal> GetAir(Web3 web3)
        {
            //Creating a new query handler
            var queryHandler = web3.Eth.GetContractQueryHandler<AirFunction>();
            var din = await queryHandler.QueryAsync<BigInteger>(ContractAddress).ConfigureAwait(false);
            return Web3.Convert.FromWei(din);
        }

        protected async Task<decimal> GetPie(Web3 web3)
        {
            //Creating a new query handler
            var queryHandler = web3.Eth.GetContractQueryHandler<AirFunction>();
            var din = await queryHandler.QueryAsync<BigInteger>(ContractAddress).ConfigureAwait(false);
            return Web3.Convert.FromWei(din);
        }

    }
}
