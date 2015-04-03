using System;
using System.Collections.Generic;
using ProEvoCanary.Helpers.Exceptions;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;
using EventModel = ProEvoCanary.Models.EventModel;

namespace ProEvoCanary.Repositories
{
    public class AdminEventRepository : IAdminEventRepository
    {
        private readonly IDBHelper _helper;

        public AdminEventRepository(IDBHelper helper)
        {
            _helper = helper;
        }

        public List<EventModel> GetEvents()
        {
            var reader = _helper.ExecuteReader("sp_GetTournamentDetails");
            var getEvents = new List<EventModel>();
            while (reader.Read())
            {
                getEvents.Add(new EventModel
                {
                    EventId = (int)reader["Id"],
                    EventName = reader["TournamentName"].ToString(),
                    Date = reader["Date"].ToString(),
                    Name = reader["Name"].ToString(),
                    Completed = (bool)reader["Completed"]
                });

            }
            return getEvents;
        }

        public EventModel GetEvent(int id, int ownerId)
        {
            if (id < 0)
                throw new IndexOutOfRangeException();

            var parameters = new Dictionary<string, IConvertible>
            {
                { "@Id", id }
            };

            var reader = _helper.ExecuteReader("sp_GetTournamentForEdit", parameters);
            var tournament = new EventModel();
            while (reader.Read())
            {
                tournament = new EventModel
                {
                    EventId = id,
                    OwnerId = (int)reader["OwnerId"],
                    EventName = reader["TournamentName"].ToString(),
                    Date = reader["Date"].ToString(),
                    Completed = (bool)reader["Completed"],
                    FixturesGenerated = (bool)reader["Completed"],
                    EventTypes = (EventTypes)Enum.Parse(typeof(EventTypes), reader["TournamentType"].ToString()),
                };
            }

            if (tournament.OwnerId != ownerId)
                throw new IndexOutOfRangeException();

            return tournament;
        }

        public int CreateEvent(string tournamentname, DateTime utcNow, EventTypes? eventType, int ownerId)
        {
            if (string.IsNullOrEmpty(tournamentname))
            {
                throw new NullReferenceException("Tournament Name is null or empty");
            }

            if (ownerId < 1)
            {
                throw new LessThanOneException("Owner Id must be greater than zero");
            }

            var parameters = new Dictionary<string, IConvertible>
            {
                { "@TournamentName", tournamentname },
                { "@TournamentType", (int)eventType },
                { "@Date", utcNow },
                { "@OwnerId", ownerId },
            };

            return _helper.ExecuteScalar("sp_AddTournament", parameters);
        }

        public void GenerateFixtures(int eventId, List<int> userIds)
        {
            throw new NotImplementedException();
        }
    }
}