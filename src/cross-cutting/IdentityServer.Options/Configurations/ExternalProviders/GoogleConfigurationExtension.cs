using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace IdentityServer.Options.Configurations.ExternalProviders
{
    public static class GoogleConfigurationExtension
    {
        public static string GetGoogleClientId(this IConfiguration configuration)
        {
            try
            {
                string connectionString = new StringBuilder()
                    .Append($"{configuration["GOOGLE_CLIENT_ID"]}")
                    .ToString();
                Console.Out.WriteLine(connectionString);
                return connectionString;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}