using System;
using MobileDevCodeChallenge.Services.Interfaces;
using MobileDevCodeChallenge.Utility.Interfaces;

namespace MobileDevCodeChallenge.Utility
{
    public class CacheService : ICacheService
    {

        public void SaveToCache<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public void SaveToCache<T>(string key, T value, DateTimeOffset expireTimeOffset)
        {
            throw new NotImplementedException();
        }

        public T RetrieveFromCache<T>(string key)
        {
            throw new NotImplementedException();
        }
    }
}