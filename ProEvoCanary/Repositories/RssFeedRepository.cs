using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using ProEvoCanary.Helpers;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Repositories
{
    public class RssFeedRepository : IRssFeedRepository
    {
        private const string RssCacheKey = "RssCache";
        private readonly TimeSpan _cacheExpiry = TimeSpan.FromHours(3);

        public List<RssFeedModel> GetFeed(string url, MemoryCache memoryCache, ILoader loader)
        {
            List<RssFeedModel> rssFeedModel;
            if (memoryCache.Contains(RssCacheKey))
            {
                rssFeedModel = memoryCache.Get(RssCacheKey) as List<RssFeedModel>;
            }
            else
            {
                rssFeedModel = loader.Load(url);
                var cacheItemExpiry = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.UtcNow.Add(_cacheExpiry) };
                memoryCache.Add(RssCacheKey, rssFeedModel, cacheItemExpiry);
            }

            return rssFeedModel;
        }


    }
}