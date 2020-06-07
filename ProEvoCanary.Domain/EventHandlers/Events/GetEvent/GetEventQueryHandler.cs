using System;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Domain.EventHandlers.Events.GetEvent
{
	public class GetEventQueryHandler : IQueryHandlerBase<GetEventQuery, EventModelDto>
	{
		private readonly IEventReadRepository _eventRepository;

		public GetEventQueryHandler(IEventReadRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		public EventModelDto Handle(GetEventQuery getEventQuery)
		{
			
			var eventModel = _eventRepository.GetEvent(getEventQuery.Id);
			return new EventModelDto
			{
				Completed = eventModel.Completed,
				Date = eventModel.Date,
				FixturesGenerated = eventModel.FixturesGenerated,
				Name = eventModel.Name,
			};
		}
	}

}
