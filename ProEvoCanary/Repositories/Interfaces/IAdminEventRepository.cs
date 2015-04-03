using System;
using System.Collections.Generic;
using ProEvoCanary.Models;

namespace ProEvoCanary.Repositories.Interfaces
{
    public interface IAdminEventRepository : IEventRepository
    {
        int CreateEvent(string tournamentname, DateTime utcNow, EventTypes eventType, int ownerId);
        void GenerateFixtures(int eventId, List<int> userIds);
    }
}