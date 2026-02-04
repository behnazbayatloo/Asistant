using Asistant_Infra_Cache.Contract;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Asistant_Infra_Cache.DistributedCache
{
    public class DistributedCacheService(IDistributedCache distributedCache) : ICacheService
    {
        public void SetSliding<T>(string key, T value, int expirationTime)
        {
            var cacheOptions = new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(expirationTime),
            };

            distributedCache.SetString(key, JsonSerializer.Serialize(value), cacheOptions);
        }

        public T Get<T>(string key)
        {
            var value = distributedCache.GetString(key);

            if (value != null)
            {
                return JsonSerializer.Deserialize<T>(value);
            }

            return default;
        }

        public void Remove(string key)
        {
            distributedCache.Remove(key);
        }

        public void Set<T>(string key, T data, int expiretionTime)
        {
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(expiretionTime)
            };

            distributedCache.SetString(key, JsonSerializer.Serialize(data), cacheOptions);
        }
    }
}
