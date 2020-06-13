using System;
using System.Collections.Generic;
using ProEvoCanary.DataAccess.Models;
using ProEvoCanary.DataAccess.Repositories.Interfaces;

namespace ProEvoCanary.DataAccess.Repositories
{
	public class EventReadRepository : IEventReadRepository
	{
		private readonly IDbHelper _helper;

		public EventReadRepository(IDbHelper helper)
		{
			_helper = helper;
		}
        public List<EventModel> GetEvents()
        {
            var reader = _helper.ExecuteReader("up_GetTournamentDetails");
            var lstTournament = new List<EventModel>();
            while (reader.Read())
            {
                lstTournament.Add(new EventModel
                {
                    TournamentId = (Guid)reader["TournamentId"],
                    TournamentName = reader["TournamentName"].ToString(),
                    Date = reader["Date"].ToString(),
                    Name = reader["Name"].ToString(),
                    Completed = (bool)reader["Completed"]
                });

            }
            return lstTournament;
        }

        public EventModel GetEvent(Guid id)
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
                        ResultId = (Guid)reader["ResultId"],
                        TournamentId = (Guid)reader["TournamentId"]
                    });
            }

            tournament.FixturesGenerated = tournament.Results.Count > 0;

            return tournament;
        }
    }
}