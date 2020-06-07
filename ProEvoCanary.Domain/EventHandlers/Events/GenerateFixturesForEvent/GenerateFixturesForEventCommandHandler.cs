using System;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Domain.EventHandlers.Events.GenerateFixturesForEvent
{
	public class GenerateFixturesForEventCommandHandler : ICommandHandlerBase<GenerateFixturesForEventCommand, Guid>
	{
		private readonly IEventWriteRepository _eventRepository;

		public GenerateFixturesForEventCommandHandler(IEventWriteRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		public Guid Handle(GenerateFixturesForEventCommand generateFixturesForEventCommand)
		{
			_eventRepository.AddTournamentUsers(generateFixturesForEventCommand.Id, generateFixturesForEventCommand.UserIds);
			_eventRepository.GenerateFixtures(generateFixturesForEventCommand.Id, generateFixturesForEventCommand.UserIds);
			return generateFixturesForEventCommand.Id;
		}

	}
}
