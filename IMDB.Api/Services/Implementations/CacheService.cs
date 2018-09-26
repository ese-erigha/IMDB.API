using System;
using IMDB.Api.Helpers;
using IMDB.Api.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IMDB.Api.Services.Implementations
{
    public class CacheService : ICacheService 
    {
        readonly IMemoryCache _cache;
        readonly AppSettings _appSettings;

        public CacheService(IMemoryCache cache, IOptions<AppSettings> appSettings)
        {
            _cache = cache;
            _appSettings = appSettings.Value;
        }

        public string Get(string key)
        {
            return _cache.Get<string>(key);
        }

        public void Set<T>(string key, T value) where T : class
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(Double.Parse(_appSettings.CacheExpirationInMinutes))
            };

            string data = JsonConvert.SerializeObject(value);
            _cache.Set(key, data,cacheEntryOptions);
        }
    }
}
