using Nethereum.TryOnBrowser.Modal;
using Microsoft.Extensions.DependencyInjection;

namespace Nethereum.TryOnBrowser.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorModal(this IServiceCollection services)
        {
            return services.AddScoped<ModalService>();
        }
    }
}
