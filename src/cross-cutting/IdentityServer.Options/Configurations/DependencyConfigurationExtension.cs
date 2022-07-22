using IdentityServer.Options.Configurations.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace IdentityServer.Options.Configurations
{
    public static class DependencyConfigurationExtension
    {
        public static IConfiguration ReadConfigurationEnvironments(this IConfiguration configuration)
        {
            configuration["ConnectionStrings:IdentityStore"] = configuration.GetDbConfiguration();
            configuration["ConnectionStrings:Google:Client_Id"] = configuration.GetDbConfiguration();
            configuration["ConnectionStrings:Google:Client_Secret"] = configuration.GetDbConfiguration();
            return configuration;
        }
    }
}