using System.Collections.Generic;
using ProEvoCanary.Domain;

namespace ProEvoCanary.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        List<PlayerModel> GetTopPlayers();
        List<PlayerModel> GetTopPlayersRange(int pageNumber, int playersPerPage);
        List<PlayerModel> GetAllPlayers();
    }
}