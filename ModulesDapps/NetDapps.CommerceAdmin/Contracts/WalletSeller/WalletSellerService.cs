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
using NetDapps.CommerceAdmin.Contracts.WalletSeller.ContractDefinition;

namespace NetDapps.CommerceAdmin.Contracts.WalletSeller
{
    public partial class WalletSellerService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, WalletSellerDeployment walletSellerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<WalletSellerDeployment>().SendRequestAndWaitForReceiptAsync(walletSellerDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, WalletSellerDeployment walletSellerDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<WalletSellerDeployment>().SendRequestAsync(walletSellerDeployment);
        }

        public static async Task<WalletSellerService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, WalletSellerDeployment walletSellerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, walletSellerDeployment, cancellationTokenSource);
            return new WalletSellerService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public WalletSellerService(Nethereum.Web3.Web3 web3, string contractAddress)
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

        public Task<string> ConfigureRequestAsync(string sysIdAsString, string nameOfPoMain, string nameOfFundingContract)
        {
            var configureFunction = new ConfigureFunction();
                configureFunction.SysIdAsString = sysIdAsString;
                configureFunction.NameOfPoMain = nameOfPoMain;
                configureFunction.NameOfFundingContract = nameOfFundingContract;
            
             return ContractHandler.SendRequestAsync(configureFunction);
        }

        public Task<TransactionReceipt> ConfigureRequestAndWaitForReceiptAsync(string sysIdAsString, string nameOfPoMain, string nameOfFundingContract, CancellationTokenSource cancellationToken = null)
        {
            var configureFunction = new ConfigureFunction();
                configureFunction.SysIdAsString = sysIdAsString;
                configureFunction.NameOfPoMain = nameOfPoMain;
                configureFunction.NameOfFundingContract = nameOfFundingContract;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(configureFunction, cancellationToken);
        }

        public Task<string> ReportSalesOrderCancelFailureRequestAsync(ReportSalesOrderCancelFailureFunction reportSalesOrderCancelFailureFunction)
        {
             return ContractHandler.SendRequestAsync(reportSalesOrderCancelFailureFunction);
        }

        public Task<TransactionReceipt> ReportSalesOrderCancelFailureRequestAndWaitForReceiptAsync(ReportSalesOrderCancelFailureFunction reportSalesOrderCancelFailureFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(reportSalesOrderCancelFailureFunction, cancellationToken);
        }

        public Task<string> ReportSalesOrderCancelFailureRequestAsync(ulong ethPoNumber)
        {
            var reportSalesOrderCancelFailureFunction = new ReportSalesOrderCancelFailureFunction();
                reportSalesOrderCancelFailureFunction.EthPoNumber = ethPoNumber;
            
             return ContractHandler.SendRequestAsync(reportSalesOrderCancelFailureFunction);
        }

        public Task<TransactionReceipt> ReportSalesOrderCancelFailureRequestAndWaitForReceiptAsync(ulong ethPoNumber, CancellationTokenSource cancellationToken = null)
        {
            var reportSalesOrderCancelFailureFunction = new ReportSalesOrderCancelFailureFunction();
                reportSalesOrderCancelFailureFunction.EthPoNumber = ethPoNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(reportSalesOrderCancelFailureFunction, cancellationToken);
        }

        public Task<string> OnCancelPurchaseOrderRequestedRequestAsync(OnCancelPurchaseOrderRequestedFunction onCancelPurchaseOrderRequestedFunction)
        {
             return ContractHandler.SendRequestAsync(onCancelPurchaseOrderRequestedFunction);
        }

        public Task<TransactionReceipt> OnCancelPurchaseOrderRequestedRequestAndWaitForReceiptAsync(OnCancelPurchaseOrderRequestedFunction onCancelPurchaseOrderRequestedFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onCancelPurchaseOrderRequestedFunction, cancellationToken);
        }

        public Task<string> OnCancelPurchaseOrderRequestedRequestAsync(Po po)
        {
            var onCancelPurchaseOrderRequestedFunction = new OnCancelPurchaseOrderRequestedFunction();
                onCancelPurchaseOrderRequestedFunction.Po = po;
            
             return ContractHandler.SendRequestAsync(onCancelPurchaseOrderRequestedFunction);
        }

        public Task<TransactionReceipt> OnCancelPurchaseOrderRequestedRequestAndWaitForReceiptAsync(Po po, CancellationTokenSource cancellationToken = null)
        {
            var onCancelPurchaseOrderRequestedFunction = new OnCancelPurchaseOrderRequestedFunction();
                onCancelPurchaseOrderRequestedFunction.Po = po;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onCancelPurchaseOrderRequestedFunction, cancellationToken);
        }

        public Task<string> ReportSalesOrderNotApprovedRequestAsync(ReportSalesOrderNotApprovedFunction reportSalesOrderNotApprovedFunction)
        {
             return ContractHandler.SendRequestAsync(reportSalesOrderNotApprovedFunction);
        }

        public Task<TransactionReceipt> ReportSalesOrderNotApprovedRequestAndWaitForReceiptAsync(ReportSalesOrderNotApprovedFunction reportSalesOrderNotApprovedFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(reportSalesOrderNotApprovedFunction, cancellationToken);
        }

        public Task<string> ReportSalesOrderNotApprovedRequestAsync(ulong ethPoNumber)
        {
            var reportSalesOrderNotApprovedFunction = new ReportSalesOrderNotApprovedFunction();
                reportSalesOrderNotApprovedFunction.EthPoNumber = ethPoNumber;
            
             return ContractHandler.SendRequestAsync(reportSalesOrderNotApprovedFunction);
        }

        public Task<TransactionReceipt> ReportSalesOrderNotApprovedRequestAndWaitForReceiptAsync(ulong ethPoNumber, CancellationTokenSource cancellationToken = null)
        {
            var reportSalesOrderNotApprovedFunction = new ReportSalesOrderNotApprovedFunction();
                reportSalesOrderNotApprovedFunction.EthPoNumber = ethPoNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(reportSalesOrderNotApprovedFunction, cancellationToken);
        }

        public Task<byte[]> SystemIdQueryAsync(SystemIdFunction systemIdFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SystemIdFunction, byte[]>(systemIdFunction, blockParameter);
        }

        
        public Task<byte[]> SystemIdQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SystemIdFunction, byte[]>(null, blockParameter);
        }

        public Task<string> SetSalesOrderNumberByEthPoNumberRequestAsync(SetSalesOrderNumberByEthPoNumberFunction setSalesOrderNumberByEthPoNumberFunction)
        {
             return ContractHandler.SendRequestAsync(setSalesOrderNumberByEthPoNumberFunction);
        }

        public Task<TransactionReceipt> SetSalesOrderNumberByEthPoNumberRequestAndWaitForReceiptAsync(SetSalesOrderNumberByEthPoNumberFunction setSalesOrderNumberByEthPoNumberFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setSalesOrderNumberByEthPoNumberFunction, cancellationToken);
        }

        public Task<string> SetSalesOrderNumberByEthPoNumberRequestAsync(ulong ethPoNumber, byte[] sellerSalesOrderNumber)
        {
            var setSalesOrderNumberByEthPoNumberFunction = new SetSalesOrderNumberByEthPoNumberFunction();
                setSalesOrderNumberByEthPoNumberFunction.EthPoNumber = ethPoNumber;
                setSalesOrderNumberByEthPoNumberFunction.SellerSalesOrderNumber = sellerSalesOrderNumber;
            
             return ContractHandler.SendRequestAsync(setSalesOrderNumberByEthPoNumberFunction);
        }

        public Task<TransactionReceipt> SetSalesOrderNumberByEthPoNumberRequestAndWaitForReceiptAsync(ulong ethPoNumber, byte[] sellerSalesOrderNumber, CancellationTokenSource cancellationToken = null)
        {
            var setSalesOrderNumberByEthPoNumberFunction = new SetSalesOrderNumberByEthPoNumberFunction();
                setSalesOrderNumberByEthPoNumberFunction.EthPoNumber = ethPoNumber;
                setSalesOrderNumberByEthPoNumberFunction.SellerSalesOrderNumber = sellerSalesOrderNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setSalesOrderNumberByEthPoNumberFunction, cancellationToken);
        }

        public Task<string> PoMainQueryAsync(PoMainFunction poMainFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PoMainFunction, string>(poMainFunction, blockParameter);
        }

        
        public Task<string> PoMainQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PoMainFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> GetTokenBalanceOwnedByThisQueryAsync(GetTokenBalanceOwnedByThisFunction getTokenBalanceOwnedByThisFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetTokenBalanceOwnedByThisFunction, BigInteger>(getTokenBalanceOwnedByThisFunction, blockParameter);
        }

        
        public Task<BigInteger> GetTokenBalanceOwnedByThisQueryAsync(string tokenAddress, BlockParameter blockParameter = null)
        {
            var getTokenBalanceOwnedByThisFunction = new GetTokenBalanceOwnedByThisFunction();
                getTokenBalanceOwnedByThisFunction.TokenAddress = tokenAddress;
            
            return ContractHandler.QueryAsync<GetTokenBalanceOwnedByThisFunction, BigInteger>(getTokenBalanceOwnedByThisFunction, blockParameter);
        }

        public Task<string> FundingContractQueryAsync(FundingContractFunction fundingContractFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FundingContractFunction, string>(fundingContractFunction, blockParameter);
        }

        
        public Task<string> FundingContractQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FundingContractFunction, string>(null, blockParameter);
        }

        public Task<string> RefundPoToBuyerRequestAsync(RefundPoToBuyerFunction refundPoToBuyerFunction)
        {
             return ContractHandler.SendRequestAsync(refundPoToBuyerFunction);
        }

        public Task<TransactionReceipt> RefundPoToBuyerRequestAndWaitForReceiptAsync(RefundPoToBuyerFunction refundPoToBuyerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(refundPoToBuyerFunction, cancellationToken);
        }

        public Task<string> RefundPoToBuyerRequestAsync(ulong ethPoNumber)
        {
            var refundPoToBuyerFunction = new RefundPoToBuyerFunction();
                refundPoToBuyerFunction.EthPoNumber = ethPoNumber;
            
             return ContractHandler.SendRequestAsync(refundPoToBuyerFunction);
        }

        public Task<TransactionReceipt> RefundPoToBuyerRequestAndWaitForReceiptAsync(ulong ethPoNumber, CancellationTokenSource cancellationToken = null)
        {
            var refundPoToBuyerFunction = new RefundPoToBuyerFunction();
                refundPoToBuyerFunction.EthPoNumber = ethPoNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(refundPoToBuyerFunction, cancellationToken);
        }

        public Task<string> ReleasePoFundsToSellerRequestAsync(ReleasePoFundsToSellerFunction releasePoFundsToSellerFunction)
        {
             return ContractHandler.SendRequestAsync(releasePoFundsToSellerFunction);
        }

        public Task<TransactionReceipt> ReleasePoFundsToSellerRequestAndWaitForReceiptAsync(ReleasePoFundsToSellerFunction releasePoFundsToSellerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(releasePoFundsToSellerFunction, cancellationToken);
        }

        public Task<string> ReleasePoFundsToSellerRequestAsync(ulong ethPoNumber)
        {
            var releasePoFundsToSellerFunction = new ReleasePoFundsToSellerFunction();
                releasePoFundsToSellerFunction.EthPoNumber = ethPoNumber;
            
             return ContractHandler.SendRequestAsync(releasePoFundsToSellerFunction);
        }

        public Task<TransactionReceipt> ReleasePoFundsToSellerRequestAndWaitForReceiptAsync(ulong ethPoNumber, CancellationTokenSource cancellationToken = null)
        {
            var releasePoFundsToSellerFunction = new ReleasePoFundsToSellerFunction();
                releasePoFundsToSellerFunction.EthPoNumber = ethPoNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(releasePoFundsToSellerFunction, cancellationToken);
        }

        public Task<string> ReportSalesOrderInvoiceFaultRequestAsync(ReportSalesOrderInvoiceFaultFunction reportSalesOrderInvoiceFaultFunction)
        {
             return ContractHandler.SendRequestAsync(reportSalesOrderInvoiceFaultFunction);
        }

        public Task<TransactionReceipt> ReportSalesOrderInvoiceFaultRequestAndWaitForReceiptAsync(ReportSalesOrderInvoiceFaultFunction reportSalesOrderInvoiceFaultFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(reportSalesOrderInvoiceFaultFunction, cancellationToken);
        }

        public Task<string> ReportSalesOrderInvoiceFaultRequestAsync(ulong ethPoNumber)
        {
            var reportSalesOrderInvoiceFaultFunction = new ReportSalesOrderInvoiceFaultFunction();
                reportSalesOrderInvoiceFaultFunction.EthPoNumber = ethPoNumber;
            
             return ContractHandler.SendRequestAsync(reportSalesOrderInvoiceFaultFunction);
        }

        public Task<TransactionReceipt> ReportSalesOrderInvoiceFaultRequestAndWaitForReceiptAsync(ulong ethPoNumber, CancellationTokenSource cancellationToken = null)
        {
            var reportSalesOrderInvoiceFaultFunction = new ReportSalesOrderInvoiceFaultFunction();
                reportSalesOrderInvoiceFaultFunction.EthPoNumber = ethPoNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(reportSalesOrderInvoiceFaultFunction, cancellationToken);
        }

        public Task<string> OnCreatePurchaseOrderRequestedRequestAsync(OnCreatePurchaseOrderRequestedFunction onCreatePurchaseOrderRequestedFunction)
        {
             return ContractHandler.SendRequestAsync(onCreatePurchaseOrderRequestedFunction);
        }

        public Task<TransactionReceipt> OnCreatePurchaseOrderRequestedRequestAndWaitForReceiptAsync(OnCreatePurchaseOrderRequestedFunction onCreatePurchaseOrderRequestedFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onCreatePurchaseOrderRequestedFunction, cancellationToken);
        }

        public Task<string> OnCreatePurchaseOrderRequestedRequestAsync(Po po)
        {
            var onCreatePurchaseOrderRequestedFunction = new OnCreatePurchaseOrderRequestedFunction();
                onCreatePurchaseOrderRequestedFunction.Po = po;
            
             return ContractHandler.SendRequestAsync(onCreatePurchaseOrderRequestedFunction);
        }

        public Task<TransactionReceipt> OnCreatePurchaseOrderRequestedRequestAndWaitForReceiptAsync(Po po, CancellationTokenSource cancellationToken = null)
        {
            var onCreatePurchaseOrderRequestedFunction = new OnCreatePurchaseOrderRequestedFunction();
                onCreatePurchaseOrderRequestedFunction.Po = po;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onCreatePurchaseOrderRequestedFunction, cancellationToken);
        }

        public Task<string> AddressRegistryQueryAsync(AddressRegistryFunction addressRegistryFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AddressRegistryFunction, string>(addressRegistryFunction, blockParameter);
        }

        
        public Task<string> AddressRegistryQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AddressRegistryFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> GetTokenBalanceOwnedByMsgSenderQueryAsync(GetTokenBalanceOwnedByMsgSenderFunction getTokenBalanceOwnedByMsgSenderFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetTokenBalanceOwnedByMsgSenderFunction, BigInteger>(getTokenBalanceOwnedByMsgSenderFunction, blockParameter);
        }

        
        public Task<BigInteger> GetTokenBalanceOwnedByMsgSenderQueryAsync(string tokenAddress, BlockParameter blockParameter = null)
        {
            var getTokenBalanceOwnedByMsgSenderFunction = new GetTokenBalanceOwnedByMsgSenderFunction();
                getTokenBalanceOwnedByMsgSenderFunction.TokenAddress = tokenAddress;
            
            return ContractHandler.QueryAsync<GetTokenBalanceOwnedByMsgSenderFunction, BigInteger>(getTokenBalanceOwnedByMsgSenderFunction, blockParameter);
        }
    }
}
