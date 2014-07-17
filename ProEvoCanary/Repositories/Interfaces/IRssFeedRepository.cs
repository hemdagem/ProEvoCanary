using System.Collections.Generic;
using System.Runtime.Caching;
using ProEvo45.Helpers;
using ProEvo45.Models;

namespace ProEvo45.Repositories.Interfaces
{
    public interface IRssFeedRepository
    {
        List<RssFeedModel> GetFeed(string url, MemoryCache memoryCache, ILoader loader);
    }
}