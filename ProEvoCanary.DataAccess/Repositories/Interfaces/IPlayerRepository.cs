using System.Collections.Generic;
using ProEvoCanary.DataAccess.Models;

namespace ProEvoCanary.DataAccess.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        List<PlayerModel> GetTopPlayers();
        List<PlayerModel> GetTopPlayersRange(int pageNumber, int playersPerPage);
        List<PlayerModel> GetAllPlayers();
    }
}