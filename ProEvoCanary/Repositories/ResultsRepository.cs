using System.Collections.Generic;
using ProEvoCanary.Helpers;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Repositories.Interfaces;
using ProEvoCanary.Models;

namespace ProEvoCanary.Repositories
{
    public class ResultsRepository : IResultRepository
    {
        private readonly IdBHelper _helper;

        public ResultsRepository(IdBHelper helper)
        {
            _helper = helper;
        }

        public ResultsRepository() : this(new DBHelper()) { }

        public List<ResultsModel> GetResults()
        {
            _helper.ClearParameters();
            var lstResults = new List<ResultsModel>();

            using (var reader = _helper.ExecuteReader("sp_RecentResults"))
            {
                while (reader.Read())
                {
                    lstResults.Add(new ResultsModel
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

            return lstResults;
        }

        public RecordsModel GetHeadToHeadRecord(int playerOne, int playerTwo)
        {
            _helper.ClearParameters();
            _helper.AddParameter("@UserOneId", playerOne);
            _helper.AddParameter("@UserTwoId", playerTwo);
            var headToHeadRecordList = new RecordsModel {Results = new List<ResultsModel>()};
            using (var reader = _helper.ExecuteReader("sp_HeadToHeadRecord"))
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
                        HomeScore = int.Parse(reader["HomeScore"].ToString()),
                        AwayScore = int.Parse(reader["AwayScore"].ToString()),
                        ResultId = int.Parse(reader["Id"].ToString())
                    });
                }
            }

            return headToHeadRecordList;
        }

    }
}