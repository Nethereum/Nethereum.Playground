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
using NetDapps.CommerceAdmin.Contracts.WalletBuyer.ContractDefinition;

namespace NetDapps.CommerceAdmin.Contracts.WalletBuyer
{
    public partial class WalletBuyerService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, WalletBuyerDeployment walletBuyerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<WalletBuyerDeployment>().SendRequestAndWaitForReceiptAsync(walletBuyerDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, WalletBuyerDeployment walletBuyerDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<WalletBuyerDeployment>().SendRequestAsync(walletBuyerDeployment);
        }

        public static async Task<WalletBuyerService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, WalletBuyerDeployment walletBuyerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, walletBuyerDeployment, cancellationTokenSource);
            return new WalletBuyerService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public WalletBuyerService(Nethereum.Web3.Web3 web3, string contractAddress)
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

        public Task<string> OnSalesOrderNotApprovedRequestAsync(OnSalesOrderNotApprovedFunction onSalesOrderNotApprovedFunction)
        {
             return ContractHandler.SendRequestAsync(onSalesOrderNotApprovedFunction);
        }

        public Task<TransactionReceipt> OnSalesOrderNotApprovedRequestAndWaitForReceiptAsync(OnSalesOrderNotApprovedFunction onSalesOrderNotApprovedFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onSalesOrderNotApprovedFunction, cancellationToken);
        }

        public Task<string> OnSalesOrderNotApprovedRequestAsync(Po po)
        {
            var onSalesOrderNotApprovedFunction = new OnSalesOrderNotApprovedFunction();
                onSalesOrderNotApprovedFunction.Po = po;
            
             return ContractHandler.SendRequestAsync(onSalesOrderNotApprovedFunction);
        }

        public Task<TransactionReceipt> OnSalesOrderNotApprovedRequestAndWaitForReceiptAsync(Po po, CancellationTokenSource cancellationToken = null)
        {
            var onSalesOrderNotApprovedFunction = new OnSalesOrderNotApprovedFunction();
                onSalesOrderNotApprovedFunction.Po = po;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onSalesOrderNotApprovedFunction, cancellationToken);
        }

        public Task<string> CancelPurchaseOrderRequestAsync(CancelPurchaseOrderFunction cancelPurchaseOrderFunction)
        {
             return ContractHandler.SendRequestAsync(cancelPurchaseOrderFunction);
        }

        public Task<TransactionReceipt> CancelPurchaseOrderRequestAndWaitForReceiptAsync(CancelPurchaseOrderFunction cancelPurchaseOrderFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelPurchaseOrderFunction, cancellationToken);
        }

        public Task<string> CancelPurchaseOrderRequestAsync(ulong ethPoNumber)
        {
            var cancelPurchaseOrderFunction = new CancelPurchaseOrderFunction();
                cancelPurchaseOrderFunction.EthPoNumber = ethPoNumber;
            
             return ContractHandler.SendRequestAsync(cancelPurchaseOrderFunction);
        }

        public Task<TransactionReceipt> CancelPurchaseOrderRequestAndWaitForReceiptAsync(ulong ethPoNumber, CancellationTokenSource cancellationToken = null)
        {
            var cancelPurchaseOrderFunction = new CancelPurchaseOrderFunction();
                cancelPurchaseOrderFunction.EthPoNumber = ethPoNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelPurchaseOrderFunction, cancellationToken);
        }

        public Task<byte[]> SystemIdQueryAsync(SystemIdFunction systemIdFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SystemIdFunction, byte[]>(systemIdFunction, blockParameter);
        }

        
        public Task<byte[]> SystemIdQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SystemIdFunction, byte[]>(null, blockParameter);
        }

        public Task<string> OnPurchasePaymentMadeOkRequestAsync(OnPurchasePaymentMadeOkFunction onPurchasePaymentMadeOkFunction)
        {
             return ContractHandler.SendRequestAsync(onPurchasePaymentMadeOkFunction);
        }

        public Task<TransactionReceipt> OnPurchasePaymentMadeOkRequestAndWaitForReceiptAsync(OnPurchasePaymentMadeOkFunction onPurchasePaymentMadeOkFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onPurchasePaymentMadeOkFunction, cancellationToken);
        }

        public Task<string> OnPurchasePaymentMadeOkRequestAsync(Po po)
        {
            var onPurchasePaymentMadeOkFunction = new OnPurchasePaymentMadeOkFunction();
                onPurchasePaymentMadeOkFunction.Po = po;
            
             return ContractHandler.SendRequestAsync(onPurchasePaymentMadeOkFunction);
        }

        public Task<TransactionReceipt> OnPurchasePaymentMadeOkRequestAndWaitForReceiptAsync(Po po, CancellationTokenSource cancellationToken = null)
        {
            var onPurchasePaymentMadeOkFunction = new OnPurchasePaymentMadeOkFunction();
                onPurchasePaymentMadeOkFunction.Po = po;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onPurchasePaymentMadeOkFunction, cancellationToken);
        }

        public Task<string> PoMainQueryAsync(PoMainFunction poMainFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PoMainFunction, string>(poMainFunction, blockParameter);
        }

        
        public Task<string> PoMainQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PoMainFunction, string>(null, blockParameter);
        }

        public Task<string> OnPurchaseRefundMadeOkRequestAsync(OnPurchaseRefundMadeOkFunction onPurchaseRefundMadeOkFunction)
        {
             return ContractHandler.SendRequestAsync(onPurchaseRefundMadeOkFunction);
        }

        public Task<TransactionReceipt> OnPurchaseRefundMadeOkRequestAndWaitForReceiptAsync(OnPurchaseRefundMadeOkFunction onPurchaseRefundMadeOkFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onPurchaseRefundMadeOkFunction, cancellationToken);
        }

        public Task<string> OnPurchaseRefundMadeOkRequestAsync(Po po)
        {
            var onPurchaseRefundMadeOkFunction = new OnPurchaseRefundMadeOkFunction();
                onPurchaseRefundMadeOkFunction.Po = po;
            
             return ContractHandler.SendRequestAsync(onPurchaseRefundMadeOkFunction);
        }

        public Task<TransactionReceipt> OnPurchaseRefundMadeOkRequestAndWaitForReceiptAsync(Po po, CancellationTokenSource cancellationToken = null)
        {
            var onPurchaseRefundMadeOkFunction = new OnPurchaseRefundMadeOkFunction();
                onPurchaseRefundMadeOkFunction.Po = po;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onPurchaseRefundMadeOkFunction, cancellationToken);
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

        public Task<string> CreatePurchaseOrderRequestAsync(CreatePurchaseOrderFunction createPurchaseOrderFunction)
        {
             return ContractHandler.SendRequestAsync(createPurchaseOrderFunction);
        }

        public Task<TransactionReceipt> CreatePurchaseOrderRequestAndWaitForReceiptAsync(CreatePurchaseOrderFunction createPurchaseOrderFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createPurchaseOrderFunction, cancellationToken);
        }

        public Task<string> CreatePurchaseOrderRequestAsync(Po po)
        {
            var createPurchaseOrderFunction = new CreatePurchaseOrderFunction();
                createPurchaseOrderFunction.Po = po;
            
             return ContractHandler.SendRequestAsync(createPurchaseOrderFunction);
        }

        public Task<TransactionReceipt> CreatePurchaseOrderRequestAndWaitForReceiptAsync(Po po, CancellationTokenSource cancellationToken = null)
        {
            var createPurchaseOrderFunction = new CreatePurchaseOrderFunction();
                createPurchaseOrderFunction.Po = po;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createPurchaseOrderFunction, cancellationToken);
        }

        public Task<string> CreatePurchaseOrderTestRequestAsync(CreatePurchaseOrderTestFunction createPurchaseOrderTestFunction)
        {
             return ContractHandler.SendRequestAsync(createPurchaseOrderTestFunction);
        }

        public Task<TransactionReceipt> CreatePurchaseOrderTestRequestAndWaitForReceiptAsync(CreatePurchaseOrderTestFunction createPurchaseOrderTestFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createPurchaseOrderTestFunction, cancellationToken);
        }

        public Task<string> CreatePurchaseOrderTestRequestAsync(string tokenAddress)
        {
            var createPurchaseOrderTestFunction = new CreatePurchaseOrderTestFunction();
                createPurchaseOrderTestFunction.TokenAddress = tokenAddress;
            
             return ContractHandler.SendRequestAsync(createPurchaseOrderTestFunction);
        }

        public Task<TransactionReceipt> CreatePurchaseOrderTestRequestAndWaitForReceiptAsync(string tokenAddress, CancellationTokenSource cancellationToken = null)
        {
            var createPurchaseOrderTestFunction = new CreatePurchaseOrderTestFunction();
                createPurchaseOrderTestFunction.TokenAddress = tokenAddress;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createPurchaseOrderTestFunction, cancellationToken);
        }

        public Task<string> OnSalesOrderCancelFailureRequestAsync(OnSalesOrderCancelFailureFunction onSalesOrderCancelFailureFunction)
        {
             return ContractHandler.SendRequestAsync(onSalesOrderCancelFailureFunction);
        }

        public Task<TransactionReceipt> OnSalesOrderCancelFailureRequestAndWaitForReceiptAsync(OnSalesOrderCancelFailureFunction onSalesOrderCancelFailureFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onSalesOrderCancelFailureFunction, cancellationToken);
        }

        public Task<string> OnSalesOrderCancelFailureRequestAsync(Po po)
        {
            var onSalesOrderCancelFailureFunction = new OnSalesOrderCancelFailureFunction();
                onSalesOrderCancelFailureFunction.Po = po;
            
             return ContractHandler.SendRequestAsync(onSalesOrderCancelFailureFunction);
        }

        public Task<TransactionReceipt> OnSalesOrderCancelFailureRequestAndWaitForReceiptAsync(Po po, CancellationTokenSource cancellationToken = null)
        {
            var onSalesOrderCancelFailureFunction = new OnSalesOrderCancelFailureFunction();
                onSalesOrderCancelFailureFunction.Po = po;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onSalesOrderCancelFailureFunction, cancellationToken);
        }

        public Task<string> OnSalesOrderInvoiceFaultRequestAsync(OnSalesOrderInvoiceFaultFunction onSalesOrderInvoiceFaultFunction)
        {
             return ContractHandler.SendRequestAsync(onSalesOrderInvoiceFaultFunction);
        }

        public Task<TransactionReceipt> OnSalesOrderInvoiceFaultRequestAndWaitForReceiptAsync(OnSalesOrderInvoiceFaultFunction onSalesOrderInvoiceFaultFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onSalesOrderInvoiceFaultFunction, cancellationToken);
        }

        public Task<string> OnSalesOrderInvoiceFaultRequestAsync(Po po)
        {
            var onSalesOrderInvoiceFaultFunction = new OnSalesOrderInvoiceFaultFunction();
                onSalesOrderInvoiceFaultFunction.Po = po;
            
             return ContractHandler.SendRequestAsync(onSalesOrderInvoiceFaultFunction);
        }

        public Task<TransactionReceipt> OnSalesOrderInvoiceFaultRequestAndWaitForReceiptAsync(Po po, CancellationTokenSource cancellationToken = null)
        {
            var onSalesOrderInvoiceFaultFunction = new OnSalesOrderInvoiceFaultFunction();
                onSalesOrderInvoiceFaultFunction.Po = po;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onSalesOrderInvoiceFaultFunction, cancellationToken);
        }

        public Task<string> FundingContractQueryAsync(FundingContractFunction fundingContractFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FundingContractFunction, string>(fundingContractFunction, blockParameter);
        }

        
        public Task<string> FundingContractQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FundingContractFunction, string>(null, blockParameter);
        }

        public Task<string> OnPurchasePaymentFailedRequestAsync(OnPurchasePaymentFailedFunction onPurchasePaymentFailedFunction)
        {
             return ContractHandler.SendRequestAsync(onPurchasePaymentFailedFunction);
        }

        public Task<TransactionReceipt> OnPurchasePaymentFailedRequestAndWaitForReceiptAsync(OnPurchasePaymentFailedFunction onPurchasePaymentFailedFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onPurchasePaymentFailedFunction, cancellationToken);
        }

        public Task<string> OnPurchasePaymentFailedRequestAsync(Po po)
        {
            var onPurchasePaymentFailedFunction = new OnPurchasePaymentFailedFunction();
                onPurchasePaymentFailedFunction.Po = po;
            
             return ContractHandler.SendRequestAsync(onPurchasePaymentFailedFunction);
        }

        public Task<TransactionReceipt> OnPurchasePaymentFailedRequestAndWaitForReceiptAsync(Po po, CancellationTokenSource cancellationToken = null)
        {
            var onPurchasePaymentFailedFunction = new OnPurchasePaymentFailedFunction();
                onPurchasePaymentFailedFunction.Po = po;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onPurchasePaymentFailedFunction, cancellationToken);
        }

        public Task<string> OnPurchaseRefundFailedRequestAsync(OnPurchaseRefundFailedFunction onPurchaseRefundFailedFunction)
        {
             return ContractHandler.SendRequestAsync(onPurchaseRefundFailedFunction);
        }

        public Task<TransactionReceipt> OnPurchaseRefundFailedRequestAndWaitForReceiptAsync(OnPurchaseRefundFailedFunction onPurchaseRefundFailedFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onPurchaseRefundFailedFunction, cancellationToken);
        }

        public Task<string> OnPurchaseRefundFailedRequestAsync(Po po)
        {
            var onPurchaseRefundFailedFunction = new OnPurchaseRefundFailedFunction();
                onPurchaseRefundFailedFunction.Po = po;
            
             return ContractHandler.SendRequestAsync(onPurchaseRefundFailedFunction);
        }

        public Task<TransactionReceipt> OnPurchaseRefundFailedRequestAndWaitForReceiptAsync(Po po, CancellationTokenSource cancellationToken = null)
        {
            var onPurchaseRefundFailedFunction = new OnPurchaseRefundFailedFunction();
                onPurchaseRefundFailedFunction.Po = po;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onPurchaseRefundFailedFunction, cancellationToken);
        }

        public Task<string> OnPurchaseUpdatedWithSalesOrderRequestAsync(OnPurchaseUpdatedWithSalesOrderFunction onPurchaseUpdatedWithSalesOrderFunction)
        {
             return ContractHandler.SendRequestAsync(onPurchaseUpdatedWithSalesOrderFunction);
        }

        public Task<TransactionReceipt> OnPurchaseUpdatedWithSalesOrderRequestAndWaitForReceiptAsync(OnPurchaseUpdatedWithSalesOrderFunction onPurchaseUpdatedWithSalesOrderFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onPurchaseUpdatedWithSalesOrderFunction, cancellationToken);
        }

        public Task<string> OnPurchaseUpdatedWithSalesOrderRequestAsync(Po po)
        {
            var onPurchaseUpdatedWithSalesOrderFunction = new OnPurchaseUpdatedWithSalesOrderFunction();
                onPurchaseUpdatedWithSalesOrderFunction.Po = po;
            
             return ContractHandler.SendRequestAsync(onPurchaseUpdatedWithSalesOrderFunction);
        }

        public Task<TransactionReceipt> OnPurchaseUpdatedWithSalesOrderRequestAndWaitForReceiptAsync(Po po, CancellationTokenSource cancellationToken = null)
        {
            var onPurchaseUpdatedWithSalesOrderFunction = new OnPurchaseUpdatedWithSalesOrderFunction();
                onPurchaseUpdatedWithSalesOrderFunction.Po = po;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(onPurchaseUpdatedWithSalesOrderFunction, cancellationToken);
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
