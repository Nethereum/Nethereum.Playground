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

namespace NetDapps.CommerceAdmin.Contracts.WalletBuyer.ContractDefinition
{


    public partial class WalletBuyerDeployment : WalletBuyerDeploymentBase
    {
        public WalletBuyerDeployment() : base(BYTECODE) { }
        public WalletBuyerDeployment(string byteCode) : base(byteCode) { }
    }

    public class WalletBuyerDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405260405162001e4838038062001e4883398101604081905262000026916200005f565b600180546001600160a01b0319166001600160a01b0392909216919091179055620000b4565b805162000059816200009a565b92915050565b6000602082840312156200007257600080fd5b60006200008084846200004c565b949350505050565b60006001600160a01b03821662000059565b620000a58162000088565b8114620000b157600080fd5b50565b611d8480620000c46000396000f3fe608060405234801561001057600080fd5b50600436106101165760003560e01c80637c1a303e116100a2578063d712abf111610071578063d712abf114610216578063e7829b4714610229578063efd100ea1461023c578063f3ad65f41461024f578063f792cadc1461025757610116565b80637c1a303e146101d55780637e1854b2146101e8578063912843e4146101fb578063995df76c1461020e57610116565b806362dad02a116100e957806362dad02a14610174578063664ee733146101875780636fd46a9e1461019c5780637339e008146101af57806378f6f0d3146101c257610116565b806306ac2d3d1461011b5780630af847f91461013057806321a198b31461014357806339f898bc14610156575b600080fd5b61012e6101293660046113ef565b61026a565b005b61012e61013e366004611480565b610478565b61012e6101513660046114dd565b6104d2565b61015e610642565b60405161016b9190611a83565b60405180910390f35b61012e610182366004611480565b610648565b61018f6106c1565b60405161016b9190611aac565b61012e6101aa366004611480565b6106d0565b61015e6101bd36600461136f565b610749565b61012e6101d036600461149f565b6107d3565b61012e6101e336600461136f565b610a25565b61012e6101f6366004611480565b610c89565b61012e610209366004611480565b610cd8565b61018f610d27565b61012e610224366004611480565b610d36565b61012e610237366004611480565b610daf565b61012e61024a366004611480565b610e28565b61018f610ea1565b61015e61026536600461136f565b610eb0565b6040516319f6a32560e31b815273__$4f6e1f7166b61d394a3a463d15dc4917b6$__9063cfb51928906102a1908690600401611aba565b60206040518083038186803b1580156102b957600080fd5b505af41580156102cd573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506102f191908101906113d1565b60035560015460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c15390610324908590600401611aba565b60206040518083038186803b15801561033c57600080fd5b505afa158015610350573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506103749190810190611395565b600080546001600160a01b0319166001600160a01b039283161790819055166103b85760405162461bcd60e51b81526004016103af90611acb565b60405180910390fd5b60015460405163d9c4c15360e01b81526001600160a01b039091169063d9c4c153906103e8908490600401611aba565b60206040518083038186803b15801561040057600080fd5b505afa158015610414573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506104389190810190611395565b600280546001600160a01b0319166001600160a01b039283161790819055166104735760405162461bcd60e51b81526004016103af90611b0b565b505050565b61048560208201826114dd565b6001600160401b0316816080013582602001357f44a5c3b1572b39916b50bcc206aabe6ec2376404f3091b9e5ba279956b3039b6846040516104c79190611b3b565b60405180910390a450565b600080546040516321a198b360e01b81526001600160a01b03909116906321a198b390610503908590600401611b59565b602060405180830381600087803b15801561051d57600080fd5b505af1158015610531573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525061055591908101906113b3565b90506001811515141561063e5761056a610ee1565b600054604051634339a73160e11b81526001600160a01b03909116906386734e629061059a908690600401611b59565b6102206040518083038186803b1580156105b357600080fd5b505afa1580156105c7573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506105eb91908101906114be565b905080600001516001600160401b0316816080015182602001517fe3bacf19264647689c63f2fac4cefdfc0a6fa678de13fde4bd23096d53a48819846040516106349190611b4a565b60405180910390a4505b5050565b60035481565b6000546001600160a01b031633146106725760405162461bcd60e51b81526004016103af90611b1b565b61067f60208201826114dd565b6001600160401b0316816080013582602001357f655ef95e2c43e1ecc35e54c13cd5a9e37e4bd5fa494923605b5be49c2e0a53b3846040516104c79190611b3b565b6000546001600160a01b031681565b6000546001600160a01b031633146106fa5760405162461bcd60e51b81526004016103af90611b1b565b61070760208201826114dd565b6001600160401b0316816080013582602001357f6888809b5e40bd6e609d3027d849929bb7e4712ea5e7350511d41786b64169a5846040516104c79190611b3b565b6040516370a0823160e01b815260009082906001600160a01b038216906370a082319061077a903090600401611a45565b60206040518083038186803b15801561079257600080fd5b505afa1580156107a6573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506107ca91908101906113d1565b9150505b919050565b61014081015160025461018083015160405163095ea7b360e01b81526001600160a01b038085169363095ea7b393610812939290911691600401611a61565b602060405180830381600087803b15801561082c57600080fd5b505af1158015610840573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525061086491908101906113b3565b50600080546040516378f6f0d360e01b81526001600160a01b03909116906378f6f0d390610896908690600401611b4a565b602060405180830381600087803b1580156108b057600080fd5b505af11580156108c4573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052506108e891908101906113b3565b905060018115151415610473576108fd610ee1565b600054602085015160408087015190516397b7578360e01b81526001600160a01b03909316926397b7578392610937929091600401611a91565b6102206040518083038186803b15801561095057600080fd5b505afa158015610964573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525061098891908101906114be565b90508060e00151816040015182602001517f2d34e771d84afff1f37ed97b270ab3a9c8e0f289852edb07f7ba4c99b3dccbef846040516109c89190611b4a565b60405180910390a480600001516001600160401b0316816080015182602001517f94d425423d9e94548675d285b64601c0670994eef28e15e627afcf93dd1d2b6b84604051610a179190611b4a565b60405180910390a450505050565b610a2d610ee1565b60035460208201526040516319f6a32560e31b815273__$4f6e1f7166b61d394a3a463d15dc4917b6$__9063cfb5192890610a6a90600401611aeb565b60206040518083038186803b158015610a8257600080fd5b505af4158015610a96573d6000803e3d6000fd5b505050506040513d601f19601f82011682018060405250610aba91908101906113d1565b604080830191909152516319f6a32560e31b815273__$4f6e1f7166b61d394a3a463d15dc4917b6$__9063cfb5192890610af690600401611adb565b60206040518083038186803b158015610b0e57600080fd5b505af4158015610b22573d6000803e3d6000fd5b505050506040513d601f19601f82011682018060405250610b4691908101906113d1565b60808201526040516319f6a32560e31b815273__$4f6e1f7166b61d394a3a463d15dc4917b6$__9063cfb5192890610b8090600401611afb565b60206040518083038186803b158015610b9857600080fd5b505af4158015610bac573d6000803e3d6000fd5b505050506040513d601f19601f82011682018060405250610bd091908101906113d1565b60e08201526040516319f6a32560e31b815273__$4f6e1f7166b61d394a3a463d15dc4917b6$__9063cfb5192890610c0a90600401611b2b565b60206040518083038186803b158015610c2257600080fd5b505af4158015610c36573d6000803e3d6000fd5b505050506040513d601f19601f82011682018060405250610c5a91908101906113d1565b6101208201526001600160a01b038216610140820152600a610160820152601061018082015261063e816107d3565b610c9660208201826114dd565b6001600160401b0316816080013582602001357f016ba557e3a1b9898203be6bed91c2397430775729448f081818eba5f850ceca846040516104c79190611b3b565b610ce560208201826114dd565b6001600160401b0316816080013582602001357f9db5b4a4a7ecec21ae845499c0e8787020b37c0b0dc0bf232992cc21ced9745f846040516104c79190611b3b565b6002546001600160a01b031681565b6000546001600160a01b03163314610d605760405162461bcd60e51b81526004016103af90611b1b565b610d6d60208201826114dd565b6001600160401b0316816080013582602001357f2f134e21437714f88cc4384c191fd15ca2cf634231049aeb585b2b4d96a93a92846040516104c79190611b3b565b6000546001600160a01b03163314610dd95760405162461bcd60e51b81526004016103af90611b1b565b610de660208201826114dd565b6001600160401b0316816080013582602001357fda1f8aa2f53b779066600c43d2d3e4671612b91421d9ad11c6d01310cb56004e846040516104c79190611b3b565b6000546001600160a01b03163314610e525760405162461bcd60e51b81526004016103af90611b1b565b610e5f60208201826114dd565b6001600160401b0316816080013582602001357fc89363be02d61b6dec61a4003ab05161bca7c4ab35f85c7c4fb2a99c05b004bb846040516104c79190611b3b565b6001546001600160a01b031681565b6040516370a0823160e01b815260009082906001600160a01b038216906370a082319061077a903390600401611a53565b6040805161022081018252600080825260208201819052918101829052606081018290526080810182905260a0810182905260c0810182905260e08101829052610100810182905261012081018290526101408101829052610160810182905261018081018290526101a081018290526101c08101829052906101e082019081526020016000905290565b8035610f7781611cef565b92915050565b8051610f7781611cef565b8051610f7781611d03565b8035610f7781611d0c565b8051610f7781611d0c565b8035610f7781611d15565b8051610f7781611d15565b8035610f7781611d22565b8051610f7781611d22565b600082601f830112610fe657600080fd5b8135610ff9610ff482611b8d565b611b67565b9150808252602083016020830185838301111561101557600080fd5b611020838284611c96565b50505092915050565b6000610220828403121561103c57600080fd5b50919050565b6000610220828403121561105557600080fd5b611060610220611b67565b9050600061106e8484611359565b825250602061107f84848301610f93565b602083015250604061109384828501610f93565b60408301525060606110a784828501610f93565b60608301525060806110bb84828501610f93565b60808301525060a06110cf84828501610f93565b60a08301525060c06110e384828501610f93565b60c08301525060e06110f784828501610f93565b60e08301525061010061110c84828501610f93565b6101008301525061012061112284828501610f93565b6101208301525061014061113884828501610f6c565b6101408301525061016061114e84828501611343565b6101608301525061018061116484828501611343565b610180830152506101a061117a84828501611343565b6101a0830152506101c061119084828501611343565b6101c0830152506101e06111a684828501610fa9565b6101e0830152506102006111bc84828501610fbf565b6102008301525092915050565b600061022082840312156111dc57600080fd5b6111e7610220611b67565b905060006111f58484611364565b825250602061120684848301610f9e565b602083015250604061121a84828501610f9e565b604083015250606061122e84828501610f9e565b606083015250608061124284828501610f9e565b60808301525060a061125684828501610f9e565b60a08301525060c061126a84828501610f9e565b60c08301525060e061127e84828501610f9e565b60e08301525061010061129384828501610f9e565b610100830152506101206112a984828501610f9e565b610120830152506101406112bf84828501610f7d565b610140830152506101606112d58482850161134e565b610160830152506101806112eb8482850161134e565b610180830152506101a06113018482850161134e565b6101a0830152506101c06113178482850161134e565b6101c0830152506101e061132d84828501610fb4565b6101e0830152506102006111bc84828501610fca565b8035610f7781611d2f565b8051610f7781611d2f565b8035610f7781611d38565b8051610f7781611d38565b60006020828403121561138157600080fd5b600061138d8484610f6c565b949350505050565b6000602082840312156113a757600080fd5b600061138d8484610f7d565b6000602082840312156113c557600080fd5b600061138d8484610f88565b6000602082840312156113e357600080fd5b600061138d8484610f9e565b60008060006060848603121561140457600080fd5b83356001600160401b0381111561141a57600080fd5b61142686828701610fd5565b93505060208401356001600160401b0381111561144257600080fd5b61144e86828701610fd5565b92505060408401356001600160401b0381111561146a57600080fd5b61147686828701610fd5565b9150509250925092565b6000610220828403121561149357600080fd5b600061138d8484611029565b600061022082840312156114b257600080fd5b600061138d8484611042565b600061022082840312156114d157600080fd5b600061138d84846111c9565b6000602082840312156114ef57600080fd5b600061138d8484611359565b61150481611c63565b82525050565b61150481611c1b565b61150481611c2b565b61150481611c6a565b61150481611c75565b61150481611c80565b600061154282611bb4565b61154c8185611bb8565b935061155c818560208601611ca2565b61156581611cce565b9093019392505050565b600061157c602983611bb8565b7f436f756c64206e6f742066696e6420506f4d61696e206164647265737320696e81526820726567697374727960b81b602082015260400192915050565b60006115c7601283611bb8565b7129b7bcb632b73a21b7b93837b930ba34b7b760711b815260200192915050565b60006115f5601083611bb8565b6f496e417070507572636861736531323360801b815260200192915050565b6000611621600883611bb8565b674248542d3131303160c01b815260200192915050565b6000611645603283611bb8565b7f436f756c64206e6f742066696e642046756e64696e67436f6e7472616374206181527164647265737320696e20726567697374727960701b602082015260400192915050565b6000611699602483611bb8565b7f46756e6374696f6e206d7573742062652063616c6c65642066726f6d20504f2081526326b0b4b760e11b602082015260400192915050565b60006116df600783611bb8565b66111052551154d560ca1b815260200192915050565b61022082016117048280611c0c565b61170e8482611a3c565b5061171c6020830183611bd0565b6117296020850182611513565b506117376040830183611bd0565b6117446040850182611513565b506117526060830183611bd0565b61175f6060850182611513565b5061176d6080830183611bd0565b61177a6080850182611513565b5061178860a0830183611bd0565b61179560a0850182611513565b506117a360c0830183611bd0565b6117b060c0850182611513565b506117be60e0830183611bd0565b6117cb60e0850182611513565b506117da610100830183611bd0565b6117e8610100850182611513565b506117f7610120830183611bd0565b611805610120850182611513565b50611814610140830183611bc1565b61182261014085018261150a565b50611831610160830183611bfd565b61183f610160850182611a33565b5061184e610180830183611bfd565b61185c610180850182611a33565b5061186b6101a0830183611bfd565b6118796101a0850182611a33565b506118886101c0830183611bfd565b6118966101c0850182611a33565b506118a56101e0830183611bdf565b6118b36101e0850182611525565b506118c2610200830183611bee565b6118d061020085018261152e565b50505050565b80516102208301906118e88482611a3c565b5060208201516118fb6020850182611513565b50604082015161190e6040850182611513565b5060608201516119216060850182611513565b5060808201516119346080850182611513565b5060a082015161194760a0850182611513565b5060c082015161195a60c0850182611513565b5060e082015161196d60e0850182611513565b50610100820151611982610100850182611513565b50610120820151611997610120850182611513565b506101408201516119ac61014085018261150a565b506101608201516119c1610160850182611a33565b506101808201516119d6610180850182611a33565b506101a08201516119eb6101a0850182611a33565b506101c0820151611a006101c0850182611a33565b506101e0820151611a156101e0850182611525565b506102008201516118d061020085018261152e565b61150481611c8b565b61150481611c4e565b61150481611c57565b60208101610f77828461150a565b60208101610f7782846114fb565b60408101611a6f828561150a565b611a7c6020830184611a2a565b9392505050565b60208101610f778284611513565b60408101611a9f8285611513565b611a7c6020830184611513565b60208101610f77828461151c565b60208082528101611a7c8184611537565b60208082528101610f778161156f565b60208082528101610f77816115ba565b60208082528101610f77816115e8565b60208082528101610f7781611614565b60208082528101610f7781611638565b60208082528101610f778161168c565b60208082528101610f77816116d2565b6102208101610f7782846116f5565b6102208101610f7782846118d6565b60208101610f778284611a3c565b6040518181016001600160401b0381118282101715611b8557600080fd5b604052919050565b60006001600160401b03821115611ba357600080fd5b506020601f91909101601f19160190565b5190565b90815260200190565b6000611a7c6020840184610f6c565b6000611a7c6020840184610f93565b6000611a7c6020840184610fa9565b6000611a7c6020840184610fbf565b6000611a7c6020840184611343565b6000611a7c6020840184611359565b6000610f7782611c42565b151590565b90565b806107ce81611cd8565b806107ce81611ce5565b6001600160a01b031690565b63ffffffff1690565b6001600160401b031690565b6000610f77825b6000610f7782611c1b565b6000610f7782611c2e565b6000610f7782611c38565b6000610f7782611c4e565b82818337506000910152565b60005b83811015611cbd578181015183820152602001611ca5565b838111156118d05750506000910152565b601f01601f191690565b60058110611ce257fe5b50565b600e8110611ce257fe5b611cf881611c1b565b8114611ce257600080fd5b611cf881611c26565b611cf881611c2b565b60058110611ce257600080fd5b600e8110611ce257600080fd5b611cf881611c4e565b611cf881611c5756fea365627a7a7230582055ed1afa858a40d61c837af9f9f29e9b6695a55f4355ac4af098ebe43e50ef7d6c6578706572696d656e74616cf564736f6c634300050a0040";
        public WalletBuyerDeploymentBase() : base(BYTECODE) { }
        public WalletBuyerDeploymentBase(string byteCode) : base(byteCode) { }
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

