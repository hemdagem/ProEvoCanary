using System.Collections.Generic;
using System.Runtime.Caching;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Helpers;
using ProEvoCanary.Models;
using ProEvoCanaryTests;

namespace ProEvoCanary.Tests
{
    class RssFeedRepositoryTests
    {
        [Test]
        public void ShouldGetCachedNews()
        {
            //given
             Mock<ILoader>  _loader = new Mock<ILoader>();
             MemoryCache _defaultCache = new MemoryCache("testCache");
            _loader.Setup(x => x.Load(It.IsAny<string>())).Returns(new List<RssFeedModel>{new RssFeedModel
                {
                    LinkTitle = "hemang",
                    LinkDescription = "ha"
                }});

            //when
            RssFeedRepository repository = new RssFeedRepository();
            repository.GetFeed(It.IsAny<string>(), _defaultCache, _loader.Object);
            repository.GetFeed(It.IsAny<string>(), _defaultCache, _loader.Object);

            //then
            _loader.Verify(x => x.Load(It.IsAny<string>()), Times.Once);


        }
    }
}
