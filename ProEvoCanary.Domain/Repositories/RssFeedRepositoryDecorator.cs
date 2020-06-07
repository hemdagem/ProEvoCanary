using System.Collections.Generic;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Models;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Domain.Repositories
{
    public class RssFeedRepositoryDecorator : IRssFeedRepository
    {
        private readonly ICacheManager _cacheRssLoader;
        private readonly IRssLoader _rssLoader;

        public RssFeedRepositoryDecorator(ICacheManager cacheManager, IRssLoader rssLoader)
        {
            _cacheRssLoader = cacheManager;
            _rssLoader = rssLoader;
        }

        public List<RssFeedModel> GetFeed(string url)
        {
	        return _cacheRssLoader.AddOrGetExisting(url, () => _rssLoader.Load(url));
        }
    }
}