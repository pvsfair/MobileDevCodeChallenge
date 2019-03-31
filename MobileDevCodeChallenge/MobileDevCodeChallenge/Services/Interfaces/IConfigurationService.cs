using MobileDevCodeChallenge.Models;

namespace MobileDevCodeChallenge.Services.Interfaces
{
    public interface IConfigurationService
    {
        Configuration GetConfiguration();

        string GetApiKey();

    }
}