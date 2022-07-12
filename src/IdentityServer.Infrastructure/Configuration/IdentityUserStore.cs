using IdentityServer.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentityServer.Infrastructure.Configuration
{
    public class IdentityUserStore : UserStore<User, Role, IdentitiesDbContext, long>
    {
        public IdentityUserStore(IdentitiesDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}