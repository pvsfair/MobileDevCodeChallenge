using System.Threading.Tasks;
using MobileDevCodeChallenge.Models;
using MobileDevCodeChallenge.Services.Interfaces;

namespace MobileDevCodeChallenge.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public Task<Configuration> GetConfiguration()
        {
            throw new System.NotImplementedException();
        }

        public string GetApiKey()
        {
            return "1f54bd990f1cdfb230adb312546d765d";
        }

        public string GetBaseUrlTmdb()
        {
            return "https://api.themoviedb.org/3";
        }
    }
}