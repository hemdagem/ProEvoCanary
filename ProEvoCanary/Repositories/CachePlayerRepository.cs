using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Web.Caching;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Repositories
{
    public class CachePlayerRepository : ICachePlayerRepository
    {
        private readonly ICache _cache;
        private const string TopPlayerListCacheKey = "TopPlayerCacheList";
        private const string PlayerListCacheKey = "PlayerCacheList";
        private readonly CacheItemPolicy _policy = new CacheItemPolicy();

        public CachePlayerRepository() : this(new CachingManager()) { }

        public CachePlayerRepository(ICache cache)
        {
            _cache = cache;
        }

        public List<PlayerModel> GetTopPlayers()
        {
            return _cache.Get(TopPlayerListCacheKey) as List<PlayerModel>;
        }

        public SelectListModel GetAllPlayers()
        {
            return _cache.Get(PlayerListCacheKey) as SelectListModel;
        }

        public void AddToCache(string key, object value, int cacheHours)
        {
            _policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(cacheHours);
            _cache.Add(key, value, cacheHours);
        }

    }
}