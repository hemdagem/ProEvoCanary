using System.Collections.Generic;
using ProEvoCanary.Helpers;
using ProEvoCanary.Helpers.Interfaces;
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
            var lstResults = new List<ResultsModel>();

            _helper.ClearParameters();
            _helper.AddParameter("@UserOneID", playerOne);
            _helper.AddParameter("@UserTwoID", playerTwo);


            using (var reader = _helper.ExecuteReader("sp_HeadToHeadResults"))
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

        public RecordsModel GetHeadToHeadRecord(int playerOne, int playerTwo)
        {

            _helper.ClearParameters();
            _helper.AddParameter("@UserOneID", playerOne);
            _helper.AddParameter("@UserTwoID", playerTwo);
            var headToHeadRecordList = new RecordsModel();

            using (var reader = _helper.ExecuteReader("sp_HeadToHeadRecord"))
            {
                while (reader.Read())
                {
                    headToHeadRecordList.TotalMatches = (int)reader["TotalMatches"];
                    headToHeadRecordList.TotalDraws = (int)reader["TotalDraws"];
                    headToHeadRecordList.PlayerOneWins = (int)reader["PlayerOneWins"];
                    headToHeadRecordList.PlayerTwoWins = (int)reader["PlayerTwoWins"];
                }
            }

            return headToHeadRecordList;
        }

    }
}