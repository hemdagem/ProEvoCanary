using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using ProEvoCanary.Domain;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Repositories
{
    public class CachePlayerRepository : ICachePlayerRepository
    {
        private readonly ICacheManager _cacheManager;
        private const string TopPlayerListCacheKey = "TopPlayerCacheList";
        private const string PlayerListCacheKey = "PlayerCacheList";
        private readonly CacheItemPolicy _policy = new CacheItemPolicy();

        public CachePlayerRepository(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public List<PlayerModel> GetTopPlayers()
        {
            return _cacheManager.Get(TopPlayerListCacheKey) as List<PlayerModel>;
        }

        public List<PlayerModel> GetTopPlayersRange(int pageNumber, int playersPerPage)
        {
            throw new NotImplementedException();
        }

        public List<PlayerModel> GetAllPlayers()
        {
            return _cacheManager.Get(PlayerListCacheKey) as List<PlayerModel>;
        }

        public void AddToCache(string key, object value, int cacheHours)
        {
            _policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(cacheHours);
            _cacheManager.Add(key, value, cacheHours);
        }

    }
}