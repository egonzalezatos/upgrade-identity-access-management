using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.ExternalProviders.Extensions
{
    public static class GoogleExtension
    {
        public static AuthenticationBuilder AddGoogleAuth(this AuthenticationBuilder authService)
        {
            authService.AddGoogle(o =>
            {
                o.ClientId = "967072489230-auqi4cs2srgsrn262c58bahmf87330f1.apps.googleusercontent.com";
                o.ClientSecret = "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";
            });
                
            return authService;
        }   
    }
}