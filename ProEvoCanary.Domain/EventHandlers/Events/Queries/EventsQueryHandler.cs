using System.Collections.Generic;
using System.Linq;
using ProEvoCanary.Application.EventHandlers.Configuration;
using ProEvoCanary.Application.Infrastructure;
using ProEvoCanary.DataAccess.Repositories.Interfaces;

namespace ProEvoCanary.Application.EventHandlers.Events.Queries
{
	public interface IEventsQueryHandler :
		IQuery<List<EventModelDto>>,
		IQueryHandler<GetEvent, EventModelDto>
	{

	}
	public class EventsQueryHandler : IEventsQueryHandler
	{
		private readonly IEventReadRepository _eventRepository;
		private readonly ICacheManager _cacheManager;
		private const string EventsListCacheKey = "EventsListCache";


		public EventsQueryHandler(IEventReadRepository eventRepository, ICacheManager cacheManager)
		{
			_eventRepository = eventRepository;
			_cacheManager = cacheManager;
		}

		public List<EventModelDto> Handle()
		{
			var eventModel = _cacheManager.AddOrGetExisting(EventsListCacheKey, () => _eventRepository.GetEvents());

			return eventModel.Select(x => new EventModelDto {TournamentName = x.TournamentName}).ToList();
		}

		public EventModelDto Handle(GetEvent query)
		{
			var eventModel = _eventRepository.GetEvent(query.Id);
			return new EventModelDto {TournamentName = eventModel.TournamentName};
		}
	}
}
