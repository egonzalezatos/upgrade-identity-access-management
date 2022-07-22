using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.ExternalProviders.Extensions
{
    public static class DependencyExtension
    {
        public static AuthenticationBuilder AddExternalProviders(this AuthenticationBuilder auth, IConfiguration configuration)
        {
            auth.AddGoogleAuth(configuration);
            return auth;
        } 
    }
}