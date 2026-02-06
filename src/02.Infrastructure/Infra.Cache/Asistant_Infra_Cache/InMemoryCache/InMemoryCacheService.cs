
using Asistant_Domain_Core.InfraContracts;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Infra_Cache.InMemoryCache
{
    public class InMemoryCacheService(IMemoryCache cache) : ICacheService
    {
        public void SetSliding<T>(string key, T data, int expiretionTime)
        {
            var options = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(30)
            };

            cache.Set(key, data, options);
        }

        public T Get<T>(string key)
        {
            return cache.Get<T>(key)!;
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }

        public void Set<T>(string key, T data, int expiretionTime)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(expiretionTime)
            };

            cache.Set(key, data, options);
        }
    }
}
