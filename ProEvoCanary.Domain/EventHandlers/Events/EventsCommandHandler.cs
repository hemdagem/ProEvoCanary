using System;
using System.Collections.Generic;
using System.Text;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.EventHandlers.Events.GetEvent;

namespace ProEvoCanary.Domain.EventHandlers.Events
{
	class EventsCommandHandler : IQuery<List<GetEvent.EventModelDto>>, IQueryHandler<GetEvent.GetEvent, EventModelDto>
	{
		public List<EventModelDto> Handle()
		{
			throw new NotImplementedException();
		}

		public EventModelDto Handle(GetEvent.GetEvent query)
		{
			throw new NotImplementedException();
		}
	}
}
