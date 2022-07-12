using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServer.Infrastructure.Configuration.Seeds
{
    public static class GrantConfigurationData
    {
        //Resources
        public static IEnumerable<IdentityResource> IdentityResources
            => new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            
                //Custom Resources
                new IdentityResource( 
                    name:"role",
                    userClaims: new []{"role"}),
                new IdentityResource(
                    name: "custom",
                    displayName: "My custom resource",
                    userClaims: new[] {"name", "status"})
            };
        
        //Scopes
        public static IEnumerable<ApiScope> ApiScopes => new[]
        {
            new ApiScope("CoursesAPI.read", "read api"),
            new ApiScope("CoursesAPI.write", "write api"),
            new ApiScope("trainee-admin-service.read", "read on trainee admin"),
            new ApiScope("trainee-tracking-service.read", "read on trainee tracking"),
            new ApiScope("trainee-tracking-service.write", "write on trainee tracking"),
        };

        //Api Resources
        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource("CoursesAPI")
            {
                Scopes = {"CoursesAPI.read", "CoursesAPI.write"},
                ApiSecrets = {new Secret("CoursesAPI_Secret".Sha256())},
                UserClaims = {"role"}
            },
            new ApiResource("trainee-admin-service")
            {
                Scopes = {"trainee-admin-service.read"},
                ApiSecrets = {new Secret("trainee-admin-service".Sha256())},
                UserClaims = {"role"}
            },
            new ApiResource("trainee-tracking-service")
            {
                Scopes = {"trainee-tracking-service.read", "trainee-tracking-service.write"},
                ApiSecrets = {new Secret("trainee-tracking-service".Sha256())},
                UserClaims = {"role"}
            },
        };

        //Clients
        public static IEnumerable<Client> Clients => new[]
        {
            new Client
            {
                ClientId = "trainee_tracking_service",
                ClientName = "Upgrade Trainee Tracking Microservice",
                ClientSecrets = {new Secret("ClientSecret1".Sha256())},
                
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"trainee-admin-service.read"}
            },
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = {new Secret("ClientSecret1".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = {"https://localhost:5000/signing-oidc"},
                FrontChannelLogoutUri = "https://localhost:5000/signing-oidc",
                PostLogoutRedirectUris = {"https://localhost:5000/signing-callback-oidc"},
                AllowOfflineAccess = true,
                AllowedScopes = {"openid", "profile", "CoursesAPI.read"},
                RequirePkce = true,
                RequireConsent = true,
                AllowPlainTextPkce = false
            },
            new Client
            {
                ClientId = "front_client",
                ClientSecrets = {new Secret("FrontClient1".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = {"http://localhost/callback"},
                PostLogoutRedirectUris = {"http://localhost/test"},
                AllowedCorsOrigins = {"http://localhost"},
                RequirePkce = true,
                RequireClientSecret = false,
                AllowedScopes = {"openid", "profile", "trainee-tracking-service.read", "trainee-tracking-service.write"},
                RequireConsent = false,
            }
        };

    }
}