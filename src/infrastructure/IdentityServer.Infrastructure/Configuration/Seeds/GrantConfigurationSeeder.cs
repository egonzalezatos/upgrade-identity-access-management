using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Infrastructure.Configuration.Seeds
{
    public static class GrantConfigurationSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
            using var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
            context.Database.Migrate();
            
            Add(context.Clients, GrantConfigurationData.Clients.ToList(), (model) => model.ToEntity());
            Add(context.IdentityResources, GrantConfigurationData.IdentityResources.ToList(), (model) => model.ToEntity());
            Add(context.ApiScopes, GrantConfigurationData.ApiScopes.ToList(), (model) => model.ToEntity());
            Add(context.ApiResources, GrantConfigurationData.ApiResources.ToList(), (model) => model.ToEntity());
            context.SaveChanges();
        }

        private static void Add<T, TU>(DbSet<T> set, IEnumerable<TU> data, Func<TU, T> toEntity) where T : class
        {
            if (set.Any()) return;
            foreach (var resource in data)
                set.Add(toEntity(resource));
        }
    }
    
}