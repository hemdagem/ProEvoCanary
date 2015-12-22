using System.Collections.Generic;
using ProEvoCanary.Domain.Models;

namespace ProEvoCanary.Domain.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        List<PlayerModel> GetTopPlayers();
        List<PlayerModel> GetTopPlayersRange(int pageNumber, int playersPerPage);
        List<PlayerModel> GetAllPlayers();
    }
}