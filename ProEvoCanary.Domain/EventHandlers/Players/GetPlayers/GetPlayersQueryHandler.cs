using System.Collections.Generic;
using System.Linq;
using ProEvoCanary.Application.EventHandlers.Configuration;
using ProEvoCanary.Application.Infrastructure;
using ProEvoCanary.DataAccess.Repositories.Interfaces;

namespace ProEvoCanary.Application.EventHandlers.Players.GetPlayers
{
	public interface IGetPlayersQueryHandler : IQuery<List<PlayerModelDto>> { }
	public class GetPlayersQueryHandler : IGetPlayersQueryHandler
	{
		private readonly IPlayerRepository _playerRepository;
		private readonly ICacheManager _cacheManager;
		private const string PlayerCacheListKey = "PlayerCacheList";

		public GetPlayersQueryHandler(IPlayerRepository playerRepository, ICacheManager cacheManager)
		{
			_playerRepository = playerRepository;
			_cacheManager = cacheManager;
		}

		public List<PlayerModelDto> Handle()
		{
			var eventModel = _cacheManager.AddOrGetExisting(PlayerCacheListKey, () => _playerRepository.GetAllPlayers());

			return eventModel.Select(x => new PlayerModelDto
			{
				GoalsPerGame = x.GoalsPerGame, MatchesPlayed = x.MatchesPlayed, PlayerId = x.PlayerId,
				PlayerName = x.PlayerName, PointsPerGame = x.PointsPerGame
			}).ToList();
		}
	}

}
