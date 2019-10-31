using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace NetDapps.CommerceAdmin.Contracts.WalletSeller.ContractDefinition
{


    public partial class WalletSellerDeployment : WalletSellerDeploymentBase
    {
        public WalletSellerDeployment() : base(BYTECODE) { }
        public WalletSellerDeployment(string byteCode) : base(byteCode) { }
    }

    public class WalletSellerDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "6080604052604051610ff7380380610ff783398101604081905261002291610058565b600180546001600160a01b0319166001600160a01b03929092169190911790556100a6565b80516100528161008f565b92915050565b60006020828403121561006a57600080fd5b60006100768484610047565b949350505050565b60006001600160a01b038216610052565b6100988161007e565b81146100a357600080fd5b50565b610f42806100b56000396000f3fe608060405234801561001057600080fd5b50600436106100f55760003560e01c80637339e00811610097578063cebbe67411610066578063cebbe674146101cf578063d3c840de146101e2578063f3ad65f4146101f5578063f792cadc146101fd576100f5565b80637339e0081461018e578063995df76c146101a1578063a7997fc4146101a9578063cc9ab253146101bc576100f5565b806332af5ed5116100d357806332af5ed51461013557806339f898bc146101485780634f67a8d414610166578063664ee73314610179576100f5565b806306ac2d3d146100fa5780630e996a041461010f57806330caa00414610122575b600080fd5b61010d610108366004610875565b610210565b005b61010d61011d366004610928565b61041e565b61010d610130366004610909565b610483565b61010d610143366004610928565b6104de565b61015061050e565b60405161015d9190610ca2565b60405180910390f35b61010d610174366004610946565b610514565b610181610580565b60405161015d9190610cb0565b61015061019c366004610813565b61058f565b610181610619565b61010d6101b7366004610928565b610628565b61010d6101ca366004610928565b610658565b61010d6101dd366004610928565b610688565b61010d6101f0366004610909565b6106b8565b610181610708565b61015061020b366004610813565b610717565b6040516319f6a32560e31b815273__$4f6e1f7166b61d394a3a463d15dc4917b6$__9063cfb5192890610247908690600401610cbe565b60206040518083038186803b15801561025f57600080fd5b505af4158015610273573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506102979190810190610857565b60035560015460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c153906102ca908590600401610cbe565b60206040518083038186803b1580156102e257600080fd5b505afa1580156102f6573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525061031a9190810190610839565b600080546001600160a01b0319166001600160a01b0392831617908190551661035e5760405162461bcd60e51b815260040161035590610cd6565b60405180910390fd5b60015460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c1539061038e908490600401610cbe565b60206040518083038186803b1580156103a657600080fd5b505afa1580156103ba573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506103de9190810190610839565b600280546001600160a01b0319166001600160a01b039283161790819055166104195760405162461bcd60e51b815260040161035590610ce6565b505050565b6000546040516303a65a8160e21b81526001600160a01b0390911690630e996a049061044e908490600401610d05565b600060405180830381600087803b15801561046857600080fd5b505af115801561047c573d6000803e3d6000fd5b5050505050565b6104906020820182610928565b67ffffffffffffffff16816080013582602001357fe3bacf19264647689c63f2fac4cefdfc0a6fa678de13fde4bd23096d53a48819846040516104d39190610cf6565b60405180910390a450565b6000546040516332af5ed560e01b81526001600160a01b03909116906332af5ed59061044e908490600401610d05565b60035481565b6000546003546040516336b5e67960e11b81526001600160a01b0390921691636d6bccf29161054a918691908690600401610d13565b600060405180830381600087803b15801561056457600080fd5b505af1158015610578573d6000803e3d6000fd5b505050505050565b6000546001600160a01b031681565b6040516370a0823160e01b815260009082906001600160a01b038216906370a08231906105c0903090600401610c86565b60206040518083038186803b1580156105d857600080fd5b505afa1580156105ec573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506106109190810190610857565b9150505b919050565b6002546001600160a01b031681565b6000546040516329e65ff160e21b81526001600160a01b039091169063a7997fc49061044e908490600401610d05565b60005460405163cc9ab25360e01b81526001600160a01b039091169063cc9ab2539061044e908490600401610d05565b6000546040516333aef99d60e21b81526001600160a01b039091169063cebbe6749061044e908490600401610d05565b6106c56020820182610928565b67ffffffffffffffff16816080013582602001357f94d425423d9e94548675d285b64601c0670994eef28e15e627afcf93dd1d2b6b846040516104d39190610cf6565b6001546001600160a01b031681565b6040516370a0823160e01b815260009082906001600160a01b038216906370a08231906105c0903390600401610c94565b803561075381610eb6565b92915050565b805161075381610eb6565b803561075381610eca565b805161075381610eca565b803561075381610ed3565b803561075381610ee0565b600082601f8301126107a157600080fd5b81356107b46107af82610d62565b610d3b565b915080825260208301602083018583830111156107d057600080fd5b6107db838284610e5d565b50505092915050565b600061022082840312156107f757600080fd5b50919050565b803561075381610eed565b803561075381610ef6565b60006020828403121561082557600080fd5b60006108318484610748565b949350505050565b60006020828403121561084b57600080fd5b60006108318484610759565b60006020828403121561086957600080fd5b6000610831848461076f565b60008060006060848603121561088a57600080fd5b833567ffffffffffffffff8111156108a157600080fd5b6108ad86828701610790565b935050602084013567ffffffffffffffff8111156108ca57600080fd5b6108d686828701610790565b925050604084013567ffffffffffffffff8111156108f357600080fd5b6108ff86828701610790565b9150509250925092565b6000610220828403121561091c57600080fd5b600061083184846107e4565b60006020828403121561093a57600080fd5b60006108318484610808565b6000806040838503121561095957600080fd5b60006109658585610808565b925050602061097685828601610764565b9150509250929050565b61098981610e35565b82525050565b61098981610df1565b61098981610dfc565b61098981610e3c565b61098981610e47565b61098981610e52565b60006109c782610d8a565b6109d18185610d8e565b93506109e1818560208601610e69565b6109ea81610e95565b9093019392505050565b6000610a01602983610d8e565b7f436f756c64206e6f742066696e6420506f4d61696e206164647265737320696e81526820726567697374727960b81b602082015260400192915050565b6000610a4c603283610d8e565b7f436f756c64206e6f742066696e642046756e64696e67436f6e7472616374206181527164647265737320696e20726567697374727960701b602082015260400192915050565b6102208201610aa28280610de2565b610aac8482610c7d565b50610aba6020830183610da6565b610ac76020850182610998565b50610ad56040830183610da6565b610ae26040850182610998565b50610af06060830183610da6565b610afd6060850182610998565b50610b0b6080830183610da6565b610b186080850182610998565b50610b2660a0830183610da6565b610b3360a0850182610998565b50610b4160c0830183610da6565b610b4e60c0850182610998565b50610b5c60e0830183610da6565b610b6960e0850182610998565b50610b78610100830183610da6565b610b86610100850182610998565b50610b95610120830183610da6565b610ba3610120850182610998565b50610bb2610140830183610d97565b610bc061014085018261098f565b50610bcf610160830183610dd3565b610bdd610160850182610c74565b50610bec610180830183610dd3565b610bfa610180850182610c74565b50610c096101a0830183610dd3565b610c176101a0850182610c74565b50610c266101c0830183610dd3565b610c346101c0850182610c74565b50610c436101e0830183610db5565b610c516101e08501826109aa565b50610c60610200830183610dc4565b610c6e6102008501826109b3565b50505050565b61098981610e1f565b61098981610e28565b60208101610753828461098f565b602081016107538284610980565b602081016107538284610998565b6020810161075382846109a1565b60208082528101610ccf81846109bc565b9392505050565b60208082528101610753816109f4565b6020808252810161075381610a3f565b61022081016107538284610a93565b602081016107538284610c7d565b60608101610d218286610c7d565b610d2e6020830185610998565b6108316040830184610998565b60405181810167ffffffffffffffff81118282101715610d5a57600080fd5b604052919050565b600067ffffffffffffffff821115610d7957600080fd5b506020601f91909101601f19160190565b5190565b90815260200190565b6000610ccf6020840184610748565b6000610ccf6020840184610764565b6000610ccf602084018461077a565b6000610ccf6020840184610785565b6000610ccf60208401846107fd565b6000610ccf6020840184610808565b600061075382610e13565b90565b8061061481610e9f565b8061061481610eac565b6001600160a01b031690565b63ffffffff1690565b67ffffffffffffffff1690565b6000610753825b600061075382610df1565b600061075382610dff565b600061075382610e09565b82818337506000910152565b60005b83811015610e84578181015183820152602001610e6c565b83811115610c6e5750506000910152565b601f01601f191690565b60058110610ea957fe5b50565b600e8110610ea957fe5b610ebf81610df1565b8114610ea957600080fd5b610ebf81610dfc565b60058110610ea957600080fd5b600e8110610ea957600080fd5b610ebf81610e1f565b610ebf81610e2856fea365627a7a723058201cacdc69ad263505d51b10225cb0800007777ff407e97ddd7d5d57656437402a6c6578706572696d656e74616cf564736f6c634300050a0040";
        public WalletSellerDeploymentBase() : base(BYTECODE) { }
        public WalletSellerDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address", "contractAddressOfRegistry", 1)]
        public virtual string ContractAddressOfRegistry { get; set; }
    }

    public partial class ConfigureFunction : ConfigureFunctionBase { }

    [Function("configure")]
    public class ConfigureFunctionBase : FunctionMessage
    {
        [Parameter("string", "sysIdAsString", 1)]
        public virtual string SysIdAsString { get; set; }
        [Parameter("string", "nameOfPoMain", 2)]
        public virtual string NameOfPoMain { get; set; }
        [Parameter("string", "nameOfFundingContract", 3)]
        public virtual string NameOfFundingContract { get; set; }
    }

    public partial class ReportSalesOrderCancelFailureFunction : ReportSalesOrderCancelFailureFunctionBase { }

    [Function("reportSalesOrderCancelFailure")]
    public class ReportSalesOrderCancelFailureFunctionBase : FunctionMessage
    {
        [Parameter("uint64", "ethPoNumber", 1)]
        public virtual ulong EthPoNumber { get; set; }
    }

    public partial class OnCancelPurchaseOrderRequestedFunction : OnCancelPurchaseOrderRequestedFunctionBase { }

    [Function("onCancelPurchaseOrderRequested")]
    public class OnCancelPurchaseOrderRequestedFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "po", 1)]
        public virtual Po Po { get; set; }
    }

    public partial class ReportSalesOrderNotApprovedFunction : ReportSalesOrderNotApprovedFunctionBase { }

    [Function("reportSalesOrderNotApproved")]
    public class ReportSalesOrderNotApprovedFunctionBase : FunctionMessage
    {
        [Parameter("uint64", "ethPoNumber", 1)]
        public virtual ulong EthPoNumber { get; set; }
    }

    public partial class SystemIdFunction : SystemIdFunctionBase { }

    [Function("systemId", "bytes32")]
    public class SystemIdFunctionBase : FunctionMessage
    {

    }

    public partial class SetSalesOrderNumberByEthPoNumberFunction : SetSalesOrderNumberByEthPoNumberFunctionBase { }

    [Function("setSalesOrderNumberByEthPoNumber")]
    public class SetSalesOrderNumberByEthPoNumberFunctionBase : FunctionMessage
    {
        [Parameter("uint64", "ethPoNumber", 1)]
        public virtual ulong EthPoNumber { get; set; }
        [Parameter("bytes32", "sellerSalesOrderNumber", 2)]
        public virtual byte[] SellerSalesOrderNumber { get; set; }
    }

    public partial class PoMainFunction : PoMainFunctionBase { }

    [Function("poMain", "address")]
    public class PoMainFunctionBase : FunctionMessage
    {

    }

    public partial class GetTokenBalanceOwnedByThisFunction : GetTokenBalanceOwnedByThisFunctionBase { }

    [Function("getTokenBalanceOwnedByThis", "uint256")]
    public class GetTokenBalanceOwnedByThisFunctionBase : FunctionMessage
    {
        [Parameter("address", "tokenAddress", 1)]
        public virtual string TokenAddress { get; set; }
    }

    public partial class FundingContractFunction : FundingContractFunctionBase { }

    [Function("fundingContract", "address")]
    public class FundingContractFunctionBase : FunctionMessage
    {

    }

    public partial class RefundPoToBuyerFunction : RefundPoToBuyerFunctionBase { }

    [Function("refundPoToBuyer")]
    public class RefundPoToBuyerFunctionBase : FunctionMessage
    {
        [Parameter("uint64", "ethPoNumber", 1)]
        public virtual ulong EthPoNumber { get; set; }
    }

    public partial class ReleasePoFundsToSellerFunction : ReleasePoFundsToSellerFunctionBase { }

    [Function("releasePoFundsToSeller")]
    public class ReleasePoFundsToSellerFunctionBase : FunctionMessage
    {
        [Parameter("uint64", "ethPoNumber", 1)]
        public virtual ulong EthPoNumber { get; set; }
    }

    public partial class ReportSalesOrderInvoiceFaultFunction : ReportSalesOrderInvoiceFaultFunctionBase { }

    [Function("reportSalesOrderInvoiceFault")]
    public class ReportSalesOrderInvoiceFaultFunctionBase : FunctionMessage
    {
        [Parameter("uint64", "ethPoNumber", 1)]
        public virtual ulong EthPoNumber { get; set; }
    }

    public partial class OnCreatePurchaseOrderRequestedFunction : OnCreatePurchaseOrderRequestedFunctionBase { }

    [Function("onCreatePurchaseOrderRequested")]
    public class OnCreatePurchaseOrderRequestedFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "po", 1)]
        public virtual Po Po { get; set; }
    }

    public partial class AddressRegistryFunction : AddressRegistryFunctionBase { }

    [Function("addressRegistry", "address")]
    public class AddressRegistryFunctionBase : FunctionMessage
    {

    }

    public partial class GetTokenBalanceOwnedByMsgSenderFunction : GetTokenBalanceOwnedByMsgSenderFunctionBase { }

    [Function("getTokenBalanceOwnedByMsgSender", "uint256")]
    public class GetTokenBalanceOwnedByMsgSenderFunctionBase : FunctionMessage
    {
        [Parameter("address", "tokenAddress", 1)]
        public virtual string TokenAddress { get; set; }
    }

    public partial class PurchaseRaisedOkLogEventDTO : PurchaseRaisedOkLogEventDTOBase { }

    [Event("PurchaseRaisedOkLog")]
    public class PurchaseRaisedOkLogEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "buyerSysId", 1, true )]
        public virtual byte[] BuyerSysId { get; set; }
        [Parameter("bytes32", "sellerSysId", 2, true )]
        public virtual byte[] SellerSysId { get; set; }
        [Parameter("uint64", "ethPurchaseOrderNumber", 3, true )]
        public virtual ulong EthPurchaseOrderNumber { get; set; }
        [Parameter("tuple", "po", 4, false )]
        public virtual Po Po { get; set; }
    }

    public partial class PurchaseCancelRequestedOkLogEventDTO : PurchaseCancelRequestedOkLogEventDTOBase { }

    [Event("PurchaseCancelRequestedOkLog")]
    public class PurchaseCancelRequestedOkLogEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "buyerSysId", 1, true )]
        public virtual byte[] BuyerSysId { get; set; }
        [Parameter("bytes32", "sellerSysId", 2, true )]
        public virtual byte[] SellerSysId { get; set; }
        [Parameter("uint64", "ethPurchaseOrderNumber", 3, true )]
        public virtual ulong EthPurchaseOrderNumber { get; set; }
        [Parameter("tuple", "po", 4, false )]
        public virtual Po Po { get; set; }
    }









    public partial class SystemIdOutputDTO : SystemIdOutputDTOBase { }

    [FunctionOutput]
    public class SystemIdOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }



    public partial class PoMainOutputDTO : PoMainOutputDTOBase { }

    [FunctionOutput]
    public class PoMainOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetTokenBalanceOwnedByThisOutputDTO : GetTokenBalanceOwnedByThisOutputDTOBase { }

    [FunctionOutput]
    public class GetTokenBalanceOwnedByThisOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "balanceOwnedByThis", 1)]
        public virtual BigInteger BalanceOwnedByThis { get; set; }
    }

    public partial class FundingContractOutputDTO : FundingContractOutputDTOBase { }

    [FunctionOutput]
    public class FundingContractOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }









    public partial class AddressRegistryOutputDTO : AddressRegistryOutputDTOBase { }

    [FunctionOutput]
    public class AddressRegistryOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetTokenBalanceOwnedByMsgSenderOutputDTO : GetTokenBalanceOwnedByMsgSenderOutputDTOBase { }

    [FunctionOutput]
    public class GetTokenBalanceOwnedByMsgSenderOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "balanceOwnedByMsgSender", 1)]
        public virtual BigInteger BalanceOwnedByMsgSender { get; set; }
    }
}
