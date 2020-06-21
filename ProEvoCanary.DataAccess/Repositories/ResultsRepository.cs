using System;
using System.Collections.Generic;
using ProEvoCanary.DataAccess.Models;
using ProEvoCanary.DataAccess.Repositories.Interfaces;

namespace ProEvoCanary.DataAccess.Repositories
{
    public class ResultsRepository : IResultRepository
    {
        private readonly IDbHelper _helper;

        public ResultsRepository(IDbHelper helper)
        {
            _helper = helper;
        }

        public List<ResultsModel> GetResults()
        {
            var resultsList = new List<ResultsModel>();

            using (var reader = _helper.ExecuteReader("up_RecentResults"))
            {
                while (reader.Read())
                {
                    resultsList.Add(new ResultsModel
                    {
                        HomeTeam = reader["HomeTeam"].ToString(),
                        AwayTeam = reader["AwayTeam"].ToString(),
                        HomeScore = int.Parse(reader["HomeScore"].ToString()),
                        AwayScore = int.Parse(reader["AwayScore"].ToString()),
                        HomeTeamId = int.Parse(reader["HomeTeamId"].ToString()),
                        AwayTeamId = int.Parse(reader["AwayTeamId"].ToString()),
                        ResultId = Guid.Parse(reader["ResultId"].ToString())
                    });
                }
            }

            return resultsList;
        }

        public RecordsModel GetHeadToHeadRecord(int playerOne, int playerTwo)
        {
            var parameters = new 
            {
                UserOneId =playerOne,
                UserTwoId =playerTwo,
            };

            var headToHeadRecordList = new RecordsModel { Results = new List<ResultsModel>() };
            using (var reader = _helper.ExecuteReader("up_HeadToHeadRecord", parameters))
            {
                while (reader.Read())
                {
                    headToHeadRecordList.TotalMatches = (int)reader["TotalMatches"];
                    headToHeadRecordList.TotalDraws = (int)reader["TotalDraws"];
                    headToHeadRecordList.PlayerOneWins = (int)reader["PlayerOneWins"];
                    headToHeadRecordList.PlayerTwoWins = (int)reader["PlayerTwoWins"];
                }

                reader.NextResult();

                while (reader.Read())
                {
                    headToHeadRecordList.Results.Add(new ResultsModel
                    {
                        HomeTeam = reader["HomeUser"].ToString(),
                        AwayTeam = reader["AwayUser"].ToString(),
                        HomeScore = (int)reader["HomeScore"],
                        AwayScore = (int)reader["AwayScore"],
                        ResultId = (Guid)reader["ResultId"]
                    });
                }
            }

            return headToHeadRecordList;
        }

        public int AddResult(Guid id, int homeScore, int awayScore)
        {
            var parameters = new
            {
                ResultId = id,
                HomeScore = homeScore < 0 ? 0 : homeScore,
                AwayScore = awayScore < 0 ? 0 : awayScore
            };

            return _helper.ExecuteScalar("up_AddResult", parameters);
        }

        public ResultsModel GetResult(Guid id)
        {

            ResultsModel model = null;

            using (var reader = _helper.ExecuteReader("up_GetResult", new { ResultId = id }))
            {
                while (reader.Read())
                {
                    model = new ResultsModel
                    {
                        HomeTeam = reader["HomeTeam"].ToString(),
                        AwayTeam = reader["AwayTeam"].ToString(),
                        HomeScore = int.Parse(reader["HomeScore"].ToString()),
                        AwayScore = int.Parse(reader["AwayScore"].ToString()),
                        ResultId = Guid.Parse(reader["ResultId"].ToString()),
                        TournamentId = Guid.Parse(reader["TournamentId"].ToString()),
                    };
                }
            }

            return model;
        }
    }
}