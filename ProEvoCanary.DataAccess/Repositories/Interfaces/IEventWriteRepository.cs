using System;
using System.Collections.Generic;

namespace ProEvoCanary.DataAccess.Repositories.Interfaces
{
	public interface IEventWriteRepository
	{
		int CreateEvent(string tournamentName, DateTime Date, int eventType);
		void GenerateFixtures(Guid eventId, List<int> userIds);
		int AddTournamentUsers(Guid eventId, List<int> userIds);
	}
}