using System.Collections.Generic;
using AutoMapper;
using ProEvoCanary.DataAccess.Repositories;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.Infrastructure;

namespace ProEvoCanary.Domain.EventHandlers.Events.GetEvents
{
	public interface IEventsQueryHandler :
		IQuery<List<EventModelDto>>,
		IQueryHandler<GetEvent, EventModelDto>
	{

	}
	public class EventsQueryHandler : IEventsQueryHandler
	{
		private readonly IEventReadRepository _eventRepository;
		private readonly IMapper _mapper;
		private readonly ICacheManager _cacheManager;
		private const string EventsListCacheKey = "EventsListCache";


		public EventsQueryHandler(IEventReadRepository eventRepository, IMapper mapper, ICacheManager cacheManager)
		{
			_eventRepository = eventRepository;
			_mapper = mapper;
			_cacheManager = cacheManager;
		}

		public List<EventModelDto> Handle()
		{
			var eventModel = _cacheManager.AddOrGetExisting(EventsListCacheKey, () => _eventRepository.GetEvents());

			return _mapper.Map<List<EventModelDto>>(eventModel);
		}

		public EventModelDto Handle(GetEvent query)
		{
			var eventModel = _eventRepository.GetEvent(query.Id);
			return _mapper.Map<EventModelDto>(eventModel);
		}
	}
}
