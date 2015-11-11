using System.Collections.Generic;
using ProEvoCanary.Domain;

namespace ProEvoCanary.Repositories.Interfaces
{
    public interface IRssFeedRepository
    {
        List<RssFeedModel> GetFeed(string url);
    }
}