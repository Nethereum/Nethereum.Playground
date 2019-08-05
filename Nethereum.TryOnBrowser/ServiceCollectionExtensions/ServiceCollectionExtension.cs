using Microsoft.Extensions.DependencyInjection;
using Nethereum.TryOnBrowser.Components.Modal;
using Nethereum.TryOnBrowser.Pages;
using Nethereum.TryOnBrowser.Repositories;

namespace Nethereum.TryOnBrowser.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorModal(this IServiceCollection services)
        {
            return services.AddScoped<ModalService>();
        }

        public static IServiceCollection AddCodeRepository(this IServiceCollection services)
        { 
            return services.AddSingleton<CodeSampleRepository>();
        }

        public static IServiceCollection AddCompiler(this IServiceCollection services)
        {
            return services.AddSingleton<Compiler>();
        }
    }
}
