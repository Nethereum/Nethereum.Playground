using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Nethereum.TryOnBrowser
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            //adding web3 and accounts to ensure they get included
            var web3 = new Nethereum.Web3.Web3();
            var account = new Nethereum.Web3.Accounts.Managed.ManagedAccount("", "");
            var wallet = new Nethereum.HdWallet.Wallet("","");
            app.AddComponent<App>("app");
        }
    }
}
