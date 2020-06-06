using System;
using System.Collections.Generic;
using System.Text;
using ProEvoCanary.Domain.Commands;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Domain.EventHandlers
{
	public class AddEventCommandHandler
	{
		private readonly IEventRepository _eventRepository;

		public AddEventCommandHandler(IEventRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		public Guid Handle(AddEventCommand addEventCommand)
		{
			var eventCreated = _eventRepository.CreateEvent(addEventCommand.Name, addEventCommand.DateOfEvent, 1);
			return addEventCommand.Id;
		}
	}
}