    public partial class OnSalesOrderNotApprovedFunction : OnSalesOrderNotApprovedFunctionBase { }

    [Function("onSalesOrderNotApproved")]
    public class OnSalesOrderNotApprovedFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "po", 1)]
        public virtual Po Po { get; set; }
    }

    public partial class CancelPurchaseOrderFunction : CancelPurchaseOrderFunctionBase { }

    [Function("cancelPurchaseOrder")]
    public class CancelPurchaseOrderFunctionBase : FunctionMessage
    {
        [Parameter("uint64", "ethPoNumber", 1)]
        public virtual ulong EthPoNumber { get; set; }
    }

    public partial class SystemIdFunction : SystemIdFunctionBase { }

    [Function("systemId", "bytes32")]
    public class SystemIdFunctionBase : FunctionMessage
    {

    }

    public partial class OnPurchasePaymentMadeOkFunction : OnPurchasePaymentMadeOkFunctionBase { }

    [Function("onPurchasePaymentMadeOk")]
    public class OnPurchasePaymentMadeOkFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "po", 1)]
        public virtual Po Po { get; set; }
    }

    public partial class PoMainFunction : PoMainFunctionBase { }

    [Function("poMain", "address")]
    public class PoMainFunctionBase : FunctionMessage
    {

    }

    public partial class OnPurchaseRefundMadeOkFunction : OnPurchaseRefundMadeOkFunctionBase { }

    [Function("onPurchaseRefundMadeOk")]
    public class OnPurchaseRefundMadeOkFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "po", 1)]
        public virtual Po Po { get; set; }
    }

    public partial class GetTokenBalanceOwnedByThisFunction : GetTokenBalanceOwnedByThisFunctionBase { }

    [Function("getTokenBalanceOwnedByThis", "uint256")]
    public class GetTokenBalanceOwnedByThisFunctionBase : FunctionMessage
    {
        [Parameter("address", "tokenAddress", 1)]
        public virtual string TokenAddress { get; set; }
    }

    public partial class CreatePurchaseOrderFunction : CreatePurchaseOrderFunctionBase { }

    [Function("createPurchaseOrder")]
    public class CreatePurchaseOrderFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "po", 1)]
        public virtual Po Po { get; set; }
    }

    public partial class CreatePurchaseOrderTestFunction : CreatePurchaseOrderTestFunctionBase { }

    [Function("createPurchaseOrderTest")]
    public class CreatePurchaseOrderTestFunctionBase : FunctionMessage
    {
        [Parameter("address", "tokenAddress", 1)]
        public virtual string TokenAddress { get; set; }
    }

    public partial class OnSalesOrderCancelFailureFunction : OnSalesOrderCancelFailureFunctionBase { }

    [Function("onSalesOrderCancelFailure")]
    public class OnSalesOrderCancelFailureFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "po", 1)]
        public virtual Po Po { get; set; }
    }

    public partial class OnSalesOrderInvoiceFaultFunction : OnSalesOrderInvoiceFaultFunctionBase { }

    [Function("onSalesOrderInvoiceFault")]
    public class OnSalesOrderInvoiceFaultFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "po", 1)]
        public virtual Po Po { get; set; }
    }

    public partial class FundingContractFunction : FundingContractFunctionBase { }

    [Function("fundingContract", "address")]
    public class FundingContractFunctionBase : FunctionMessage
    {

    }

    public partial class OnPurchasePaymentFailedFunction : OnPurchasePaymentFailedFunctionBase { }

    [Function("onPurchasePaymentFailed")]
    public class OnPurchasePaymentFailedFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "po", 1)]
        public virtual Po Po { get; set; }
    }

    public partial class OnPurchaseRefundFailedFunction : OnPurchaseRefundFailedFunctionBase { }

    [Function("onPurchaseRefundFailed")]
    public class OnPurchaseRefundFailedFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "po", 1)]
        public virtual Po Po { get; set; }
    }

    public partial class OnPurchaseUpdatedWithSalesOrderFunction : OnPurchaseUpdatedWithSalesOrderFunctionBase { }

    [Function("onPurchaseUpdatedWithSalesOrder")]
    public class OnPurchaseUpdatedWithSalesOrderFunctionBase : FunctionMessage
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

    public partial class WalletPurchaseLogEventDTO : WalletPurchaseLogEventDTOBase { }

    [Event("WalletPurchaseLog")]
    public class WalletPurchaseLogEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "buyerSysId", 1, true )]
        public virtual byte[] BuyerSysId { get; set; }
        [Parameter("bytes32", "buyerPurchaseOrderNumber", 2, true )]
        public virtual byte[] BuyerPurchaseOrderNumber { get; set; }
        [Parameter("bytes32", "buyerProductId", 3, true )]
        public virtual byte[] BuyerProductId { get; set; }
        [Parameter("tuple", "po", 4, false )]
        public virtual Po Po { get; set; }
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

    public partial class PurchaseUpdatedWithSalesOrderOkLogEventDTO : PurchaseUpdatedWithSalesOrderOkLogEventDTOBase { }

    [Event("PurchaseUpdatedWithSalesOrderOkLog")]
    public class PurchaseUpdatedWithSalesOrderOkLogEventDTOBase : IEventDTO
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

    public partial class PurchasePaymentMadeOkLogEventDTO : PurchasePaymentMadeOkLogEventDTOBase { }

    [Event("PurchasePaymentMadeOkLog")]
    public class PurchasePaymentMadeOkLogEventDTOBase : IEventDTO
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

    public partial class PurchasePaymentFailedLogEventDTO : PurchasePaymentFailedLogEventDTOBase { }

    [Event("PurchasePaymentFailedLog")]
    public class PurchasePaymentFailedLogEventDTOBase : IEventDTO
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

    public partial class PurchaseRefundMadeOkLogEventDTO : PurchaseRefundMadeOkLogEventDTOBase { }

    [Event("PurchaseRefundMadeOkLog")]
    public class PurchaseRefundMadeOkLogEventDTOBase : IEventDTO
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

    public partial class PurchaseRefundFailedLogEventDTO : PurchaseRefundFailedLogEventDTOBase { }

    [Event("PurchaseRefundFailedLog")]
    public class PurchaseRefundFailedLogEventDTOBase : IEventDTO
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

    public partial class SalesOrderCancelFailedLogEventDTO : SalesOrderCancelFailedLogEventDTOBase { }

    [Event("SalesOrderCancelFailedLog")]
    public class SalesOrderCancelFailedLogEventDTOBase : IEventDTO
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

    public partial class SalesOrderNotApprovedLogEventDTO : SalesOrderNotApprovedLogEventDTOBase { }

    [Event("SalesOrderNotApprovedLog")]
    public class SalesOrderNotApprovedLogEventDTOBase : IEventDTO
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

    public partial class SalesOrderInvoiceFaultLogEventDTO : SalesOrderInvoiceFaultLogEventDTOBase { }

    [Event("SalesOrderInvoiceFaultLog")]
    public class SalesOrderInvoiceFaultLogEventDTOBase : IEventDTO
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
