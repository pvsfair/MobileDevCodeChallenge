using System;
using MobileDevCodeChallenge.Utility.Interfaces;

namespace MobileDevCodeChallenge.Droid.Utility
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