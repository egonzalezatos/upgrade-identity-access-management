using IdentityServer.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentityServer.Infrastructure.Configuration
{
    public class IdentityRoleStore : RoleStore<Role, IdentitiesDbContext, long>
    {
        public IdentityRoleStore(IdentitiesDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}