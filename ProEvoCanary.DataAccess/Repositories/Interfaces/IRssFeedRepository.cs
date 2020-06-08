using System.Collections.Generic;
using ProEvoCanary.DataAccess.Models;

namespace ProEvoCanary.DataAccess.Repositories.Interfaces
{
    public interface IRssFeedRepository
    {
        List<RssFeedModel> GetFeed(string url);
    }
}