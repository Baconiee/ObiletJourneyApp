using Microsoft.Extensions.Caching.Distributed;
using ObiletJourneyApp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ObiletJourneyApp.Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetAsync<T>(string cacheKey)
        {
            var cached = await _cache.GetStringAsync(cacheKey);
            return string.IsNullOrEmpty(cached) ? default : JsonSerializer.Deserialize<T>(cached);
        }

        public async Task SetAsync<T>(string cacheKey, T data, TimeSpan expiration)
        {
            var serialized = JsonSerializer.Serialize(data);
            await _cache.SetStringAsync(cacheKey, serialized, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            });
        }
    }
}
