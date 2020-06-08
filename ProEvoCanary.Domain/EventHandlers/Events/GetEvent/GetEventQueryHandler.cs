using System;
using ProEvoCanary.DataAccess.Repositories;
using ProEvoCanary.Domain.EventHandlers.Configuration;

namespace ProEvoCanary.Domain.EventHandlers.Events.GetEvent
{
	public class GetEventQueryHandler : IQueryHandler<GetEvent, EventModelDto>
	{
		private readonly IEventReadRepository _eventRepository;

		public GetEventQueryHandler(IEventReadRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		public EventModelDto Handle(GetEvent query)
		{
			
			var eventModel = _eventRepository.GetEvent(query.Id);
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
