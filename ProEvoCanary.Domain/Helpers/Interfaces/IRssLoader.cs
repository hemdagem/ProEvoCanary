using System.Collections.Generic;
using ProEvoCanary.Domain.Models;

namespace ProEvoCanary.Domain.Helpers.Interfaces
{
    public interface IRssLoader
    {
        List<RssFeedModel> Load(string url);
    }
}