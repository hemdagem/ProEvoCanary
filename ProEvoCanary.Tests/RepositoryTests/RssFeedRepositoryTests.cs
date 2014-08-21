using System.Collections.Generic;
using System.Runtime.Caching;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Helpers;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories;

namespace ProEvoCanary.Tests
{
    class RssFeedRepositoryTests
    {


        [Test]
        public void ShouldGetNews()
        {
            //given
            var loader = new Mock<ILoader>();
            var defaultCache = new MemoryCache("testCache");
            loader.Setup(x => x.Load(It.IsAny<string>())).Returns(new List<RssFeedModel>{new RssFeedModel
                {
                    LinkTitle = "hemang",
                    LinkDescription = "ha",
                    ImageHeight = "200",
                    ImageUrl = "http://google.com",
                    ImageWidth = "200",
                    LinkUrl = "http://arsenal.com",
                    PublishDate = "10/10/2010"
                }});

            //when
            var repository = new RssFeedRepository(defaultCache, loader.Object);
           var feed = repository.GetFeed(It.IsAny<string>());
           

            //then
            Assert.That(feed[0].LinkTitle,Is.EqualTo("hemang"));
            Assert.That(feed[0].LinkDescription, Is.EqualTo("ha"));
            Assert.That(feed[0].ImageHeight, Is.EqualTo("200"));
            Assert.That(feed[0].ImageUrl, Is.EqualTo("http://google.com"));
            Assert.That(feed[0].ImageWidth, Is.EqualTo("200"));
            Assert.That(feed[0].LinkUrl, Is.EqualTo("http://arsenal.com"));
            Assert.That(feed[0].PublishDate, Is.EqualTo("10/10/2010"));

        }  
        
        [Test]
        public void ShouldGetCachedNews()
        {
            //given
            var loader = new Mock<ILoader>();
            var defaultCache = new MemoryCache("testCache");
            loader.Setup(x => x.Load(It.IsAny<string>())).Returns(new List<RssFeedModel>{new RssFeedModel
                {
                    LinkTitle = "hemang",
                    LinkDescription = "ha"
                }});

            //when
            var repository = new RssFeedRepository(defaultCache, loader.Object);
            repository.GetFeed(It.IsAny<string>());
            repository.GetFeed(It.IsAny<string>());

            //then
            loader.Verify(x => x.Load(It.IsAny<string>()), Times.Once);

        }
    }
}
