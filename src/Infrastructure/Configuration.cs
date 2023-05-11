using Microsoft.Extensions.Configuration;

namespace TechnestHackhaton.Persistence
{
    static class Configuration
    {
        static public string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                try
                {
                    var directory = Path.Combine(Directory.GetCurrentDirectory(), "TechnestHackhaton\\src\\WebAPI");
                    configurationManager.SetBasePath(Path.Combine(directory));
                    configurationManager.AddJsonFile("appsettings.json");
                }
                catch
                {
                    configurationManager.AddJsonFile("appsettings.Production.json");
                }
                return configurationManager.GetConnectionString("MSSql");
            }
        }
    }
}
