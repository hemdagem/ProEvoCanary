using System.Collections.Generic;
using ProEvoCanary.Helpers;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Repositories
{
    public class CacheEventRepository : ICacheEventRepository
    {
        private readonly ICacheManager _cacheManager;
        private const string EventsListCacheKey = "EventsListCache";

        public CacheEventRepository() : this(new CachingManager()) { }

        public CacheEventRepository(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public List<EventModel> GetEvents()
        {
            return _cacheManager.Get(EventsListCacheKey) as List<EventModel>;
        }

        public void AddToCache(string key, object value, int cacheHours)
        {
            _cacheManager.Add(key, value, cacheHours);
        }
    }
}