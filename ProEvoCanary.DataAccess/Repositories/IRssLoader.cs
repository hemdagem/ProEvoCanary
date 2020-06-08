using System.Collections.Generic;
using ProEvoCanary.DataAccess.Models;

namespace ProEvoCanary.Domain.Helpers
{
    public interface IRssLoader
    {
        List<RssFeedModel> Load(string url);
    }
}