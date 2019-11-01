using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using NetDapps.CommerceAdmin.Contracts.BusinessPartnerStorage.ContractDefinition;
using System.Threading;
using System.Threading.Tasks;

namespace NetDapps.CommerceAdmin.Contracts.BusinessPartnerStorage
{
    public partial class BusinessPartnerStorageService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, BusinessPartnerStorageDeployment businessPartnerStorageDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<BusinessPartnerStorageDeployment>().SendRequestAndWaitForReceiptAsync(businessPartnerStorageDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, BusinessPartnerStorageDeployment businessPartnerStorageDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<BusinessPartnerStorageDeployment>().SendRequestAsync(businessPartnerStorageDeployment);
        }

        public static async Task<BusinessPartnerStorageService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, BusinessPartnerStorageDeployment businessPartnerStorageDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, businessPartnerStorageDeployment, cancellationTokenSource);
            return new BusinessPartnerStorageService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3 { get; }

        public ContractHandler ContractHandler { get; }

        public BusinessPartnerStorageService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> GetWalletAddressQueryAsync(GetWalletAddressFunction getWalletAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetWalletAddressFunction, string>(getWalletAddressFunction, blockParameter);
        }


        public Task<string> GetWalletAddressQueryAsync(byte[] systemId, BlockParameter blockParameter = null)
        {
            var getWalletAddressFunction = new GetWalletAddressFunction();
            getWalletAddressFunction.SystemId = systemId;

            return ContractHandler.QueryAsync<GetWalletAddressFunction, string>(getWalletAddressFunction, blockParameter);
        }

        public Task<string> SetWalletAddressRequestAsync(SetWalletAddressFunction setWalletAddressFunction)
        {
            return ContractHandler.SendRequestAsync(setWalletAddressFunction);
        }

        public Task<TransactionReceipt> SetWalletAddressRequestAndWaitForReceiptAsync(SetWalletAddressFunction setWalletAddressFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(setWalletAddressFunction, cancellationToken);
        }

        public Task<string> SetWalletAddressRequestAsync(byte[] systemId, string walletAddress)
        {
            var setWalletAddressFunction = new SetWalletAddressFunction();
            setWalletAddressFunction.SystemId = systemId;
            setWalletAddressFunction.WalletAddress = walletAddress;

            return ContractHandler.SendRequestAsync(setWalletAddressFunction);
        }

        public Task<TransactionReceipt> SetWalletAddressRequestAndWaitForReceiptAsync(byte[] systemId, string walletAddress, CancellationTokenSource cancellationToken = null)
        {
            var setWalletAddressFunction = new SetWalletAddressFunction();
            setWalletAddressFunction.SystemId = systemId;
            setWalletAddressFunction.WalletAddress = walletAddress;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(setWalletAddressFunction, cancellationToken);
        }

        public Task<string> ConfigureRequestAsync(ConfigureFunction configureFunction)
        {
            return ContractHandler.SendRequestAsync(configureFunction);
        }

        public Task<TransactionReceipt> ConfigureRequestAndWaitForReceiptAsync(ConfigureFunction configureFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(configureFunction, cancellationToken);
        }

        public Task<string> ConfigureRequestAsync(string nameOfEternalStorage)
        {
            var configureFunction = new ConfigureFunction();
            configureFunction.NameOfEternalStorage = nameOfEternalStorage;

            return ContractHandler.SendRequestAsync(configureFunction);
        }

        public Task<TransactionReceipt> ConfigureRequestAndWaitForReceiptAsync(string nameOfEternalStorage, CancellationTokenSource cancellationToken = null)
        {
            var configureFunction = new ConfigureFunction();
            configureFunction.NameOfEternalStorage = nameOfEternalStorage;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(configureFunction, cancellationToken);
        }

        public Task<string> SetBuyerViewVendorIdForSellerSysIdRequestAsync(SetBuyerViewVendorIdForSellerSysIdFunction setBuyerViewVendorIdForSellerSysIdFunction)
        {
            return ContractHandler.SendRequestAsync(setBuyerViewVendorIdForSellerSysIdFunction);
        }

        public Task<TransactionReceipt> SetBuyerViewVendorIdForSellerSysIdRequestAndWaitForReceiptAsync(SetBuyerViewVendorIdForSellerSysIdFunction setBuyerViewVendorIdForSellerSysIdFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(setBuyerViewVendorIdForSellerSysIdFunction, cancellationToken);
        }

        public Task<string> SetBuyerViewVendorIdForSellerSysIdRequestAsync(byte[] buyerSystemId, byte[] sellerSystemId, byte[] buyerViewVendorId)
        {
            var setBuyerViewVendorIdForSellerSysIdFunction = new SetBuyerViewVendorIdForSellerSysIdFunction();
            setBuyerViewVendorIdForSellerSysIdFunction.BuyerSystemId = buyerSystemId;
            setBuyerViewVendorIdForSellerSysIdFunction.SellerSystemId = sellerSystemId;
            setBuyerViewVendorIdForSellerSysIdFunction.BuyerViewVendorId = buyerViewVendorId;

            return ContractHandler.SendRequestAsync(setBuyerViewVendorIdForSellerSysIdFunction);
        }

        public Task<TransactionReceipt> SetBuyerViewVendorIdForSellerSysIdRequestAndWaitForReceiptAsync(byte[] buyerSystemId, byte[] sellerSystemId, byte[] buyerViewVendorId, CancellationTokenSource cancellationToken = null)
        {
            var setBuyerViewVendorIdForSellerSysIdFunction = new SetBuyerViewVendorIdForSellerSysIdFunction();
            setBuyerViewVendorIdForSellerSysIdFunction.BuyerSystemId = buyerSystemId;
            setBuyerViewVendorIdForSellerSysIdFunction.SellerSystemId = sellerSystemId;
            setBuyerViewVendorIdForSellerSysIdFunction.BuyerViewVendorId = buyerViewVendorId;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(setBuyerViewVendorIdForSellerSysIdFunction, cancellationToken);
        }

        public Task<string> EternalStorageQueryAsync(EternalStorageFunction eternalStorageFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<EternalStorageFunction, string>(eternalStorageFunction, blockParameter);
        }


        public Task<string> EternalStorageQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<EternalStorageFunction, string>(null, blockParameter);
        }

        public Task<string> SetSystemDescriptionRequestAsync(SetSystemDescriptionFunction setSystemDescriptionFunction)
        {
            return ContractHandler.SendRequestAsync(setSystemDescriptionFunction);
        }

        public Task<TransactionReceipt> SetSystemDescriptionRequestAndWaitForReceiptAsync(SetSystemDescriptionFunction setSystemDescriptionFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(setSystemDescriptionFunction, cancellationToken);
        }

        public Task<string> SetSystemDescriptionRequestAsync(byte[] systemId, byte[] systemDescription)
        {
            var setSystemDescriptionFunction = new SetSystemDescriptionFunction();
            setSystemDescriptionFunction.SystemId = systemId;
            setSystemDescriptionFunction.SystemDescription = systemDescription;

            return ContractHandler.SendRequestAsync(setSystemDescriptionFunction);
        }

        public Task<TransactionReceipt> SetSystemDescriptionRequestAndWaitForReceiptAsync(byte[] systemId, byte[] systemDescription, CancellationTokenSource cancellationToken = null)
        {
            var setSystemDescriptionFunction = new SetSystemDescriptionFunction();
            setSystemDescriptionFunction.SystemId = systemId;
            setSystemDescriptionFunction.SystemDescription = systemDescription;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(setSystemDescriptionFunction, cancellationToken);
        }

        public Task<string> GetSystemDescriptionQueryAsync(GetSystemDescriptionFunction getSystemDescriptionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetSystemDescriptionFunction, string>(getSystemDescriptionFunction, blockParameter);
        }


        public Task<string> GetSystemDescriptionQueryAsync(byte[] systemId, BlockParameter blockParameter = null)
        {
            var getSystemDescriptionFunction = new GetSystemDescriptionFunction();
            getSystemDescriptionFunction.SystemId = systemId;

            return ContractHandler.QueryAsync<GetSystemDescriptionFunction, string>(getSystemDescriptionFunction, blockParameter);
        }

        //public Task<byte[]> GetSystemDescriptionQueryAsync(GetSystemDescriptionFunction getSystemDescriptionFunction, BlockParameter blockParameter = null)
        //{
        //    return ContractHandler.QueryAsync<GetSystemDescriptionFunction, byte[]>(getSystemDescriptionFunction, blockParameter);
        //}


        //public Task<byte[]> GetSystemDescriptionQueryAsync(byte[] systemId, BlockParameter blockParameter = null)
        //{
        //    var getSystemDescriptionFunction = new GetSystemDescriptionFunction();
        //    getSystemDescriptionFunction.SystemId = systemId;

        //    return ContractHandler.QueryAsync<GetSystemDescriptionFunction, byte[]>(getSystemDescriptionFunction, blockParameter);
        //}

        public Task<byte[]> GetSellerViewCustomerIdForBuyerSysIdQueryAsync(GetSellerViewCustomerIdForBuyerSysIdFunction getSellerViewCustomerIdForBuyerSysIdFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetSellerViewCustomerIdForBuyerSysIdFunction, byte[]>(getSellerViewCustomerIdForBuyerSysIdFunction, blockParameter);
        }


        public Task<byte[]> GetSellerViewCustomerIdForBuyerSysIdQueryAsync(byte[] buyerSystemId, byte[] sellerSystemId, BlockParameter blockParameter = null)
        {
            var getSellerViewCustomerIdForBuyerSysIdFunction = new GetSellerViewCustomerIdForBuyerSysIdFunction();
            getSellerViewCustomerIdForBuyerSysIdFunction.BuyerSystemId = buyerSystemId;
            getSellerViewCustomerIdForBuyerSysIdFunction.SellerSystemId = sellerSystemId;

            return ContractHandler.QueryAsync<GetSellerViewCustomerIdForBuyerSysIdFunction, byte[]>(getSellerViewCustomerIdForBuyerSysIdFunction, blockParameter);
        }

        public Task<string> SetSellerViewCustomerIdForBuyerSysIdRequestAsync(SetSellerViewCustomerIdForBuyerSysIdFunction setSellerViewCustomerIdForBuyerSysIdFunction)
        {
            return ContractHandler.SendRequestAsync(setSellerViewCustomerIdForBuyerSysIdFunction);
        }

        public Task<TransactionReceipt> SetSellerViewCustomerIdForBuyerSysIdRequestAndWaitForReceiptAsync(SetSellerViewCustomerIdForBuyerSysIdFunction setSellerViewCustomerIdForBuyerSysIdFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(setSellerViewCustomerIdForBuyerSysIdFunction, cancellationToken);
        }

        public Task<string> SetSellerViewCustomerIdForBuyerSysIdRequestAsync(byte[] buyerSystemId, byte[] sellerSystemId, byte[] sellerViewCustomerId)
        {
            var setSellerViewCustomerIdForBuyerSysIdFunction = new SetSellerViewCustomerIdForBuyerSysIdFunction();
            setSellerViewCustomerIdForBuyerSysIdFunction.BuyerSystemId = buyerSystemId;
            setSellerViewCustomerIdForBuyerSysIdFunction.SellerSystemId = sellerSystemId;
            setSellerViewCustomerIdForBuyerSysIdFunction.SellerViewCustomerId = sellerViewCustomerId;

            return ContractHandler.SendRequestAsync(setSellerViewCustomerIdForBuyerSysIdFunction);
        }

        public Task<TransactionReceipt> SetSellerViewCustomerIdForBuyerSysIdRequestAndWaitForReceiptAsync(byte[] buyerSystemId, byte[] sellerSystemId, byte[] sellerViewCustomerId, CancellationTokenSource cancellationToken = null)
        {
            var setSellerViewCustomerIdForBuyerSysIdFunction = new SetSellerViewCustomerIdForBuyerSysIdFunction();
            setSellerViewCustomerIdForBuyerSysIdFunction.BuyerSystemId = buyerSystemId;
            setSellerViewCustomerIdForBuyerSysIdFunction.SellerSystemId = sellerSystemId;
            setSellerViewCustomerIdForBuyerSysIdFunction.SellerViewCustomerId = sellerViewCustomerId;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(setSellerViewCustomerIdForBuyerSysIdFunction, cancellationToken);
        }

        public Task<string> AddressRegistryQueryAsync(AddressRegistryFunction addressRegistryFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AddressRegistryFunction, string>(addressRegistryFunction, blockParameter);
        }


        public Task<string> AddressRegistryQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AddressRegistryFunction, string>(null, blockParameter);
        }

        public Task<byte[]> GetBuyerViewVendorIdForSellerSysIdQueryAsync(GetBuyerViewVendorIdForSellerSysIdFunction getBuyerViewVendorIdForSellerSysIdFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetBuyerViewVendorIdForSellerSysIdFunction, byte[]>(getBuyerViewVendorIdForSellerSysIdFunction, blockParameter);
        }


        public Task<byte[]> GetBuyerViewVendorIdForSellerSysIdQueryAsync(byte[] buyerSystemId, byte[] sellerSystemId, BlockParameter blockParameter = null)
        {
            var getBuyerViewVendorIdForSellerSysIdFunction = new GetBuyerViewVendorIdForSellerSysIdFunction();
            getBuyerViewVendorIdForSellerSysIdFunction.BuyerSystemId = buyerSystemId;
            getBuyerViewVendorIdForSellerSysIdFunction.SellerSystemId = sellerSystemId;

            return ContractHandler.QueryAsync<GetBuyerViewVendorIdForSellerSysIdFunction, byte[]>(getBuyerViewVendorIdForSellerSysIdFunction, blockParameter);
        }
    }
}
