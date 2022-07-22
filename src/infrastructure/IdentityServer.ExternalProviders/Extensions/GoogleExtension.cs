using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.ExternalProviders.Extensions
{
    internal static class GoogleExtension
    {
        public static AuthenticationBuilder AddGoogleAuth(this AuthenticationBuilder authService, IConfiguration configuration)
        {
            authService.AddGoogle(o =>
            {
                //"967072489230123-auqi4cs2srgsrn262c58bahmf87330f1.apps.googleusercontent.com"; //123
                o.ClientId = configuration.GetConnectionString("Google:Client_Id");
                //"YGOCSPX-FyImlU8HnI7Osxe_dm6M1GfD9cT1Y"; //YY
                o.ClientSecret = configuration.GetConnectionString("Google:Client_Secret");
            });
                
            return authService;
        }   
    }
}
