using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Security.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services)
        {
            services.AddCors();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddOAuth("oauth", cfg =>
                {
                    cfg.ClientId = "client_id";
                    cfg.ClientSecret = "this_ is a long secret for this api ok?";
                    cfg.CallbackPath = "/oauth/callback";
                    cfg.AuthorizationEndpoint = "http://localhost:8082/login";
                    cfg.TokenEndpoint = "http://localhost:8082/token";
                });

                /*.AddJwtBearer(options =>
                {
                    // base-address of your identityserver
                    options.Authority = "http://localhost:5000";

                    // if you are using API resources, you can specify the name here
                    options.Audience = "identityServer";

                    // IdentityServer emits a typ header by default, recommended extra check
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                });*/
            return services;
        }

    }
}