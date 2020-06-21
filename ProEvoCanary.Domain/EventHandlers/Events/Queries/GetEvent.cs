using System;

namespace ProEvoCanary.Application.EventHandlers.Events.Queries
{
	public class GetEvent
	{
		public GetEvent(Guid eventId)
		{
			Id = eventId;
		}

		public Guid Id { get;}
	}
}