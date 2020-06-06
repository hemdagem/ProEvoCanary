using System;

namespace ProEvoCanary.Domain.EventHandlers.Events.GetEvent
{
	public class GetEventQuery
	{
		public GetEventQuery(Guid eventId)
		{
			Id = eventId;
		}

		public Guid Id { get;}
	}
}