using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using ProEvoCanary.Helpers;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Repositories
{
    public class RssFeedRepository : IRssFeedRepository
    {
        private readonly MemoryCache _memoryCache;
        private readonly ILoader _loader;
        private const string RssCacheKey = "RssCache";
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddHours(3)
        };

        public RssFeedRepository() : this(MemoryCache.Default, new Loader())
        {
            
        }

        public RssFeedRepository(MemoryCache memoryCache, ILoader loader)
        {
            _memoryCache = memoryCache;
            _loader = loader;
        }

        public List<RssFeedModel> GetFeed(string url)
        {
            List<RssFeedModel> rssFeedModel;
            if (_memoryCache.Contains(RssCacheKey))
            {
                rssFeedModel = _memoryCache.Get(RssCacheKey) as List<RssFeedModel>;
            }
            else
            {
                rssFeedModel = _loader.Load(url);
                _memoryCache.Add(RssCacheKey, rssFeedModel, _policy);
            }

            return rssFeedModel;
        }


    }
}