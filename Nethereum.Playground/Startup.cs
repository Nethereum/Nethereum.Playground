using System.Text.Json.Serialization;
using Blazor.Extensions.Storage;
using Microsoft.AspNetCore.Components.Builder;
using Nethereum.Playground.ServiceCollectionExtensions;
using Microsoft.Extensions.DependencyInjection;
using Blazor.FileReader;
using Nethereum.Playground.Components.Sqlite;
using Microsoft.CodeAnalysis.CSharp.AddAccessibilityModifiers;

namespace Nethereum.Playground
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddStorage();
            services.AddBlazorModal();
            services.AddCodeRepository();
            services.AddCompiler();
            services.AddFileReaderService();
            services.AddGitterAuth();
            SQLitePCL.raw.SetProvider(new SQLite3Provider_WebAssembly());

            services.AddAuthorizationCore();
            //options =>
            //{
            //    options.AddPolicy("read:weather_forecast", policy => policy.RequireClaim("read:weather_forecast"));
            //});

        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            //adding web3 and accounts to ensure they get included
            //this is a workaround it will be removed in the future
            //var web3 = new Nethereum.Web3.Web3();
            var js = Newtonsoft.Json.ConstructorHandling.AllowNonPublicDefaultConstructor;
            var jsarray = new Newtonsoft.Json.Linq.JArray();
            
            //hack workspace
            var x = Microsoft.CodeAnalysis.CSharp.Formatting.CSharpFormattingOptions.IndentBlock;
            //var y = Microsoft.CodeAnalysis.CSharp.CSharpFeaturesResources
            //var account = new Nethereum.Web3.Accounts.Managed.ManagedAccount("", "");
            //var words = "ripple scissors kick mammal hire column oak again sun offer wealth tomorrow wagon turn fatal";
            //var wallet = new Nethereum.HdWallet.Wallet(words, null);

            app.AddComponent<App>("app");
        }
    }
}
