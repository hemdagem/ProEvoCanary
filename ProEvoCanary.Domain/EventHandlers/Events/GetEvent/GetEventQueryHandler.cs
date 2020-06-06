using System;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Domain.EventHandlers.Events.GetEvent
{
	public class GetEventQueryHandler : IQueryHandlerBase<GetEventQuery, EventModel>
	{
		private readonly IEventRepository _eventRepository;

		public GetEventQueryHandler(IEventRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		public EventModel Handle(GetEventQuery getEventQuery)
		{
			
			var eventModel = _eventRepository.GetEvent(1);
			return new EventModel
			{
				Completed = eventModel.Completed,
				Date = eventModel.Date,
				FixturesGenerated = eventModel.FixturesGenerated,
				Name = eventModel.Name,
			};
		}
	}
}
