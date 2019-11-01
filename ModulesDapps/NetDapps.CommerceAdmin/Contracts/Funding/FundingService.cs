using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using NetDapps.CommerceAdmin.Contracts.Funding.ContractDefinition;

namespace NetDapps.CommerceAdmin.Contracts.Funding
{
    public partial class FundingService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, FundingDeployment fundingDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<FundingDeployment>().SendRequestAndWaitForReceiptAsync(fundingDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, FundingDeployment fundingDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<FundingDeployment>().SendRequestAsync(fundingDeployment);
        }

        public static async Task<FundingService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, FundingDeployment fundingDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, fundingDeployment, cancellationTokenSource);
            return new FundingService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public FundingService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> ConfigureRequestAsync(ConfigureFunction configureFunction)
        {
             return ContractHandler.SendRequestAsync(configureFunction);
        }

        public Task<TransactionReceipt> ConfigureRequestAndWaitForReceiptAsync(ConfigureFunction configureFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(configureFunction, cancellationToken);
        }

        public Task<string> ConfigureRequestAsync(string nameOfPoStorage, string nameOfPoMain, string nameOfBusinessPartnerStorage)
        {
            var configureFunction = new ConfigureFunction();
                configureFunction.NameOfPoStorage = nameOfPoStorage;
                configureFunction.NameOfPoMain = nameOfPoMain;
                configureFunction.NameOfBusinessPartnerStorage = nameOfBusinessPartnerStorage;
            
             return ContractHandler.SendRequestAsync(configureFunction);
        }

        public Task<TransactionReceipt> ConfigureRequestAndWaitForReceiptAsync(string nameOfPoStorage, string nameOfPoMain, string nameOfBusinessPartnerStorage, CancellationTokenSource cancellationToken = null)
        {
            var configureFunction = new ConfigureFunction();
                configureFunction.NameOfPoStorage = nameOfPoStorage;
                configureFunction.NameOfPoMain = nameOfPoMain;
                configureFunction.NameOfBusinessPartnerStorage = nameOfBusinessPartnerStorage;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(configureFunction, cancellationToken);
        }

        public Task<string> TransferOutFundsForPoToSellerRequestAsync(TransferOutFundsForPoToSellerFunction transferOutFundsForPoToSellerFunction)
        {
             return ContractHandler.SendRequestAsync(transferOutFundsForPoToSellerFunction);
        }

        public Task<TransactionReceipt> TransferOutFundsForPoToSellerRequestAndWaitForReceiptAsync(TransferOutFundsForPoToSellerFunction transferOutFundsForPoToSellerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOutFundsForPoToSellerFunction, cancellationToken);
        }

        public Task<string> TransferOutFundsForPoToSellerRequestAsync(ulong poNumber)
        {
            var transferOutFundsForPoToSellerFunction = new TransferOutFundsForPoToSellerFunction();
                transferOutFundsForPoToSellerFunction.PoNumber = poNumber;
            
             return ContractHandler.SendRequestAsync(transferOutFundsForPoToSellerFunction);
        }

        public Task<TransactionReceipt> TransferOutFundsForPoToSellerRequestAndWaitForReceiptAsync(ulong poNumber, CancellationTokenSource cancellationToken = null)
        {
            var transferOutFundsForPoToSellerFunction = new TransferOutFundsForPoToSellerFunction();
                transferOutFundsForPoToSellerFunction.PoNumber = poNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOutFundsForPoToSellerFunction, cancellationToken);
        }

        public Task<string> SwitchDebugOnRequestAsync(SwitchDebugOnFunction switchDebugOnFunction)
        {
             return ContractHandler.SendRequestAsync(switchDebugOnFunction);
        }

        public Task<string> SwitchDebugOnRequestAsync()
        {
             return ContractHandler.SendRequestAsync<SwitchDebugOnFunction>();
        }

        public Task<TransactionReceipt> SwitchDebugOnRequestAndWaitForReceiptAsync(SwitchDebugOnFunction switchDebugOnFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(switchDebugOnFunction, cancellationToken);
        }

        public Task<TransactionReceipt> SwitchDebugOnRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<SwitchDebugOnFunction>(null, cancellationToken);
        }

        public Task<string> BusinessPartnerStorageQueryAsync(BusinessPartnerStorageFunction businessPartnerStorageFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BusinessPartnerStorageFunction, string>(businessPartnerStorageFunction, blockParameter);
        }

        
        public Task<string> BusinessPartnerStorageQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BusinessPartnerStorageFunction, string>(null, blockParameter);
        }

        public Task<string> PoStorageQueryAsync(PoStorageFunction poStorageFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PoStorageFunction, string>(poStorageFunction, blockParameter);
        }

        
        public Task<string> PoStorageQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PoStorageFunction, string>(null, blockParameter);
        }

        public Task<string> PoMainContractAddressQueryAsync(PoMainContractAddressFunction poMainContractAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PoMainContractAddressFunction, string>(poMainContractAddressFunction, blockParameter);
        }

        
        public Task<string> PoMainContractAddressQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PoMainContractAddressFunction, string>(null, blockParameter);
        }

        public Task<string> SwitchDebugOffRequestAsync(SwitchDebugOffFunction switchDebugOffFunction)
        {
             return ContractHandler.SendRequestAsync(switchDebugOffFunction);
        }

        public Task<string> SwitchDebugOffRequestAsync()
        {
             return ContractHandler.SendRequestAsync<SwitchDebugOffFunction>();
        }

        public Task<TransactionReceipt> SwitchDebugOffRequestAndWaitForReceiptAsync(SwitchDebugOffFunction switchDebugOffFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(switchDebugOffFunction, cancellationToken);
        }

        public Task<TransactionReceipt> SwitchDebugOffRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<SwitchDebugOffFunction>(null, cancellationToken);
        }

        public Task<BigInteger> GetBalanceOfThisQueryAsync(GetBalanceOfThisFunction getBalanceOfThisFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetBalanceOfThisFunction, BigInteger>(getBalanceOfThisFunction, blockParameter);
        }

        
        public Task<BigInteger> GetBalanceOfThisQueryAsync(string tokenAddress, BlockParameter blockParameter = null)
        {
            var getBalanceOfThisFunction = new GetBalanceOfThisFunction();
                getBalanceOfThisFunction.TokenAddress = tokenAddress;
            
            return ContractHandler.QueryAsync<GetBalanceOfThisFunction, BigInteger>(getBalanceOfThisFunction, blockParameter);
        }

        public Task<bool> IsDebugOnQueryAsync(IsDebugOnFunction isDebugOnFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsDebugOnFunction, bool>(isDebugOnFunction, blockParameter);
        }

        
        public Task<bool> IsDebugOnQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsDebugOnFunction, bool>(null, blockParameter);
        }

        public Task<bool> GetPoFundingStatusQueryAsync(GetPoFundingStatusFunction getPoFundingStatusFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetPoFundingStatusFunction, bool>(getPoFundingStatusFunction, blockParameter);
        }

        
        public Task<bool> GetPoFundingStatusQueryAsync(ulong poNumber, BlockParameter blockParameter = null)
        {
            var getPoFundingStatusFunction = new GetPoFundingStatusFunction();
                getPoFundingStatusFunction.PoNumber = poNumber;
            
            return ContractHandler.QueryAsync<GetPoFundingStatusFunction, bool>(getPoFundingStatusFunction, blockParameter);
        }

        public Task<string> TransferOutFundsForPoToBuyerRequestAsync(TransferOutFundsForPoToBuyerFunction transferOutFundsForPoToBuyerFunction)
        {
             return ContractHandler.SendRequestAsync(transferOutFundsForPoToBuyerFunction);
        }

        public Task<TransactionReceipt> TransferOutFundsForPoToBuyerRequestAndWaitForReceiptAsync(TransferOutFundsForPoToBuyerFunction transferOutFundsForPoToBuyerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOutFundsForPoToBuyerFunction, cancellationToken);
        }

        public Task<string> TransferOutFundsForPoToBuyerRequestAsync(ulong poNumber)
        {
            var transferOutFundsForPoToBuyerFunction = new TransferOutFundsForPoToBuyerFunction();
                transferOutFundsForPoToBuyerFunction.PoNumber = poNumber;
            
             return ContractHandler.SendRequestAsync(transferOutFundsForPoToBuyerFunction);
        }

        public Task<TransactionReceipt> TransferOutFundsForPoToBuyerRequestAndWaitForReceiptAsync(ulong poNumber, CancellationTokenSource cancellationToken = null)
        {
            var transferOutFundsForPoToBuyerFunction = new TransferOutFundsForPoToBuyerFunction();
                transferOutFundsForPoToBuyerFunction.PoNumber = poNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOutFundsForPoToBuyerFunction, cancellationToken);
        }

        public Task<string> TransferInFundsForPoFromBuyerRequestAsync(TransferInFundsForPoFromBuyerFunction transferInFundsForPoFromBuyerFunction)
        {
             return ContractHandler.SendRequestAsync(transferInFundsForPoFromBuyerFunction);
        }

        public Task<TransactionReceipt> TransferInFundsForPoFromBuyerRequestAndWaitForReceiptAsync(TransferInFundsForPoFromBuyerFunction transferInFundsForPoFromBuyerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferInFundsForPoFromBuyerFunction, cancellationToken);
        }

        public Task<string> TransferInFundsForPoFromBuyerRequestAsync(ulong poNumber)
        {
            var transferInFundsForPoFromBuyerFunction = new TransferInFundsForPoFromBuyerFunction();
                transferInFundsForPoFromBuyerFunction.PoNumber = poNumber;
            
             return ContractHandler.SendRequestAsync(transferInFundsForPoFromBuyerFunction);
        }

        public Task<TransactionReceipt> TransferInFundsForPoFromBuyerRequestAndWaitForReceiptAsync(ulong poNumber, CancellationTokenSource cancellationToken = null)
        {
            var transferInFundsForPoFromBuyerFunction = new TransferInFundsForPoFromBuyerFunction();
                transferInFundsForPoFromBuyerFunction.PoNumber = poNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferInFundsForPoFromBuyerFunction, cancellationToken);
        }

        public Task<string> AddressRegistryQueryAsync(AddressRegistryFunction addressRegistryFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AddressRegistryFunction, string>(addressRegistryFunction, blockParameter);
        }

        
        public Task<string> AddressRegistryQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AddressRegistryFunction, string>(null, blockParameter);
        }
    }
}
