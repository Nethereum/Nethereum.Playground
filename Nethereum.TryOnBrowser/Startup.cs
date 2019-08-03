using Blazor.Extensions.Storage;
using Microsoft.AspNetCore.Components.Builder;
using Nethereum.TryOnBrowser.ServiceCollectionExtensions;
using Microsoft.Extensions.DependencyInjection;
using Blazor.FileReader;

namespace Nethereum.TryOnBrowser
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddStorage();
            services.AddBlazorModal();
            services.AddCodeRepository();
            services.AddFileReaderService();


        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            //adding web3 and accounts to ensure they get included
            //this is a workaround it will be removed in the future
            var web3 = new Nethereum.Web3.Web3();
            var account = new Nethereum.Web3.Accounts.Managed.ManagedAccount("", "");
            var words = "ripple scissors kick mammal hire column oak again sun offer wealth tomorrow wagon turn fatal";
            var wallet = new Nethereum.HdWallet.Wallet(words, null);
            
            app.AddComponent<App>("app");
        }
    }
}
