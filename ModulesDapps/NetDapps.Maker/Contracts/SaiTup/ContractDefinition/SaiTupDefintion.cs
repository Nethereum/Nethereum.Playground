
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

namespace NetDapps.Maker.Contracts.SaiTup.ContractDefinition
{
    
        public partial class SaiTubDeployment : SaiTubDeploymentBase
        {
            public SaiTubDeployment() : base(BYTECODE) { }
            public SaiTubDeployment(string byteCode) : base(byteCode) { }
        }

        public class SaiTubDeploymentBase : ContractDeploymentMessage
        {
            public static string BYTECODE = "0x000";
            public SaiTubDeploymentBase() : base(BYTECODE) { }
            public SaiTubDeploymentBase(string byteCode) : base(byteCode) { }
            [Parameter("address", "sai_", 1)]
            public virtual string Sai_ { get; set; }
            [Parameter("address", "sin_", 2)]
            public virtual string Sin_ { get; set; }
            [Parameter("address", "skr_", 3)]
            public virtual string Skr_ { get; set; }
            [Parameter("address", "gem_", 4)]
            public virtual string Gem_ { get; set; }
            [Parameter("address", "gov_", 5)]
            public virtual string Gov_ { get; set; }
            [Parameter("address", "pip_", 6)]
            public virtual string Pip_ { get; set; }
            [Parameter("address", "pep_", 7)]
            public virtual string Pep_ { get; set; }
            [Parameter("address", "vox_", 8)]
            public virtual string Vox_ { get; set; }
            [Parameter("address", "pit_", 9)]
            public virtual string Pit_ { get; set; }
        }

        public partial class JoinFunction : JoinFunctionBase { }

