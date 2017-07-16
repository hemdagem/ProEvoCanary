using System;
using System.Collections.Generic;
using ProEvoCanary.Domain.Models;
using EventModel = ProEvoCanary.Domain.Models.EventModel;

namespace ProEvoCanary.Domain.Repositories.Interfaces
{
    public interface IEventRepository
    {
        List<EventModel> GetEvents();
        EventModel GetEvent(int id);
        List<Standings> GetStandings(int id);
        EventModel GetEventForEdit(int id, int ownerId);
        int CreateEvent(string tournamentname, DateTime Date, int eventType, int ownerId);
        void GenerateFixtures(int eventId, List<int> userIds);
        int AddTournamentUsers(int eventId, List<int> userIds);
    }
}