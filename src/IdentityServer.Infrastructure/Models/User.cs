using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Infrastructure.Models
{
    public class User : IdentityUser<long> 
    {
        public string ImageUrl { get; set; }
        
        public long CreatedBy { get; set; }

        //Aggregated
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Active { get; set; }
        
        [NotMapped]
        public IEnumerable<string> Roles { get; set; }

    }
}