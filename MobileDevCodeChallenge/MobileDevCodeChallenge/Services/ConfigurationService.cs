using System.Threading.Tasks;
using MobileDevCodeChallenge.Http;
using MobileDevCodeChallenge.Models;
using MobileDevCodeChallenge.Services.Interfaces;
using MobileDevCodeChallenge.Utility;
using MobileDevCodeChallenge.Utility.InjectionManager;

namespace MobileDevCodeChallenge.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly string urlConfiguration = "/configuration";
        private Configuration _cachedConfiguration = null;

        public async Task<Configuration> GetConfiguration()
        {
            if (_cachedConfiguration != null)
                return _cachedConfiguration;
            
            _cachedConfiguration = await InjectionManager.ResolveInstance<IHttpCall>()
                                                         .baseUrl(TMDbBaseConfiguration.GetBaseUrlTmdb())
                                                         .asGet(urlConfiguration)
                                                         .addApiKey()
                                                         .requestAsync<Configuration>();
            
            return _cachedConfiguration;
        }
    }
}