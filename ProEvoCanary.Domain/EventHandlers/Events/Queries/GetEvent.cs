using System;

namespace ProEvoCanary.Domain.EventHandlers.Events.Queries
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