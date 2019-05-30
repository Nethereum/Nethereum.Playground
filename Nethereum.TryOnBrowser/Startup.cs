using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.Web3;

namespace Nethereum.TryOnBrowser
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            var web3 = new Nethereum.Web3.Web3();
            app.AddComponent<App>("app");
        }
    }
}
