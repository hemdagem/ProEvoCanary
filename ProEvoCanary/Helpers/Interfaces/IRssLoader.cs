using System.Collections.Generic;
using ProEvoCanary.Models;

namespace ProEvoCanary.Helpers.Interfaces
{
    public interface IRssLoader
    {
        List<RssFeedModel> Load(string url);
    }
}