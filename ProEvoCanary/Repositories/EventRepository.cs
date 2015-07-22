using System;
using System.Collections.Generic;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;
using EventModel = ProEvoCanary.Models.EventModel;

namespace ProEvoCanary.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IDBHelper _helper;

        public EventRepository(IDBHelper helper)
        {
            _helper = helper;
        }

        public List<EventModel> GetEvents()
        {
            var reader = _helper.ExecuteReader("sp_GetTournamentDetails");
            var lstTournament = new List<EventModel>();
            while (reader.Read())
            {
                lstTournament.Add(new EventModel
                {
                    EventId = (int)reader["Id"],
                    EventName = reader["TournamentName"].ToString(),
                    Date = reader["Date"].ToString(),
                    Name = reader["Name"].ToString(),
                    Completed = (bool)reader["Completed"]
                });

            }
            return lstTournament;
        }

        public EventModel GetEvent(int id)
        {
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
            reader.NextResult();
            while (reader.Read())
            {
                tournament.Results = new List<ResultsModel>
                {
                    new ResultsModel
                    {
                        AwayTeam = reader["AwayTeam"].ToString(),
                        HomeTeam = reader["HomeTeam"].ToString(),
                        AwayScore = (int)reader["AwayScore"],
                        HomeScore = (int)reader["HomeScore"],
                    }
                };
            }

            return tournament;
        }

        public EventModel GetEventForEdit(int id, int ownerId)
        {
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
        

            if (ownerId != tournament.OwnerId)
                throw new NullReferenceException();

            return tournament;
        }
    }
}