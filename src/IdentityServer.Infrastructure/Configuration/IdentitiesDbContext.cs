using IdentityServer.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Infrastructure.Configuration
{
    public class IdentitiesDbContext : IdentityDbContext<User, Role, long>
    {
        public IdentitiesDbContext(DbContextOptions<IdentitiesDbContext> options)
            : base(options)
        {
        }
    }
}