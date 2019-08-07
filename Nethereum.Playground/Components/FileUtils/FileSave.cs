using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Nethereum.Playground.Components.FileUtils
{
    //Credit Dan Roth
    //https://github.com/danroth27/BlazorExcelSpreadsheet
    public static class FileUtil
    {
        public static Task SaveAs(this IJSRuntime js, string filename, byte[] data)
            => js.InvokeAsync<object>(
                "saveAsFile",
                filename,
                Convert.ToBase64String(data));
    }
    
}
