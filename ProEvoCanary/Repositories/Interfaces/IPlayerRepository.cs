using System.Collections.Generic;
using ProEvoCanary.Models;

namespace ProEvoCanary.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        List<PlayerModel> GetTopPlayers();
        SelectListModel GetAllPlayers();
    }
}