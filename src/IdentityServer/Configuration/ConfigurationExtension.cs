using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace IdentityServer.Configuration
{
    public static class ConfigurationExtension
    {
        //"mongodb://admin:admin@localhost:27017"
        public static string GetDbConnection(this IConfiguration configuration)
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