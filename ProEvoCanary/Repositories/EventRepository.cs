using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using ProEvoCanary.Helpers;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Repositories.Interfaces;
using ProEvoCanary.Models;

namespace ProEvoCanary.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IDBHelper _helper;
        private readonly MemoryCache _memoryCache;
        private const string EventsListCacheKey = "EventsListCache";
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddHours(3)
        };

        public List<EventModel> GetEvents()
        {
            if (_memoryCache.Contains(EventsListCacheKey))
            {
                return _memoryCache.Get(EventsListCacheKey) as List<EventModel>;
            }

            var reader = _helper.ExecuteReader("[sp_GetTDetails]");
            var lstTournament = new List<EventModel>();
            while (reader.Read())
            {
                lstTournament.Add(new EventModel
                {
                    EventID = (int)reader["TournamentID"],
                    EventName = reader["TournamentName"].ToString(),
                    Venue = reader["Venue"].ToString(),
                    Date = reader["Date"].ToString(),
                    Name = reader["Name"].ToString(),
                    Completed = (bool)reader["Completed"]
                });

            }

            _memoryCache.Add(EventsListCacheKey, lstTournament, _policy);
            return lstTournament;
        }


        public EventRepository(IDBHelper helper, MemoryCache memoryCache)
        {
            _helper = helper;
            _memoryCache = memoryCache;
        }

        public EventRepository()
            : this(new DBHelper(), MemoryCache.Default)
        {

        }
    }
}