using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using NUnit.Framework;
using ProEvoCanary.Helpers;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories;

namespace ProEvoCanary.IntegrationTests
{
    public class CacheEventRepositoryTests
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


        [Test]
        public void ShouldGetCachedListOfEvents()
        {
            //given
            Setup();

            var expectedEvents = new List<EventModel>()
            {
                new EventModel
                {
                    Completed = true,
                    Date = "10/10/2010",
                    EventId = 1,
                    EventName = "Event",
                    Name = "Arsenal",

                }
            };

            _cache.Set("EventsListCache", expectedEvents, _cacheItemPolicy);

            var repository = new CacheEventRepository(new CachingManager(_cache));

            //when
            var eventModels = repository.GetEvents();

            //then
            Assert.That(eventModels.Count,Is.EqualTo(1));
            Assert.That(eventModels[0].Completed, Is.EqualTo(expectedEvents[0].Completed));
            Assert.That(eventModels[0].Date, Is.EqualTo(expectedEvents[0].Date));
            Assert.That(eventModels[0].EventId, Is.EqualTo(expectedEvents[0].EventId));
            Assert.That(eventModels[0].EventName, Is.EqualTo(expectedEvents[0].EventName));
            Assert.That(eventModels[0].Name, Is.EqualTo(expectedEvents[0].Name));
           
            End();
        }


        private void End()
        {
            _cache.Remove("EventsListCache");
        }

        [Test]
        public void ShouldNotGetCachedEvents()
        {
            //given
            Setup();
            var repository = new CacheEventRepository(new CachingManager(MemoryCache.Default));

            //when
            var selectListModel = repository.GetEvents();

            //then
            Assert.IsNull(selectListModel);
            End();
        }


    }
}
