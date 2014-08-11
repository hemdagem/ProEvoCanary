using System.Collections.Generic;
using ProEvoCanary.Models;

namespace ProEvoCanary.Helpers
{
    public interface ILoader
    {
        List<RssFeedModel> Load(string url);
    }
}