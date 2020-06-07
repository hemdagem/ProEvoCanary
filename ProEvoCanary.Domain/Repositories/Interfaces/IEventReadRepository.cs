using System;
using System.Collections.Generic;
using ProEvoCanary.Domain.Models;

namespace ProEvoCanary.Domain.Repositories.Interfaces
{
	public interface IEventReadRepository
	{
		List<EventModel> GetEvents();
		EventModel GetEvent(Guid id);
	}
}