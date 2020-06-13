using System;
using ProEvoCanary.Domain.EventHandlers.Configuration;

namespace ProEvoCanary.Domain.EventHandlers.Events.Commands
{
	public interface IEventCommandHandler : ICommandHandler<AddEventCommand, Guid>, ICommandHandler<GenerateFixturesForEventCommand, Guid>
	{

	}
}