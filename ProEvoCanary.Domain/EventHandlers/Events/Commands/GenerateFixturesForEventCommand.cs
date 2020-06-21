using System;
using System.Collections.Generic;

namespace ProEvoCanary.Application.EventHandlers.Events.Commands
{
	public class GenerateFixturesForEventCommand
	{
		public GenerateFixturesForEventCommand(Guid id, List<int> userIds)
		{
			Id = id;
			UserIds = userIds;
		}

		public Guid Id { get; }
		public List<int> UserIds { get; }
	}
}
