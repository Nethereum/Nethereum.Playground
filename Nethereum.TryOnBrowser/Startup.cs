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
            //adding assemblies to ensure they get included
            var web3 = new Nethereum.Web3.Web3();
            var account = new Nethereum.Web3.Accounts.Managed.ManagedAccount("", "");
            // string Words = "ripple scissors kick mammal hire column oak again sun offer wealth tomorrow wagon turn fatal";
            // string Password1 = "password";
            // var wallet1 = new Nethereum.HdWallet.Wallet(Words,Password1);
            NBitcoin.Mnemonic mnemo = new NBitcoin.Mnemonic(NBitcoin.Wordlist.English, NBitcoin.WordCount.Twelve);
            app.AddComponent<App>("app");
        }
    }
}
