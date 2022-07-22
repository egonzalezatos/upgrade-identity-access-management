using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace IdentityServer.Options.Configurations.Infrastructure
{
    public static class DbConfigurationExtension 
    {
        //"mongodb://admin:admin@localhost:27017"
        internal static string GetDbConfiguration(this IConfiguration configuration)
        {
            try
            {
                string connectionString = new StringBuilder()
                    .Append($"Server={configuration["DB_SERVER"]},{configuration["DB_PORT"]};")
                    .Append($"User Id={configuration["DB_USERNAME"]};")
                    .Append($"Password={configuration["DB_PASSWORD"]};")
                    .Append($"Database={configuration["DB_DATABASE"]};")
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