using System;
using System.Collections.Generic;

namespace ProEvoCanary.Domain.Repositories.Interfaces
{
    public interface IAdminEventRepository : IEventRepository
    {
        int CreateEvent(string tournamentname, DateTime utcNow, int eventType, int ownerId);
        void GenerateFixtures(int eventId, List<int> userIds);
    }
}