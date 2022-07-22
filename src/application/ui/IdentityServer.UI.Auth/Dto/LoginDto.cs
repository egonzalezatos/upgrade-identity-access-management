using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;

namespace IdentityServer.UI.Auth.Dto
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        
        public List<AuthenticationScheme> ExternalProviders { get; set; }
    }
}