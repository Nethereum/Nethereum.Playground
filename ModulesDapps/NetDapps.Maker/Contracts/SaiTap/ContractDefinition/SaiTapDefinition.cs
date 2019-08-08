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

namespace NetDapps.Maker.Contracts.SaiTap.ContractDefinition
{
        public partial class SaiTapDeployment : SaiTapDeploymentBase
        {
            public SaiTapDeployment() : base(BYTECODE) { }
            public SaiTapDeployment(string byteCode) : base(byteCode) { }
        }

        public class SaiTapDeploymentBase : ContractDeploymentMessage
        {
            public static string BYTECODE = "0x000";
            public SaiTapDeploymentBase() : base(BYTECODE) { }
            public SaiTapDeploymentBase(string byteCode) : base(byteCode) { }
            [Parameter("address", "tub_", 1)]
            public virtual string Tub_ { get; set; }
        }

        public partial class HealFunction : HealFunctionBase { }

        [Function("heal")]
        public class HealFunctionBase : FunctionMessage
        {

        }

        public partial class SinFunction : SinFunctionBase { }

        [Function("sin", "address")]
        public class SinFunctionBase : FunctionMessage
        {

        }

        public partial class SkrFunction : SkrFunctionBase { }

        [Function("skr", "address")]
        public class SkrFunctionBase : FunctionMessage
        {

        }

        public partial class SetOwnerFunction : SetOwnerFunctionBase { }

        [Function("setOwner")]
        public class SetOwnerFunctionBase : FunctionMessage
        {
            [Parameter("address", "owner_", 1)]
            public virtual string Owner_ { get; set; }
        }

        public partial class VentFunction : VentFunctionBase { }

        [Function("vent")]
        public class VentFunctionBase : FunctionMessage
        {

        }

        public partial class CashFunction : CashFunctionBase { }

