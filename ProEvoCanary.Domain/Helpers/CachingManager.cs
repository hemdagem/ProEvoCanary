using System;
using System.Runtime.Caching;
using ProEvoCanary.Domain.Helpers.Interfaces;

namespace ProEvoCanary.Domain.Helpers
{
    public class CachingManager : ICacheManager
    {
        private readonly MemoryCache _memoryCache;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy();
        public CachingManager(MemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public CachingManager() : this(MemoryCache.Default) { }

        public T AddOrGetExisting<T>(string key, Func<T> getItemFromSource)
        {
            var cacheItem = (T)_memoryCache.Get(key);

            if (cacheItem == null)
            {
	            cacheItem = getItemFromSource();
	            _policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(30);
	            _memoryCache.Add(key, cacheItem, _policy);
            }

            return cacheItem;
        }
    }
}