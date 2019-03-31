using System.Threading.Tasks;
using MobileDevCodeChallenge.Models;

namespace MobileDevCodeChallenge.Services.Interfaces
{
    public interface IConfigurationService
    {
        Task<Configuration> GetConfiguration();

        string GetApiKey();
        string GetBaseUrlTmdb();
    }
}