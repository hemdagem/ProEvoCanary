using System.Collections.Generic;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Repositories.Interfaces;
using EventModel = ProEvoCanary.Models.EventModel;

namespace ProEvoCanary.Repositories
{
    public class CacheEventRepository : ICacheEventRepository
    {
        private readonly ICacheManager _cacheManager;
        private const string EventsListCacheKey = "EventsListCache";

        public CacheEventRepository(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public List<EventModel> GetEvents()
        {
            return _cacheManager.Get(EventsListCacheKey) as List<EventModel>;
        }

        public EventModel GetEvent(int id)
        {
            throw new System.NotImplementedException();
        }

        public EventModel GetEventForEdit(int id, int ownerId)
        {
            throw new System.NotImplementedException();
        }

        public void AddToCache(string key, object value, int cacheHours)
        {
            _cacheManager.Add(key, value, cacheHours);
        }
    }
}