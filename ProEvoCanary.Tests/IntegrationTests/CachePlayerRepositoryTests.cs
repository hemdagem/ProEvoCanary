using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Mvc;
using NUnit.Framework;
using ProEvoCanary.Helpers;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories;

namespace ProEvoCanary.Tests.IntegrationTests
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
            Setup();
            var playersExpected = new List<PlayerModel>
            {
                new PlayerModel
                {
                    GoalsPerGame = 3.2f
                }
            };

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
            var selectListModel = repository.GetAllPlayers();

            //then
            Assert.IsNull(selectListModel);
            End();
        }


        [Test]
        public void ShouldGetCachedPlayerList()
        {

           Setup();

            var playerListModel = new SelectListModel { ListItems = new List<SelectListItem>() };
            var players = new List<ListItem>
            {
                new ListItem
                {
                    Text = "Hemang",
                    Value = "1"
                }
            };

            playerListModel.ListItems = new SelectList(players, "Value", "Text");
            _cache.Set("PlayerCacheList", playerListModel, _cacheItemPolicy);

            var repository = new CachePlayerRepository(new CachingManager(_cache));

            //when
            var selectListModel = repository.GetAllPlayers();

            //then
            Assert.IsNotNull(selectListModel);
            Assert.That(selectListModel.ListItems.Count(), Is.EqualTo(1));
            Assert.That(selectListModel.ListItems.First().Text, Is.EqualTo(players.First().Text));
            Assert.That(selectListModel.ListItems.First().Value, Is.EqualTo(players.First().Value));
            End();
        }
    }
}
