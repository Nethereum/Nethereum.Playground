using Nethereum.ABI.FunctionEncoding.Attributes;

namespace NetDapps.CommerceAdmin.Contracts.WalletBuyer.ContractDefinition
{
    public partial class Po : PoBase { }

    public class PoBase
    {
        [Parameter("uint64", "ethPurchaseOrderNumber", 1)]
        public virtual ulong EthPurchaseOrderNumber { get; set; }

        [Parameter("bytes32", "buyerSysId", 2)]
        public virtual string BuyerSysId { get; set; }

        [Parameter("bytes32", "buyerPurchaseOrderNumber", 3)]
        public virtual string BuyerPurchaseOrderNumber { get; set; }

        [Parameter("bytes32", "buyerViewVendorId", 4)]
        public virtual string BuyerViewVendorId { get; set; }

        [Parameter("bytes32", "sellerSysId", 5)]
        public virtual string SellerSysId { get; set; }

        [Parameter("bytes32", "sellerSalesOrderNumber", 6)]
        public virtual string SellerSalesOrderNumber { get; set; }

        [Parameter("bytes32", "sellerViewCustomerId", 7)]
        public virtual string SellerViewCustomerId { get; set; }

        [Parameter("bytes32", "buyerProductId", 8)]
        public virtual string BuyerProductId { get; set; }

        [Parameter("bytes32", "sellerProductId", 9)]
        public virtual string SellerProductId { get; set; }

        [Parameter("bytes32", "currency", 10)]
        public virtual string Currency { get; set; }

        [Parameter("address", "currencyAddress", 11)]
        public virtual string CurrencyAddress { get; set; }

        [Parameter("uint32", "totalQuantity", 12)]
        public virtual uint TotalQuantity { get; set; }

        [Parameter("uint32", "totalValue", 13)]
        public virtual uint TotalValue { get; set; }

        [Parameter("uint32", "openInvoiceQuantity", 14)]
        public virtual uint OpenInvoiceQuantity { get; set; }

        [Parameter("uint32", "openInvoiceValue", 15)]
        public virtual uint OpenInvoiceValue { get; set; }

        [Parameter("uint8", "poStatus", 16)]
        public virtual byte PoStatus { get; set; }

        [Parameter("uint8", "wiProcessStatus", 17)]
        public virtual byte WiProcessStatus { get; set; }
    }
}
