using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Mvc;
using NUnit.Framework;
using ProEvoCanary.Helpers;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories;

namespace ProEvoCanary.IntegrationTests
{
    public class CachePlayerRepositoryTests
    {
        private MemoryCache cache;
        private CacheItemPolicy cacheItemPolicy;

        private void Setup()
        {
            cache = MemoryCache.Default;
            cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddHours(3)
            };
        }

        private void End()
        {
            cache.Remove("TopPlayerCacheList");
            cache.Remove("PlayerCacheList");
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

            cache.Set("TopPlayerCacheList", playersExpected, cacheItemPolicy);

            var repository = new CachePlayerRepository(new CachingManager(cache));

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
            cache.Set("PlayerCacheList", playerListModel, cacheItemPolicy);

            var repository = new CachePlayerRepository(new CachingManager(cache));

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
