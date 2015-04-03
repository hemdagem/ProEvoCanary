using System.Collections.Generic;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Repositories
{
    public class RssFeedRepositoryDecorator : IRssFeedRepository
    {
        private readonly ICacheRssLoader _cacheRssLoader;
        private readonly IRssLoader _rssLoader;

        public RssFeedRepositoryDecorator(ICacheRssLoader cacheRssLoader, IRssLoader rssLoader)
        {
            _cacheRssLoader = cacheRssLoader;
            _rssLoader = rssLoader;
        }

        public List<RssFeedModel> GetFeed(string url)
        {
            List<RssFeedModel> rssFeedModel = _cacheRssLoader.Load(url);

            if (rssFeedModel != null) return rssFeedModel;

            rssFeedModel = _rssLoader.Load(url);

            _cacheRssLoader.AddToCache(url, rssFeedModel, 3);

            return rssFeedModel;
        }
    }
}