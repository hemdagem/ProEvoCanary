using System;

namespace ProEvoCanary.Domain.EventHandlers.Events.GetEvents
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