using System;
using IdentityServer.Infrastructure.Configuration.Seeds;
using Microsoft.AspNetCore.Builder;

namespace IdentityServer.Infrastructure.Configuration.Extensions
{
    public static class SeederExtension
    {
        public static IApplicationBuilder EnsureSeed(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            UserRoleSeeder.Seed(serviceProvider);
            GrantConfigurationSeeder.Seed(serviceProvider);
            return app;
        }
        public static IApplicationBuilder EnsureInMemorySeed(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            UserRoleSeeder.Seed(serviceProvider);
            return app;
        }

        
    }
}