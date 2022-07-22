using System.Collections.Generic;
using IdentityServer.Infrastructure.Models;

namespace IdentityServer.Infrastructure.Configuration.Seeds
{
    public static class UserRolesData
    {
        
        public static IEnumerable<Role> Roles => new[]
        {
            new Role { Name = "ROLE_ADMIN"},
            new Role { Name = "ROLE_LEAD"},
            new Role { Name = "ROLE_TRAINEE"}
        };
        
        public static IEnumerable<User> Users => new[]
        {
            new User
            {
                UserName = "admin",
                FirstName = "admin",
                LastName = "Administrator",
                Email = "admin@localhost",
                Active = true,
                Roles = new[]{"ROLE_ADMIN", "ROLE_TRAINEE"}
            }
        };


    }
}