        [Function("cash")]
        public class CashFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "wad", 1)]
            public virtual BigInteger Wad { get; set; }
        }

        public partial class WoeFunction : WoeFunctionBase { }

        [Function("woe", "uint256")]
        public class WoeFunctionBase : FunctionMessage
        {

        }

        public partial class TubFunction : TubFunctionBase { }

        [Function("tub", "address")]
        public class TubFunctionBase : FunctionMessage
        {

        }

        public partial class MockFunction : MockFunctionBase { }

        [Function("mock")]
        public class MockFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "wad", 1)]
            public virtual BigInteger Wad { get; set; }
        }

        public partial class BidFunction : BidFunctionBase { }

        [Function("bid", "uint256")]
        public class BidFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "wad", 1)]
            public virtual BigInteger Wad { get; set; }
        }

        public partial class JoyFunction : JoyFunctionBase { }

        [Function("joy", "uint256")]
        public class JoyFunctionBase : FunctionMessage
        {

        }

        public partial class S2sFunction : S2sFunctionBase { }

        [Function("s2s", "uint256")]
        public class S2sFunctionBase : FunctionMessage
        {

        }

        public partial class OffFunction : OffFunctionBase { }

        [Function("off", "bool")]
        public class OffFunctionBase : FunctionMessage
        {

        }

        public partial class VoxFunction : VoxFunctionBase { }

        [Function("vox", "address")]
        public class VoxFunctionBase : FunctionMessage
        {

        }

        public partial class GapFunction : GapFunctionBase { }

        [Function("gap", "uint256")]
        public class GapFunctionBase : FunctionMessage
        {

        }

        public partial class FogFunction : FogFunctionBase { }

        [Function("fog", "uint256")]
        public class FogFunctionBase : FunctionMessage
        {

        }

        public partial class SetAuthorityFunction : SetAuthorityFunctionBase { }

        [Function("setAuthority")]
        public class SetAuthorityFunctionBase : FunctionMessage
        {
            [Parameter("address", "authority_", 1)]
            public virtual string Authority_ { get; set; }
        }

        public partial class OwnerFunction : OwnerFunctionBase { }

        [Function("owner", "address")]
        public class OwnerFunctionBase : FunctionMessage
        {

        }

        public partial class SaiFunction : SaiFunctionBase { }

        [Function("sai", "address")]
        public class SaiFunctionBase : FunctionMessage
        {

        }

        public partial class MoldFunction : MoldFunctionBase { }

        [Function("mold")]
        public class MoldFunctionBase : FunctionMessage
        {
            [Parameter("bytes32", "param", 1)]
            public virtual byte[] Param { get; set; }
            [Parameter("uint256", "val", 2)]
            public virtual BigInteger Val { get; set; }
        }

        public partial class CageFunction : CageFunctionBase { }

        [Function("cage")]
        public class CageFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "fix_", 1)]
            public virtual BigInteger Fix_ { get; set; }
        }

        public partial class FixFunction : FixFunctionBase { }

        [Function("fix", "uint256")]
        public class FixFunctionBase : FunctionMessage
        {

        }

        public partial class BustFunction : BustFunctionBase { }

        [Function("bust")]
        public class BustFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "wad", 1)]
            public virtual BigInteger Wad { get; set; }
        }

        public partial class BoomFunction : BoomFunctionBase { }

        [Function("boom")]
        public class BoomFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "wad", 1)]
            public virtual BigInteger Wad { get; set; }
        }

        public partial class AuthorityFunction : AuthorityFunctionBase { }

        [Function("authority", "address")]
        public class AuthorityFunctionBase : FunctionMessage
        {

        }

        public partial class AskFunction : AskFunctionBase { }

        [Function("ask", "uint256")]
        public class AskFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "wad", 1)]
            public virtual BigInteger Wad { get; set; }
        }

        public partial class LogNoteEventDTO : LogNoteEventDTOBase { }

        [Event("LogNote")]
        public class LogNoteEventDTOBase : IEventDTO
        {
            [Parameter("bytes4", "sig", 1, true)]
            public virtual byte[] Sig { get; set; }
            [Parameter("address", "guy", 2, true)]
            public virtual string Guy { get; set; }
            [Parameter("bytes32", "foo", 3, true)]
            public virtual byte[] Foo { get; set; }
            [Parameter("bytes32", "bar", 4, true)]
            public virtual byte[] Bar { get; set; }
            [Parameter("uint256", "wad", 5, false)]
            public virtual BigInteger Wad { get; set; }
            [Parameter("bytes", "fax", 6, false)]
            public virtual byte[] Fax { get; set; }
        }

        public partial class LogSetAuthorityEventDTO : LogSetAuthorityEventDTOBase { }

        [Event("LogSetAuthority")]
        public class LogSetAuthorityEventDTOBase : IEventDTO
        {
            [Parameter("address", "authority", 1, true)]
            public virtual string Authority { get; set; }
        }

        public partial class LogSetOwnerEventDTO : LogSetOwnerEventDTOBase { }

        [Event("LogSetOwner")]
        public class LogSetOwnerEventDTOBase : IEventDTO
        {
            [Parameter("address", "owner", 1, true)]
            public virtual string Owner { get; set; }
        }



        public partial class SinOutputDTO : SinOutputDTOBase { }

        [FunctionOutput]
        public class SinOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "", 1)]
            public virtual string ReturnValue1 { get; set; }
        }

        public partial class SkrOutputDTO : SkrOutputDTOBase { }

        [FunctionOutput]
        public class SkrOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "", 1)]
            public virtual string ReturnValue1 { get; set; }
        }


        public partial class WoeOutputDTO : WoeOutputDTOBase { }

        [FunctionOutput]
        public class WoeOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }

        public partial class TubOutputDTO : TubOutputDTOBase { }

        [FunctionOutput]
        public class TubOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "", 1)]
            public virtual string ReturnValue1 { get; set; }
        }

        public partial class JoyOutputDTO : JoyOutputDTOBase { }

        [FunctionOutput]
        public class JoyOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }

        public partial class OffOutputDTO : OffOutputDTOBase { }

        [FunctionOutput]
        public class OffOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("bool", "", 1)]
            public virtual bool ReturnValue1 { get; set; }
        }

        public partial class VoxOutputDTO : VoxOutputDTOBase { }

        [FunctionOutput]
        public class VoxOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "", 1)]
            public virtual string ReturnValue1 { get; set; }
        }

        public partial class GapOutputDTO : GapOutputDTOBase { }

        [FunctionOutput]
        public class GapOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }

        public partial class FogOutputDTO : FogOutputDTOBase { }

        [FunctionOutput]
        public class FogOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }


        public partial class OwnerOutputDTO : OwnerOutputDTOBase { }

        [FunctionOutput]
        public class OwnerOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "", 1)]
            public virtual string ReturnValue1 { get; set; }
        }

        public partial class SaiOutputDTO : SaiOutputDTOBase { }

        [FunctionOutput]
        public class SaiOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "", 1)]
            public virtual string ReturnValue1 { get; set; }
        }

        public partial class FixOutputDTO : FixOutputDTOBase { }

        [FunctionOutput]
        public class FixOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }

        public partial class AuthorityOutputDTO : AuthorityOutputDTOBase { }

        [FunctionOutput]
        public class AuthorityOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "", 1)]
            public virtual string ReturnValue1 { get; set; }
        }
    }

