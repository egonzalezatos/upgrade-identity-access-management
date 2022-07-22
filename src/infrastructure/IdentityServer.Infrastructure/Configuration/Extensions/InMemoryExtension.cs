using IdentityServer.Infrastructure.Configuration.Seeds;
using IdentityServer.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Infrastructure.Configuration.Extensions
{
    internal static class InMemoryExtension
    {
        internal static IServiceCollection AddInMemory(this IServiceCollection services)
        {
            services.AddDbContext<IdentitiesDbContext>(options =>
                options.UseInMemoryDatabase("test"));
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<IdentitiesDbContext>();
            
            services.AddIdentityServer(options =>
                {
                    options.UserInteraction.LoginUrl = "/Auth/Login";
                    options.UserInteraction.LogoutUrl = "/Auth/Logout";
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.Events.RaiseFailureEvents = true;
                })
                .AddAspNetIdentity<User>()
                .AddInMemoryApiResources(GrantConfigurationData.ApiResources)
                .AddInMemoryClients(GrantConfigurationData.Clients)
                .AddInMemoryApiScopes(GrantConfigurationData.ApiScopes)
                .AddInMemoryIdentityResources(GrantConfigurationData.IdentityResources)
                .AddDeveloperSigningCredential();
            return services;
        }   
    }
}