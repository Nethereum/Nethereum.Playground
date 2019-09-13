using Blazor.Auth0;
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


        public static IServiceCollection AddGitterAuth(this IServiceCollection services)
        {
            return services.AddBlazorAuth0(options =>
            {
                //Auth0_Tenant_Domain
                options.Domain = "dev-i8jaj6-9.eu.auth0.com";

                // Auth0_Client_Id
                options.ClientId = "6YlNHqlfdLu546IGgHhiIixe5xw4HdgA";

               // options.RequireAuthenticatedUser = false;

                
                //// Required if you want to make use of Auth0's RBAC
                //options.Audience = "[Auth0_Audience]";
            });
        }
    }
}
