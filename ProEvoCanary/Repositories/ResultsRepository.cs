using System.Collections.Generic;
using ProEvoCanary.Helpers;
using ProEvoCanary.Repositories.Interfaces;
using ProEvoCanary.Models;

namespace ProEvoCanary.Repositories
{
    public class ResultsRepository : IResultRepository
    {
        private readonly IDBHelper _helper;

        public ResultsRepository(IDBHelper helper)
        {
            _helper = helper;
        }

        public ResultsRepository()
            : this(new DBHelper())
        {

        }

        public List<ResultsModel> GetResults()
        {

            var reader = _helper.ExecuteReader("sp_RecentResults");
            var lstResults = new List<ResultsModel>();
            if (reader != null)
            {
                while (reader.Read())
                {

                    lstResults.Add(new ResultsModel
                    {
                        HomeTeam = reader["HomeTeam"].ToString(),
                        AwayTeam = reader["AwayTeam"].ToString(),
                        HomeScore = int.Parse(reader["HomeScore"].ToString()),
                        AwayScore = int.Parse(reader["AwayScore"].ToString()),
                        HomeTeamID = int.Parse(reader["HomeTeamID"].ToString()),
                        AwayTeamID = int.Parse(reader["AwayTeamID"].ToString()),
                        ResultID = int.Parse(reader["ResultsID"].ToString())
                    });
                }
            }

            return lstResults;
        }

        public List<ResultsModel> GetHeadToHeadResults(int playerOne, int playerTwo)
        {
            _helper.AddParameter("@UserOneID", playerOne);
            _helper.AddParameter("@UserTwoID", playerTwo);
            var reader = _helper.ExecuteReader("sp_HeadToHeadResults");
            var lstResults = new List<ResultsModel>();
            if (reader != null)
            {
                while (reader.Read())
                {

                    lstResults.Add(new ResultsModel
                    {
                        HomeTeam = reader["HomeUser"].ToString(),
                        AwayTeam = reader["AwayUser"].ToString(),
                        HomeScore = int.Parse(reader["HomeScore"].ToString()),
                        AwayScore = int.Parse(reader["AwayScore"].ToString()),
                        ResultID = int.Parse(reader["ResultsID"].ToString())
                    });
                }
            }

            return lstResults;
        }
    }
}