using System.Collections.Generic;
using ProEvoCanary.DataAccess.Models;

namespace ProEvoCanary.DataAccess.Repositories
{
    public interface IRssLoader
    {
        List<RssFeedModel> Load(string url);
    }
}