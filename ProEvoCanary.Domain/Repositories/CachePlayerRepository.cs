﻿using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Models;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Domain.Repositories
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
            return _cacheManager.Get<List<PlayerModel>>(TopPlayerListCacheKey) as List<PlayerModel>;
        }

        public List<PlayerModel> GetTopPlayersRange(int pageNumber, int playersPerPage)
        {
            throw new NotImplementedException();
        }

        public List<PlayerModel> GetAllPlayers()
        {
            return _cacheManager.Get<List<PlayerModel>>(PlayerListCacheKey) as List<PlayerModel>;
        }

        public void AddToCache(string key, object value, int cacheHours)
        {
            _policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(cacheHours);
            _cacheManager.Add(key, value, cacheHours);
        }

    }
}