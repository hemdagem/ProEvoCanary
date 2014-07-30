using System.Collections.Generic;
using System.Runtime.Caching;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Helpers;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories;

namespace ProEvoCanary.Tests
{
    class RssFeedRepositoryTests
    {
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
            var repository = new RssFeedRepository();
            repository.GetFeed(It.IsAny<string>(), defaultCache, loader.Object);
            repository.GetFeed(It.IsAny<string>(), defaultCache, loader.Object);

            //then
            loader.Verify(x => x.Load(It.IsAny<string>()), Times.Once);


        }
    }
}
