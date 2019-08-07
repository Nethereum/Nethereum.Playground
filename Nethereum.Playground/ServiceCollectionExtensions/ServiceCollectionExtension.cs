using Microsoft.Extensions.DependencyInjection;
using Nethereum.Playground.Components.Modal;
using Nethereum.Playground.Pages;
using Nethereum.Playground.Repositories;

namespace Nethereum.Playground.ServiceCollectionExtensions
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
