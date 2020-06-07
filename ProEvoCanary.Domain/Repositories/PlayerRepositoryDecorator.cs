using System.Collections.Generic;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Models;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Domain.Repositories
{
    public class PlayerRepositoryDecorator : IPlayerRepository
    {
        private readonly ICacheManager _cacheRepository;
        private readonly IPlayerRepository _playerRepository;
        private const string TopPlayerListCacheKey = "TopPlayerCacheList";
        private const string PlayerListCacheKey = "PlayerCacheList";

        public PlayerRepositoryDecorator(ICacheManager cacheRepository, IPlayerRepository playerRepository)
        {
            _cacheRepository = cacheRepository;
            _playerRepository = playerRepository;
        }

        public List<PlayerModel> GetTopPlayers()
        {
            return _cacheRepository.AddOrGetExisting(TopPlayerListCacheKey, () => _playerRepository.GetTopPlayers());
        }

        public List<PlayerModel> GetTopPlayersRange(int pageNumber, int playersPerPage)
        {
            throw new System.NotImplementedException();
        }

        public List<PlayerModel> GetAllPlayers()
        {
	        return _cacheRepository.AddOrGetExisting(PlayerListCacheKey, () => _playerRepository.GetAllPlayers());
        }


    }
}