using System;
using ProEvoCanary.DataAccess.Repositories.Interfaces;
using ProEvoCanary.Domain.EventHandlers.Configuration;

namespace ProEvoCanary.Domain.EventHandlers.Events.AddEvent
{
	public class AddEventCommandHandler : ICommandHandler<AddEventCommand, Guid>
	{
		private readonly IEventWriteRepository _eventRepository;

		public AddEventCommandHandler(IEventWriteRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		public Guid Handle(AddEventCommand command)
		{
			_eventRepository.CreateEvent(command.Name, command.DateOfEvent, 1);
			return command.Id;
		}

	}
}
