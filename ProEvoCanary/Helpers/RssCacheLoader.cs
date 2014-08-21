using System.Collections.Generic;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Helpers
{
    public class RssCacheLoader : ICacheRssLoader
    {
        private readonly ICache _cache;

        public RssCacheLoader(ICache cache)
        {
            _cache = cache;
        }

        public RssCacheLoader() : this(new CachingManager()) { }

        public List<RssFeedModel> Load(string url)
        {
            return _cache.Get(url) as List<RssFeedModel>;
        }

        public void AddToCache(string key, object value, int cacheHours)
        {
            _cache.Add(key, value, cacheHours);
        }
    }
}