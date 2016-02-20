using System;
using System.Collections.Generic;
using EventModel = ProEvoCanary.Domain.Models.EventModel;

namespace ProEvoCanary.Domain.Repositories.Interfaces
{
    public interface IEventRepository
    {
        List<EventModel> GetEvents();
        EventModel GetEvent(int id);
        EventModel GetEventForEdit(int id, int ownerId);
        int CreateEvent(string tournamentname, DateTime utcNow, int eventType, int ownerId);
        void GenerateFixtures(int eventId, List<int> userIds);
    }
}