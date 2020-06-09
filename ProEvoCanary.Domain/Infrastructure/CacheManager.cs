using System;
using System.Runtime.Caching;

namespace ProEvoCanary.Domain.Helpers
{
    public class CacheManager : ICacheManager
    {
        private readonly MemoryCache _memoryCache;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy();
        public CacheManager(MemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public CacheManager() : this(MemoryCache.Default) { }

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