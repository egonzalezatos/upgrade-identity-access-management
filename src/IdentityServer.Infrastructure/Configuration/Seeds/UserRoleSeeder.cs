using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Infrastructure.Configuration.Seeds
{
    public static class UserRoleSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<IdentityUserManager>();
            var roleManager = scope.ServiceProvider.GetRequiredService<IdentityRoleManager>();
            SeedRoles(roleManager).Wait();
            SeedUsers(userManager).Wait();
            SeedUserRoles(userManager).Wait();
        }
        
        private static async Task SeedRoles(IdentityRoleManager roleManager)
        {
            foreach (var role in UserRolesData.Roles)
            {
                var dbRole = await roleManager.FindByNameAsync(role.Name);
                if (dbRole == null)
                    await roleManager.CreateAsync(role);
                else
                    await roleManager.UpdateAsync(dbRole);
            }
        }
        
        private static async Task SeedUsers(IdentityUserManager userManager)
        {
            foreach (var user in UserRolesData.Users) 
            {
                var dbUser = await userManager.FindByNameAsync(user.UserName);
                var result = dbUser == null 
                    ? await userManager.CreateAsync(user, "Admin123!")
                    : await userManager.UpdateAsync(dbUser);
                if (result != IdentityResult.Success)
                    throw new Exception(result.Errors.First().Description);
            }
        }
        
        private static async Task SeedUserRoles(UserManager<User> userManager)
        {
            foreach (var user in UserRolesData.Users)
            {
                var userEntity = await userManager.FindByNameAsync(user.UserName);
                await userManager.AddToRolesAsync(userEntity, user.Roles);
            }
        }
    }
}