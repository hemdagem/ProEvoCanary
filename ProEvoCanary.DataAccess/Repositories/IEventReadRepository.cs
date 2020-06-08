using System;
using System.Collections.Generic;
using ProEvoCanary.DataAccess.Models;

namespace ProEvoCanary.DataAccess.Repositories
{
	public interface IEventReadRepository
	{
		List<EventModel> GetEvents();
		EventModel GetEvent(Guid id);
	}
}