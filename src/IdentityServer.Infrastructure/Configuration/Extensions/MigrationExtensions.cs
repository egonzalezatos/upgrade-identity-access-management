using System;
using IdentityServer.Infrastructure.Models;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Infrastructure.Configuration.Extensions
{
    public static class MigrationExtensions
    {
        public static IApplicationBuilder RunMigrations(this IApplicationBuilder app) 
        {
            app.MigrateContext<IdentitiesDbContext>();
            //app.MigrateContext<PersistedGrantDbContext>();
            //app.MigrateContext<ConfigurationDbContext>();
            return app;
        }
        public static void MigrateContext<T>(this IApplicationBuilder app) where T : DbContext
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                T context = scope.ServiceProvider.GetService<T>()!;
                Console.WriteLine($"Running migrations of {typeof(T).ShortDisplayName()}...");
                if (context.Database.IsRelational())
                    context.Database.Migrate();
                else
                    context.Database.EnsureCreated();
            }
        }
    }
}