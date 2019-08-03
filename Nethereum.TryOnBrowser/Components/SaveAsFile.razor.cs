using System;
using System.Linq;
using System.Threading.Tasks;
using Blazor.FileReader;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nethereum.TryOnBrowser.Components.Modal;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Nethereum.TryOnBrowser.Components
{
    public class SaveAsFileBase: ComponentBase
    {
        [Parameter]
        public SaveAsFileModel Model { get; set; }

    }
}