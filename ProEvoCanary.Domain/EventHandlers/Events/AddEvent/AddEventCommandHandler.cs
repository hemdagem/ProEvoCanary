using System;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Domain.EventHandlers.Events.AddEvent
{
	public class AddEventCommandHandler : ICommandHandlerBase<AddEventCommand, Guid>
	{
		private readonly IEventRepository _eventRepository;

		public AddEventCommandHandler(IEventRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		public Guid Handle(AddEventCommand addEventCommand)
		{
			_eventRepository.CreateEvent(addEventCommand.Name, addEventCommand.DateOfEvent, 1);
			return addEventCommand.Id;
		}

	}
}
