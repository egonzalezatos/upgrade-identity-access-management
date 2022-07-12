using System;
using System.IO;
using System.Text.Json;

namespace IdentityServer.Shared
{
    public static class JsonFile
    {
        public static T ToJson<T>(string path)
        {
            try
            {
                string file = File.ReadAllText(path);
                return JsonSerializer.Deserialize<T>(file);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
                throw;
            }
        }
    }
}