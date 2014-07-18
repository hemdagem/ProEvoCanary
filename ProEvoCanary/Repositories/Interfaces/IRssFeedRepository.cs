using System.Collections.Generic;
using System.Runtime.Caching;
using ProEvoCanary.Helpers;
using ProEvoCanary.Models;

namespace ProEvoCanary.Repositories.Interfaces
{
    public interface IRssFeedRepository
    {
        List<RssFeedModel> GetFeed(string url, MemoryCache memoryCache, ILoader loader);
    }
}