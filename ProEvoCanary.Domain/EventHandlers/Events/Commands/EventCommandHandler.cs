using System;
using ProEvoCanary.DataAccess.Repositories.Interfaces;

namespace ProEvoCanary.Domain.EventHandlers.Events.Commands
{
	public class EventCommandHandler : IEventCommandHandler
	{
		private readonly IEventWriteRepository _eventRepository;

		public EventCommandHandler(IEventWriteRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		public Guid Handle(AddEventCommand command)
		{
			_eventRepository.CreateEvent(command.Name, command.DateOfEvent, 1);
			return command.Id;
		}

		public Guid Handle(GenerateFixturesForEventCommand command)
		{
			_eventRepository.AddTournamentUsers(command.Id, command.UserIds);
			_eventRepository.GenerateFixtures(command.Id, command.UserIds);
			return command.Id;
		}
	}
}
