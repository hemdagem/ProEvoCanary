using System.Collections.Generic;
using AutoMapper;
using ProEvoCanary.Application.EventHandlers.Configuration;
using ProEvoCanary.Application.Infrastructure;
using ProEvoCanary.DataAccess.Repositories.Interfaces;

namespace ProEvoCanary.Application.EventHandlers.Players.GetPlayers
{
	public interface IGetPlayersQueryHandler : IQuery<List<PlayerModelDto>> { }
	public class GetPlayersQueryHandler : IGetPlayersQueryHandler
	{
		private readonly IPlayerRepository _playerRepository;
		private readonly IMapper _mapper;
		private readonly ICacheManager _cacheManager;
		private const string PlayerCacheListKey = "PlayerCacheList";

		public GetPlayersQueryHandler(IPlayerRepository playerRepository, IMapper mapper, ICacheManager cacheManager)
		{
			_playerRepository = playerRepository;
			_mapper = mapper;
			_cacheManager = cacheManager;
		}

		public List<PlayerModelDto> Handle()
		{
			var eventModel = _cacheManager.AddOrGetExisting(PlayerCacheListKey, () => _playerRepository.GetAllPlayers());

			return _mapper.Map<List<PlayerModelDto>>(eventModel);
		}
	}

}
