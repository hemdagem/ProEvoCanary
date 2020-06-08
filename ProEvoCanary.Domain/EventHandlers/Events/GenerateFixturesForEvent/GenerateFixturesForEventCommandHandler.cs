using System;
using ProEvoCanary.DataAccess.Repositories.Interfaces;
using ProEvoCanary.Domain.EventHandlers.Configuration;

namespace ProEvoCanary.Domain.EventHandlers.Events.GenerateFixturesForEvent
{
	public class GenerateFixturesForEventCommandHandler : ICommandHandler<GenerateFixturesForEventCommand, Guid>
	{
		private readonly IEventWriteRepository _eventRepository;

		public GenerateFixturesForEventCommandHandler(IEventWriteRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		public Guid Handle(GenerateFixturesForEventCommand command)
		{
			_eventRepository.AddTournamentUsers(command.Id, command.UserIds);
			_eventRepository.GenerateFixtures(command.Id, command.UserIds);
			return command.Id;
		}

	}
}
