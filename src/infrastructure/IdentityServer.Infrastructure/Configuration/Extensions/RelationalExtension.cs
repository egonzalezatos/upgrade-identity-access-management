using IdentityServer.Infrastructure.Models;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Infrastructure.Configuration.Extensions
{
    internal static class RelationalExtension
    {
        internal static IServiceCollection AddRelational(this IServiceCollection services, string connectionString)
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

    }
}