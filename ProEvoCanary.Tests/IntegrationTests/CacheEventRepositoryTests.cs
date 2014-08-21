using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories;

namespace ProEvoCanary.Tests.IntegrationTests
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
                    EventID = 1,
                    EventName = "Event",
                    Name = "Arsenal",
                    Venue = "Venue"

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
            Assert.That(eventModels[0].EventID, Is.EqualTo(expectedEvents[0].EventID));
            Assert.That(eventModels[0].EventName, Is.EqualTo(expectedEvents[0].EventName));
            Assert.That(eventModels[0].Name, Is.EqualTo(expectedEvents[0].Name));
            Assert.That(eventModels[0].Venue, Is.EqualTo(expectedEvents[0].Venue));
           
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
