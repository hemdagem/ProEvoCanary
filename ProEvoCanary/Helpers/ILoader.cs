using System.Collections.Generic;
using ProEvo45.Models;

namespace ProEvo45.Helpers
{
    public interface ILoader
    {
        List<RssFeedModel> Load(string url);
    }
}