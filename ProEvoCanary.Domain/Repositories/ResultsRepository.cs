using System;
using System.Collections.Generic;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Models;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Domain.Repositories
{
    public class ResultsRepository : IResultRepository
    {
        private readonly IDBHelper _helper;

        public ResultsRepository(IDBHelper helper)
        {
            _helper = helper;
        }

        public List<ResultsModel> GetResults()
        {
            var resultsList = new List<ResultsModel>();

            using (var reader = _helper.ExecuteReader("sp_RecentResults"))
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
                        ResultId = int.Parse(reader["Id"].ToString())
                    });
                }
            }

            return resultsList;
        }

        public RecordsModel GetHeadToHeadRecord(int playerOne, int playerTwo)
        {
            var parameters = new Dictionary<string, IConvertible>
            {
                { "@UserOneId", playerOne },
                { "@UserTwoId", playerTwo },
            };

            var headToHeadRecordList = new RecordsModel { Results = new List<ResultsModel>() };
            using (var reader = _helper.ExecuteReader("sp_HeadToHeadRecord", parameters))
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
                        ResultId = (int)reader["Id"]
                    });
                }
            }

            return headToHeadRecordList;
        }
    }
}