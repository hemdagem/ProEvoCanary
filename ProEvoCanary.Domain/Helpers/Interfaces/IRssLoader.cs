using System.Collections.Generic;
using ProEvoCanary.Domain;

namespace ProEvoCanary.Helpers.Interfaces
{
    public interface IRssLoader
    {
        List<RssFeedModel> Load(string url);
    }
}