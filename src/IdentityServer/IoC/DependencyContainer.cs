using IdentityServer.ExternalProviders.Extensions;
using IdentityServer.Infrastructure.Configuration.Extensions;
using IdentityServer.UI.Auth.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace IdentityServer.IoC
{
    internal static class DependencyContainer
    {
        internal static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //Run services
            services.AddInfrastructure(configuration);
            services.AddAuthentication()
                .AddExternalProviders(configuration);
            
            services
                .AddUi()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityServer", Version = "v1" });
                });
            
            services.AddLogging();
            return services;
        }
    }
}