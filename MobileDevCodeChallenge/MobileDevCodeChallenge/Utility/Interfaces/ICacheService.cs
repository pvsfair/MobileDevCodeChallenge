using System;

namespace MobileDevCodeChallenge.Utility.Interfaces
{
    public interface ICacheService
    {
        void SaveToCache<T>(string key, T value);
        void SaveToCache<T>(string key, T value, DateTimeOffset expireTimeOffset);
        T RetrieveFromCache<T>(string key);
    }
}