using System;
using System.Collections.Generic;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Models;
using ProEvoCanary.Domain.Repositories.Interfaces;
using EventModel = ProEvoCanary.Domain.Models.EventModel;

namespace ProEvoCanary.Domain.Repositories
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

        public List<Standings> GetStandings(int id)
        {
            throw new NotImplementedException();
        }

        public EventModel GetEventForEdit(int id, int ownerId)
        {
            throw new System.NotImplementedException();
        }

        public int CreateEvent(string tournamentname, DateTime utcNow, int eventType, int ownerId)
        {
            throw new NotImplementedException();
        }

        public void GenerateFixtures(int eventId, List<int> userIds)
        {
            throw new NotImplementedException();
        }

        public int AddTournamentUsers(int eventId, List<int> userIds)
        {
            throw new NotImplementedException();
        }

        public void AddToCache(string key, object value, int cacheHours)
        {
            _cacheManager.Add(key, value, cacheHours);
        }
    }
}