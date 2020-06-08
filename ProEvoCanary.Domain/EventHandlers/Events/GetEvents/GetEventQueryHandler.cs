using System.Collections.Generic;
using AutoMapper;
using ProEvoCanary.DataAccess.Repositories;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.Helpers;

namespace ProEvoCanary.Domain.EventHandlers.Events.GetEvents
{
	public class GetEventsQueryHandler : IQuery<List<GetEvent.EventModelDto>>
	{
		private readonly IEventReadRepository _eventRepository;
		private readonly IMapper _mapper;
		private readonly ICacheManager _cacheManager;
		private const string EventsListCacheKey = "EventsListCache";


		public GetEventsQueryHandler(IEventReadRepository eventRepository, IMapper mapper, ICacheManager cacheManager)
		{
			_eventRepository = eventRepository;
			_mapper = mapper;
			_cacheManager = cacheManager;
		}

		public List<GetEvent.EventModelDto> Handle()
		{
			var eventModel = _cacheManager.AddOrGetExisting(EventsListCacheKey, () => _eventRepository.GetEvents());

			return _mapper.Map<List<GetEvent.EventModelDto>>(eventModel);
		}
	}

}
