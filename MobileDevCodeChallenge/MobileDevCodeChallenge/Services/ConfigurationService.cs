using MobileDevCodeChallenge.Models;
using MobileDevCodeChallenge.Services.Interfaces;

namespace MobileDevCodeChallenge.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public Configuration GetConfiguration()
        {
            throw new System.NotImplementedException();
        }

        public string GetApiKey()
        {
            return "1f54bd990f1cdfb230adb312546d765d";
        }
    }
}