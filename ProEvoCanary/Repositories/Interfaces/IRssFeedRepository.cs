using System.Collections.Generic;
using ProEvoCanary.Models;

namespace ProEvoCanary.Repositories.Interfaces
{
    public interface IRssFeedRepository
    {
        List<RssFeedModel> GetFeed(string url);
    }
}