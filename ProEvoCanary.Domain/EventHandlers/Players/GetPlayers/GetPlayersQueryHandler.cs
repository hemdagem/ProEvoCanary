using System.Collections.Generic;
using AutoMapper;
using ProEvoCanary.DataAccess.Repositories.Interfaces;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.Infrastructure;

namespace ProEvoCanary.Domain.EventHandlers.Players.GetPlayers
{
	public class GetPlayersQueryHandler : IQuery<List<PlayerModelDto>>
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
