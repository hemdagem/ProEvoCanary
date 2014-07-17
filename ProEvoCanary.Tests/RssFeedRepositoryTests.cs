using System.Collections.Generic;
using System.Runtime.Caching;
using Moq;
using NUnit.Framework;
using ProEvo45.Helpers;
using ProEvo45.Models;
using ProEvo45Tests;

namespace ProEvoTests
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
