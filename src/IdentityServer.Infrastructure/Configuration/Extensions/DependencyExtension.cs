using System;
using IdentityServer.Infrastructure.Configuration.Seeds;
using IdentityServer.Infrastructure.Models;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IdentityServer.Infrastructure.Configuration.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.TryAddScoped<IdentityUserManager>();
            services.TryAddScoped<IdentityRoleManager>();
            services.AddLogging();
            
            services.AddInMemory();
            
            return services;
        }

        private static IServiceCollection AddInMemory(this IServiceCollection services)
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

        private static IServiceCollection AddRelational(this IServiceCollection services, string connectionString)
        {
            //DbContext
            services.AddDbContext<IdentitiesDbContext>(options => 
                options.UseSqlServer(connectionString, builder => 
                    builder.MigrationsAssembly(typeof(DependencyExtension).Assembly.GetName().Name)));
            
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<IdentitiesDbContext>();

            services.AddIdentityServer()
                .AddAspNetIdentity<User>()
                .AddConfigurationStore<ConfigurationDbContext>(options =>
                    options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, optionsBuilder =>
                        optionsBuilder.MigrationsAssembly(typeof(DependencyExtension).Assembly.GetName().Name)))
                .AddOperationalStore<PersistedGrantDbContext>(options =>
                    options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, optionsBuilder =>
                        optionsBuilder.MigrationsAssembly(typeof(DependencyExtension).Assembly.GetName().Name)))
                .AddDeveloperSigningCredential();
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