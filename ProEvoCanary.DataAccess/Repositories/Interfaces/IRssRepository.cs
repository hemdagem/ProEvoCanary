using System.Collections.Generic;
using ProEvoCanary.DataAccess.Models;

namespace ProEvoCanary.DataAccess.Repositories.Interfaces
{
    public interface IRssRepository
    {
        List<RssFeedModel> Load(string url);
    }
}