using System.Collections.Generic;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Models;

namespace ProEvoCanary.Domain.Helpers
{
    public class RssCacheLoader : ICacheRssLoader
    {
        private readonly ICacheManager _cacheManager;

        public RssCacheLoader(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public List<RssFeedModel> Load(string url)
        {
            return _cacheManager.Get<List<RssFeedModel>>(url);
        }

        public void AddToCache(string key, object value, int cacheHours)
        {
            _cacheManager.Add(key, value, cacheHours);
        }
    }
}