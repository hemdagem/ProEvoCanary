using System;
using System.Collections.Generic;
using ProEvoCanary.Domain.Helpers;
using ProEvoCanary.Domain.Helpers.Exceptions;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Models;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Domain.Repositories
{
    public class AdminEventRepository : IAdminEventRepository
    {
        private readonly IDBHelper _helper;
        private readonly IXmlGenerator _xmlGenerator;

        public AdminEventRepository(IDBHelper helper, IXmlGenerator xmlGenerator)
        {
            _helper = helper;
            _xmlGenerator = xmlGenerator;
        }

        public List<EventModel> GetEvents()
        {
            var reader = _helper.ExecuteReader("up_GetTournamentDetails");
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

        public EventModel GetEvent(int id)
        {
            if (id < 0)
                throw new IndexOutOfRangeException();

            var parameters = new Dictionary<string, IConvertible>
            {
                { "@Id", id }
            };

            var reader = _helper.ExecuteReader("up_GetTournamentForEdit", parameters);
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
            if (id < 0)
                throw new IndexOutOfRangeException();

            var parameters = new Dictionary<string, IConvertible>
            {
                { "@Id", id }
            };

            var reader = _helper.ExecuteReader("up_GetTournamentForEdit", parameters);
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

            if (tournament.OwnerId != ownerId)
                throw new IndexOutOfRangeException();

            return tournament;
        }

        public int CreateEvent(string tournamentname, DateTime utcNow, int eventType, int ownerId)
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
                { "@TournamentType", eventType },
                { "@Date", utcNow },
                { "@OwnerId", ownerId },
            };

            return _helper.ExecuteScalar("up_AddTournament", parameters);
        }

        public void GenerateFixtures(int eventId, List<int> userIds)
        {
            var generator = new FixtureGenerator();
            var teamIds = generator.Generate(userIds);

            var documentString = _xmlGenerator.GenerateXmlDocument(teamIds, eventId);

            var parameters = new Dictionary<string, IConvertible>
            {
                {"@XmlString", documentString }
            };

            _helper.ExecuteNonQuery("up_AddFixtures", parameters);
        }
    }
}