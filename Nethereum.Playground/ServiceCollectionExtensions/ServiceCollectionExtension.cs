using Microsoft.Extensions.DependencyInjection;
using NetDapps.Assemblies;
using Nethereum.Playground.Components.Modal;
using Nethereum.Playground.Pages;
using Nethereum.Playground.Repositories;
using Nethereum.Playground.Services;

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
            return services.AddScoped<CodeSampleRepository>();
        }

        public static IServiceCollection AddCompiler(this IServiceCollection services)
        {
            return services.AddScoped<Compiler>();
        }

        public static IServiceCollection AddAssemblyLoadingInitialiser(this IServiceCollection services)
        {
            return services.AddScoped<IAssemblyCacheInitialiser, PlaygroundAssemblyCacheInitialiser>();
        }
    }
}
