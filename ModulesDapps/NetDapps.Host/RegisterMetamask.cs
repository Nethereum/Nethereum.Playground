using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.Blazor;
using Nethereum.Metamask;
using Nethereum.Metamask.Blazor;
using Nethereum.UI;

namespace NetDapps.Host
{
    public static class RegisterMetamask
    {
        public static IServiceCollection AddMetamask(this IServiceCollection services)
        {
            services.AddSingleton<IMetamaskInterop, MetamaskBlazorInterop>();
            services.AddSingleton<MetamaskInterceptor>();
            services.AddSingleton<MetamaskHostProvider>();
            services.AddSingleton<SelectedEthereumHostProviderService>();
            services.AddSingleton<AuthenticationStateProvider, EthereumAuthenticationStateProvider>();
            return services;
        }
    }
}
