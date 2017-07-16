using System;
using System.Collections.Generic;
using System.Linq;
using ProEvoCanary.Domain.Helpers;
using ProEvoCanary.Domain.Helpers.Exceptions;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Models;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Domain.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IDbHelper _helper;
        private readonly IXmlGenerator _xmlGenerator;

        public EventRepository(IDbHelper helper, IXmlGenerator xmlGenerator)
        {
            _helper = helper;
            _xmlGenerator = xmlGenerator;
        }

        public List<EventModel> GetEvents()
        {
            var reader = _helper.ExecuteReader("up_GetTournamentDetails");
            var lstTournament = new List<EventModel>();
            while (reader.Read())
            {
                lstTournament.Add(new EventModel
                {
                    TournamentId = (int)reader["TournamentId"],
                    TournamentName = reader["TournamentName"].ToString(),
                    Date = reader["Date"].ToString(),
                    Name = reader["Name"].ToString(),
                    Completed = (bool)reader["Completed"]
                });

            }
            return lstTournament;
        }

        public EventModel GetEvent(int id)
        {
            var reader = _helper.ExecuteReaderMultiple("up_GetTournamentForEdit", new { TournamentId = id });
            var eventModel = reader.ReadFirst();
            EventModel tournament = new EventModel
            {
                TournamentId = eventModel.TournamentId,
                Completed = eventModel.Completed,
                Date = eventModel.Date.ToString(),
                TournamentName = eventModel.TournamentName,
                TournamentType = (TournamentType)Enum.Parse(typeof(TournamentType), eventModel.TournamentType.ToString()),
                Results = reader.Read<ResultsModel>().ToList()
            };



            tournament.FixturesGenerated = tournament.Results.Count > 0;

            return tournament;
        }

        public List<Standings> GetStandings(int id)
        {

            var reader = _helper.ExecuteReader("up_GetStandings", new { TournamentId = id });
            var standings = new List<Standings>();
            while (reader.Read())
            {
                standings.Add(new Standings
                {
                    TeamName = reader["Name"].ToString(),
                    UserId = (int)reader["UserID"],
                    Played = (int)reader["Played"],
                    Won = (int)reader["Won"],
                    Draw = (int)reader["Draw"],
                    Lost = (int)reader["Lost"],
                    For = (int)reader["For"],
                    Against = (int)reader["Against"],
                    GoalDifference = (int)reader["GoalDifference"],
                    Points = (int)reader["Points"],
                });
            }

            return standings;
        }

        public EventModel GetEventForEdit(int id, int ownerId)
        {

            var reader = _helper.ExecuteReader("up_GetTournamentForEdit", new { TournamentId = id });
            var tournament = new EventModel();
            while (reader.Read())
            {
                tournament = new EventModel
                {
                    TournamentId = id,
                    OwnerId = (int)reader["OwnerId"],
                    TournamentName = reader["TournamentName"].ToString(),
                    Date = reader["Date"].ToString(),
                    Completed = (bool)reader["Completed"],
                    TournamentType = (TournamentType)Enum.Parse(typeof(TournamentType), reader["TournamentType"].ToString()),
                };
            }
            reader.NextResult();

            tournament.Results = new List<ResultsModel>();
            while (reader.Read())
            {
                tournament.Results.Add(
                    new ResultsModel
                    {
                        AwayTeam = reader["AwayTeam"].ToString(),
                        HomeTeam = reader["HomeTeam"].ToString(),
                        AwayScore = (int)reader["AwayScore"],
                        HomeScore = (int)reader["HomeScore"],
                        ResultId = (int)reader["ResultId"],
                        TournamentId = (int)reader["TournamentId"]
                    });
            }

            tournament.FixturesGenerated = tournament.Results.Count > 0;


            if (ownerId != tournament.OwnerId)
                throw new NullReferenceException();

            return tournament;
        }

        public int CreateEvent(string tournamentname, DateTime date, int eventType, int ownerId)
        {
            if (string.IsNullOrEmpty(tournamentname))
            {
                throw new NullReferenceException("Tournament Name is null or empty");
            }

            if (ownerId < 1)
            {
                throw new LessThanOneException("Owner Id must be greater than zero");
            }

            return _helper.ExecuteScalar("up_AddTournament", new { TournamentName = tournamentname, TournamentType = eventType, Date = date, ownerId });
        }

        public void GenerateFixtures(int eventId, List<int> userIds)
        {
            var generator = new FixtureGenerator();
            var teamIds = generator.Generate(userIds);

            var documentString = _xmlGenerator.GenerateFixtures(teamIds, eventId);

            var parameters = new
            {
                XmlString = documentString
            };

            _helper.ExecuteNonQuery("up_AddFixtures", parameters);
        }

        public int AddTournamentUsers(int eventId, List<int> userIds)
        {
            var documentString = _xmlGenerator.GenerateTournamentUsers(userIds, eventId);

            var parameters = new
            {
                XmlString = documentString
            };

            return _helper.ExecuteNonQuery("up_AddTournamentUsers", parameters);
        }
    }
}