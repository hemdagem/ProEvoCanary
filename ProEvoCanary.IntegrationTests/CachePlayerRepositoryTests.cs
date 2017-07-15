using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using NUnit.Framework;
using ProEvoCanary.Domain.Helpers;
using ProEvoCanary.Domain.Repositories;
using ProEvoCanary.Domain.Models;

namespace ProEvoCanary.IntegrationTests
{
    public class CachePlayerRepositoryTests
    {
        private MemoryCache _cache;
        private CacheItemPolicy _cacheItemPolicy;

        private void Setup()
        {
            _cache = MemoryCache.Default;
            _cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddHours(3)
            };
        }

        private void End()
        {
            _cache.Remove("TopPlayerCacheList");
            _cache.Remove("PlayerCacheList");
        }

        [Test]
        public void ShouldGetCachedTopPlayers()
        {
            //given
            Setup();

            var playersExpected = new List<PlayerModel> { new PlayerModel { GoalsPerGame = 3.2f } };

            _cache.Set("TopPlayerCacheList", playersExpected, _cacheItemPolicy);

            var repository = new CachePlayerRepository(new CachingManager(_cache));

            //when
            var players = repository.GetTopPlayers();

            //then
            Assert.That(players.Count, Is.EqualTo(1));
            Assert.That(players[0].GoalsPerGame, Is.EqualTo(playersExpected[0].GoalsPerGame));

            End();
        }

        [Test]
        public void ShouldNotGetCachedPlayers()
        {
            //given
            Setup();
            var repository = new CachePlayerRepository(new CachingManager(MemoryCache.Default));

            //when
            var allPlayers = repository.GetAllPlayers();

            //then
            Assert.IsNull(allPlayers);
            End();
        }

        [Test]
        public void ShouldGetCachedPlayerList()
        {
            Setup();

            var playerListModel = new List<PlayerModel>
            {
                new PlayerModel
                {
                    PlayerName = "Hemang",
                    PlayerId = 1
                }
            };

            _cache.Set("PlayerCacheList", playerListModel, _cacheItemPolicy);

            var repository = new CachePlayerRepository(new CachingManager(_cache));

            //when
            var allPlayers = repository.GetAllPlayers();

            //then
            Assert.IsNotNull(allPlayers);
            Assert.That(allPlayers.Count(), Is.EqualTo(1));
            Assert.That(allPlayers.First().PlayerName, Is.EqualTo(playerListModel.First().PlayerName));
            Assert.That(allPlayers.First().PlayerId, Is.EqualTo(playerListModel.First().PlayerId));
            End();
        }
    }
}