        [Function("join")]
        public class JoinFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "wad", 1)]
            public virtual BigInteger Wad { get; set; }
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

        public partial class GovFunction : GovFunctionBase { }

        [Function("gov", "address")]
        public class GovFunctionBase : FunctionMessage
        {

        }

        public partial class SetOwnerFunction : SetOwnerFunctionBase { }

        [Function("setOwner")]
        public class SetOwnerFunctionBase : FunctionMessage
        {
            [Parameter("address", "owner_", 1)]
            public virtual string Owner_ { get; set; }
        }

        public partial class EraFunction : EraFunctionBase { }

        [Function("era", "uint256")]
        public class EraFunctionBase : FunctionMessage
        {

        }

        public partial class InkFunction : InkFunctionBase { }

        [Function("ink", "uint256")]
        public class InkFunctionBase : FunctionMessage
        {
            [Parameter("bytes32", "cup", 1)]
            public virtual byte[] Cup { get; set; }
        }

        public partial class RhoFunction : RhoFunctionBase { }

        [Function("rho", "uint256")]
        public class RhoFunctionBase : FunctionMessage
        {

        }

        public partial class AirFunction : AirFunctionBase { }

        [Function("air", "uint256")]
        public class AirFunctionBase : FunctionMessage
        {

        }

        public partial class RhiFunction : RhiFunctionBase { }

        [Function("rhi", "uint256")]
        public class RhiFunctionBase : FunctionMessage
        {

        }

        public partial class FlowFunction : FlowFunctionBase { }

        [Function("flow")]
        public class FlowFunctionBase : FunctionMessage
        {

        }

        public partial class CapFunction : CapFunctionBase { }

        [Function("cap", "uint256")]
        public class CapFunctionBase : FunctionMessage
        {

        }

        public partial class BiteFunction : BiteFunctionBase { }

        [Function("bite")]
        public class BiteFunctionBase : FunctionMessage
        {
            [Parameter("bytes32", "cup", 1)]
            public virtual byte[] Cup { get; set; }
        }

        public partial class DrawFunction : DrawFunctionBase { }

        [Function("draw")]
        public class DrawFunctionBase : FunctionMessage
        {
            [Parameter("bytes32", "cup", 1)]
            public virtual byte[] Cup { get; set; }
            [Parameter("uint256", "wad", 2)]
            public virtual BigInteger Wad { get; set; }
        }

        public partial class BidFunction : BidFunctionBase { }

        [Function("bid", "uint256")]
        public class BidFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "wad", 1)]
            public virtual BigInteger Wad { get; set; }
        }

        public partial class CupiFunction : CupiFunctionBase { }

        [Function("cupi", "uint256")]
        public class CupiFunctionBase : FunctionMessage
        {

        }

        public partial class AxeFunction : AxeFunctionBase { }

        [Function("axe", "uint256")]
        public class AxeFunctionBase : FunctionMessage
        {

        }

        public partial class TagFunction : TagFunctionBase { }

        [Function("tag", "uint256")]
        public class TagFunctionBase : FunctionMessage
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

        public partial class RapFunction : RapFunctionBase { }

        [Function("rap", "uint256")]
        public class RapFunctionBase : FunctionMessage
        {
            [Parameter("bytes32", "cup", 1)]
            public virtual byte[] Cup { get; set; }
        }

        public partial class WipeFunction : WipeFunctionBase { }

        [Function("wipe")]
        public class WipeFunctionBase : FunctionMessage
        {
            [Parameter("bytes32", "cup", 1)]
            public virtual byte[] Cup { get; set; }
            [Parameter("uint256", "wad", 2)]
            public virtual BigInteger Wad { get; set; }
        }

        public partial class SetAuthorityFunction : SetAuthorityFunctionBase { }

        [Function("setAuthority")]
        public class SetAuthorityFunctionBase : FunctionMessage
        {
            [Parameter("address", "authority_", 1)]
            public virtual string Authority_ { get; set; }
        }

        public partial class GemFunction : GemFunctionBase { }

        [Function("gem", "address")]
        public class GemFunctionBase : FunctionMessage
        {

        }

        public partial class TurnFunction : TurnFunctionBase { }

        [Function("turn")]
        public class TurnFunctionBase : FunctionMessage
        {
            [Parameter("address", "tap_", 1)]
            public virtual string Tap_ { get; set; }
        }

        public partial class PerFunction : PerFunctionBase { }

        [Function("per", "uint256")]
        public class PerFunctionBase : FunctionMessage
        {

        }

        public partial class ExitFunction : ExitFunctionBase { }

        [Function("exit")]
        public class ExitFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "wad", 1)]
            public virtual BigInteger Wad { get; set; }
        }

        public partial class SetPipFunction : SetPipFunctionBase { }

        [Function("setPip")]
        public class SetPipFunctionBase : FunctionMessage
        {
            [Parameter("address", "pip_", 1)]
            public virtual string Pip_ { get; set; }
        }

        public partial class PieFunction : PieFunctionBase { }

        [Function("pie", "uint256")]
        public class PieFunctionBase : FunctionMessage
        {

        }

        public partial class CageFunction : CageFunctionBase { }

        [Function("cage")]
        public class CageFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "fit_", 1)]
            public virtual BigInteger Fit_ { get; set; }
            [Parameter("uint256", "jam", 2)]
            public virtual BigInteger Jam { get; set; }
        }

        public partial class RumFunction : RumFunctionBase { }

        [Function("rum", "uint256")]
        public class RumFunctionBase : FunctionMessage
        {

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

        public partial class TaxFunction : TaxFunctionBase { }

        [Function("tax", "uint256")]
        public class TaxFunctionBase : FunctionMessage
        {

        }

        public partial class DripFunction : DripFunctionBase { }

        [Function("drip")]
        public class DripFunctionBase : FunctionMessage
        {

        }

        public partial class FreeFunction : FreeFunctionBase { }

        [Function("free")]
        public class FreeFunctionBase : FunctionMessage
        {
            [Parameter("bytes32", "cup", 1)]
            public virtual byte[] Cup { get; set; }
            [Parameter("uint256", "wad", 2)]
            public virtual BigInteger Wad { get; set; }
        }

        public partial class MatFunction : MatFunctionBase { }

        [Function("mat", "uint256")]
        public class MatFunctionBase : FunctionMessage
        {

        }

        public partial class PepFunction : PepFunctionBase { }

        [Function("pep", "address")]
        public class PepFunctionBase : FunctionMessage
        {

        }

        public partial class OutFunction : OutFunctionBase { }

        [Function("out", "bool")]
        public class OutFunctionBase : FunctionMessage
        {

        }

        public partial class LockFunction : LockFunctionBase { }

        [Function("lock")]
        public class LockFunctionBase : FunctionMessage
        {
            [Parameter("bytes32", "cup", 1)]
            public virtual byte[] Cup { get; set; }
            [Parameter("uint256", "wad", 2)]
            public virtual BigInteger Wad { get; set; }
        }

        public partial class ShutFunction : ShutFunctionBase { }

        [Function("shut")]
        public class ShutFunctionBase : FunctionMessage
        {
            [Parameter("bytes32", "cup", 1)]
            public virtual byte[] Cup { get; set; }
        }

        public partial class GiveFunction : GiveFunctionBase { }

        [Function("give")]
        public class GiveFunctionBase : FunctionMessage
        {
            [Parameter("bytes32", "cup", 1)]
            public virtual byte[] Cup { get; set; }
            [Parameter("address", "guy", 2)]
            public virtual string Guy { get; set; }
        }

        public partial class AuthorityFunction : AuthorityFunctionBase { }

        [Function("authority", "address")]
        public class AuthorityFunctionBase : FunctionMessage
        {

        }

        public partial class FitFunction : FitFunctionBase { }

        [Function("fit", "uint256")]
        public class FitFunctionBase : FunctionMessage
        {

        }

        public partial class ChiFunction : ChiFunctionBase { }

        [Function("chi", "uint256")]
        public class ChiFunctionBase : FunctionMessage
        {

        }

        public partial class SetVoxFunction : SetVoxFunctionBase { }

        [Function("setVox")]
        public class SetVoxFunctionBase : FunctionMessage
        {
            [Parameter("address", "vox_", 1)]
            public virtual string Vox_ { get; set; }
        }

        public partial class PipFunction : PipFunctionBase { }

        [Function("pip", "address")]
        public class PipFunctionBase : FunctionMessage
        {

        }

        public partial class SetPepFunction : SetPepFunctionBase { }

        [Function("setPep")]
        public class SetPepFunctionBase : FunctionMessage
        {
            [Parameter("address", "pep_", 1)]
            public virtual string Pep_ { get; set; }
        }

        public partial class FeeFunction : FeeFunctionBase { }

        [Function("fee", "uint256")]
        public class FeeFunctionBase : FunctionMessage
        {

        }

        public partial class LadFunction : LadFunctionBase { }

        [Function("lad", "address")]
        public class LadFunctionBase : FunctionMessage
        {
            [Parameter("bytes32", "cup", 1)]
            public virtual byte[] Cup { get; set; }
        }

        public partial class DinFunction : DinFunctionBase { }

        [Function("din", "uint256")]
        public class DinFunctionBase : FunctionMessage
        {

        }

        public partial class AskFunction : AskFunctionBase { }

        [Function("ask", "uint256")]
        public class AskFunctionBase : FunctionMessage
        {
            [Parameter("uint256", "wad", 1)]
            public virtual BigInteger Wad { get; set; }
        }

        public partial class SafeFunction : SafeFunctionBase { }

        [Function("safe", "bool")]
        public class SafeFunctionBase : FunctionMessage
        {
            [Parameter("bytes32", "cup", 1)]
            public virtual byte[] Cup { get; set; }
        }

        public partial class PitFunction : PitFunctionBase { }

        [Function("pit", "address")]
        public class PitFunctionBase : FunctionMessage
        {

        }

        public partial class TabFunction : TabFunctionBase { }

        [Function("tab", "uint256")]
        public class TabFunctionBase : FunctionMessage
        {
            [Parameter("bytes32", "cup", 1)]
            public virtual byte[] Cup { get; set; }
        }

        public partial class OpenFunction : OpenFunctionBase { }

        [Function("open", "bytes32")]
        public class OpenFunctionBase : FunctionMessage
        {

        }

        public partial class TapFunction : TapFunctionBase { }

        [Function("tap", "address")]
        public class TapFunctionBase : FunctionMessage
        {

        }

        public partial class CupsFunction : CupsFunctionBase { }

        [Function("cups", typeof(CupsOutputDTO))]
        public class CupsFunctionBase : FunctionMessage
        {
            [Parameter("bytes32", "", 1)]
            public virtual byte[] ReturnValue1 { get; set; }
        }

        public partial class LogNewCupEventDTO : LogNewCupEventDTOBase { }

        [Event("LogNewCup")]
        public class LogNewCupEventDTOBase : IEventDTO
        {
            [Parameter("address", "lad", 1, true)]
            public virtual string Lad { get; set; }
            [Parameter("bytes32", "cup", 2, false)]
            public virtual byte[] Cup { get; set; }
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

        public partial class GovOutputDTO : GovOutputDTOBase { }

        [FunctionOutput]
        public class GovOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "", 1)]
            public virtual string ReturnValue1 { get; set; }
        }



        public partial class EraOutputDTO : EraOutputDTOBase { }

        [FunctionOutput]
        public class EraOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }

        public partial class InkOutputDTO : InkOutputDTOBase { }

        [FunctionOutput]
        public class InkOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }

        public partial class RhoOutputDTO : RhoOutputDTOBase { }

        [FunctionOutput]
        public class RhoOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }

        public partial class AirOutputDTO : AirOutputDTOBase { }

        [FunctionOutput]
        public class AirOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }





        public partial class CapOutputDTO : CapOutputDTOBase { }

        [FunctionOutput]
        public class CapOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }





        public partial class BidOutputDTO : BidOutputDTOBase { }

        [FunctionOutput]
        public class BidOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }

        public partial class CupiOutputDTO : CupiOutputDTOBase { }

        [FunctionOutput]
        public class CupiOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }

        public partial class AxeOutputDTO : AxeOutputDTOBase { }

        [FunctionOutput]
        public class AxeOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }

        public partial class TagOutputDTO : TagOutputDTOBase { }

        [FunctionOutput]
        public class TagOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "wad", 1)]
            public virtual BigInteger Wad { get; set; }
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







        public partial class GemOutputDTO : GemOutputDTOBase { }

        [FunctionOutput]
        public class GemOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "", 1)]
            public virtual string ReturnValue1 { get; set; }
        }



        public partial class PerOutputDTO : PerOutputDTOBase { }

        [FunctionOutput]
        public class PerOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "ray", 1)]
            public virtual BigInteger Ray { get; set; }
        }





        public partial class PieOutputDTO : PieOutputDTOBase { }

        [FunctionOutput]
        public class PieOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }



        public partial class RumOutputDTO : RumOutputDTOBase { }

        [FunctionOutput]
        public class RumOutputDTOBase : IFunctionOutputDTO
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



        public partial class TaxOutputDTO : TaxOutputDTOBase { }

        [FunctionOutput]
        public class TaxOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }





        public partial class MatOutputDTO : MatOutputDTOBase { }

        [FunctionOutput]
        public class MatOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }

        public partial class PepOutputDTO : PepOutputDTOBase { }

        [FunctionOutput]
        public class PepOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "", 1)]
            public virtual string ReturnValue1 { get; set; }
        }

        public partial class OutOutputDTO : OutOutputDTOBase { }

        [FunctionOutput]
        public class OutOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("bool", "", 1)]
            public virtual bool ReturnValue1 { get; set; }
        }







        public partial class AuthorityOutputDTO : AuthorityOutputDTOBase { }

        [FunctionOutput]
        public class AuthorityOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "", 1)]
            public virtual string ReturnValue1 { get; set; }
        }

        public partial class FitOutputDTO : FitOutputDTOBase { }

        [FunctionOutput]
        public class FitOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }





        public partial class PipOutputDTO : PipOutputDTOBase { }

        [FunctionOutput]
        public class PipOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "", 1)]
            public virtual string ReturnValue1 { get; set; }
        }



        public partial class FeeOutputDTO : FeeOutputDTOBase { }

        [FunctionOutput]
        public class FeeOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }

        public partial class LadOutputDTO : LadOutputDTOBase { }

        [FunctionOutput]
        public class LadOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "", 1)]
            public virtual string ReturnValue1 { get; set; }
        }



        public partial class AskOutputDTO : AskOutputDTOBase { }

        [FunctionOutput]
        public class AskOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }

        public partial class PitOutputDTO : PitOutputDTOBase { }

        [FunctionOutput]
        public class PitOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "", 1)]
            public virtual string ReturnValue1 { get; set; }
        }



        public partial class TapOutputDTO : TapOutputDTOBase { }

        [FunctionOutput]
        public class TapOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "", 1)]
            public virtual string ReturnValue1 { get; set; }
        }

        public partial class CupsOutputDTO : CupsOutputDTOBase { }

        [FunctionOutput]
        public class CupsOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("address", "lad", 1)]
            public virtual string Lad { get; set; }
            [Parameter("uint256", "ink", 2)]
            public virtual BigInteger Ink { get; set; }
            [Parameter("uint256", "art", 3)]
            public virtual BigInteger Art { get; set; }
            [Parameter("uint256", "ire", 4)]
            public virtual BigInteger Ire { get; set; }
        }
}
