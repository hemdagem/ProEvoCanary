using System;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Domain.EventHandlers.Events.AddEvent
{
	public class AddEventCommandHandler : ICommandHandlerBase<AddEventCommand, Guid>
	{
		private readonly IEventWriteRepository _eventRepository;

		public AddEventCommandHandler(IEventWriteRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		public Guid Handle(AddEventCommand generateFixturesForEventCommand)
		{
			_eventRepository.CreateEvent(generateFixturesForEventCommand.Name, generateFixturesForEventCommand.DateOfEvent, 1);
			return generateFixturesForEventCommand.Id;
		}

	}
}
