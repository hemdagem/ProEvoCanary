using System;
using ProEvoCanary.Application.EventHandlers.Configuration;

namespace ProEvoCanary.Application.EventHandlers.Events.Commands
{
	public interface IEventCommandHandler : ICommandHandler<AddEventCommand, Guid>, ICommandHandler<GenerateFixturesForEventCommand, Guid>
	{

	}
}