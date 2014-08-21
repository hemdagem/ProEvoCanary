using System.Collections.Generic;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Repositories
{
    public class CacheEventRepository : ICacheEventRepository
    {
        private readonly ICache _cache;
        private const string EventsListCacheKey = "EventsListCache";

        public CacheEventRepository() : this(new CachingManager()) { }

        public CacheEventRepository(ICache cache)
        {
            _cache = cache;
        }

        public List<EventModel> GetEvents()
        {
            return _cache.Get(EventsListCacheKey) as List<EventModel>;
        }

        public void AddToCache(string key, object value, int cacheHours)
        {
            _cache.Add(key, value, cacheHours);
        }
    }
}