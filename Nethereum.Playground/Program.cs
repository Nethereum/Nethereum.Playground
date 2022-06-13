using Blazor.Extensions.Storage;
using Blazor.FileReader;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NetDapps.Host;
using Nethereum.Playground.Components.Sqlite;
using Nethereum.Playground.ServiceCollectionExtensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nethereum.Playground
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddAuthorizationCore();
            builder.Services.AddStorage();
            builder.Services.AddBlazorModal();
            builder.Services.AddCodeRepository();
            builder.Services.AddCompiler();
            builder.Services.AddAssemblyLoadingInitialiser();
            builder.Services.AddFileReaderService();
            builder.Services.AddMetamask();
            SQLitePCL.raw.SetProvider(new SQLite3Provider_WebAssembly());
            var host = builder.Build().RunAsync();
        }
       
    }
}
