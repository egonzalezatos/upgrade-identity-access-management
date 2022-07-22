using System;
using IdentityServer.Infrastructure.Configuration.Seeds;
using IdentityServer.Infrastructure.Models;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Upgrade.Utils.PersistenceConfiguration.Extensions;

namespace IdentityServer.Infrastructure.Configuration.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureApplicationCookie(config =>
            { config.Cookie.Name = "IAM"; });

            services.TryAddScoped<IdentityUserManager>();
            services.TryAddScoped<IdentityRoleManager>();
            
            if (configuration.IsRelational())
                services.AddRelational(configuration.GetConnectionString("IdentityStore"));
            else
                services.AddInMemory();
            
            return services;
        }
        
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            // app.EnsureSeed(serviceProvider);
            app.EnsureInMemorySeed(serviceProvider);
            return app;
        }
    }
}