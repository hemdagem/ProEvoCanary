using System.Collections.Generic;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Repositories
{
    public class PlayerRepositoryDecorator : IPlayerRepository
    {
        private readonly ICachePlayerRepository _cacheRepository;
        private readonly IPlayerRepository _playerRepository;
        private const string TopPlayerListCacheKey = "TopPlayerCacheList";
        private const string PlayerListCacheKey = "PlayerCacheList";

        public PlayerRepositoryDecorator(ICachePlayerRepository cacheRepository, IPlayerRepository playerRepository)
        {
            _cacheRepository = cacheRepository;
            _playerRepository = playerRepository;
        }

        public List<PlayerModel> GetTopPlayers()
        {
            var players = _cacheRepository.GetTopPlayers();

            if (players != null) return players;
            
            players = _playerRepository.GetTopPlayers();
            
            _cacheRepository.AddToCache(TopPlayerListCacheKey, players, 30);

            return players;
        }

        public List<PlayerModel> GetTopPlayersRange(int pageNumber, int playersPerPage)
        {
            throw new System.NotImplementedException();
        }

        public List<PlayerModel> GetAllPlayers()
        {
            var players = _cacheRepository.GetAllPlayers();

            if (players != null) return players;

            players = _playerRepository.GetAllPlayers();
            
            _cacheRepository.AddToCache(PlayerListCacheKey, players, 30);

            return players;
        }


    }
}