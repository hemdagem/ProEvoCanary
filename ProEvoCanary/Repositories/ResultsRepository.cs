using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using ProEvoCanary.Helpers;
using ProEvoCanary.Repositories.Interfaces;
using ProEvoCanary.Models;

namespace ProEvoCanary.Repositories
{
    public class ResultsRepository : IResultRepository
    {
        private readonly IDBHelper _helper;
        private readonly MemoryCache _memoryCache;
        private const string RecentResultsKey = "recent_results";
        private const string HeadToHeadResultsKey = "head_to_head_results_playerOne{0}_playerTwo{1}";
        private const string HeadToHeadRecordsKey = "head_to_head_records_playerOne{0}_playerTwo{1}";

        private readonly CacheItemPolicy policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddHours(3)
        };

        public ResultsRepository(IDBHelper helper, MemoryCache memoryCache)
        {
            _helper = helper;
            _memoryCache = memoryCache;
        }

        public ResultsRepository() : this(new DBHelper(), MemoryCache.Default) { }

        public List<ResultsModel> GetResults()
        {
            var results = GetCachedItem(RecentResultsKey) as List<ResultsModel>;
            if (results != null) return results;

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
            _memoryCache.Add(RecentResultsKey, lstResults, policy);
            return lstResults;
        }

        public List<ResultsModel> GetHeadToHeadResults(int playerOne, int playerTwo)
        {
            var cachedKey = string.Format(HeadToHeadResultsKey, playerOne, playerTwo);
            var lstResults = GetCachedItem(cachedKey) as List<ResultsModel>;

            if (lstResults != null)
            {
                return lstResults;
            }

            lstResults = new List<ResultsModel>();

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

            _memoryCache.Add(cachedKey, lstResults, policy);

            return lstResults;
        }

        public RecordsModel GetHeadToHeadRecord(int playerOne, int playerTwo)
        {
            var cachedKey = string.Format(HeadToHeadRecordsKey, playerOne, playerTwo);

            if (_memoryCache.Contains(cachedKey))
            {
                return _memoryCache.Get(cachedKey) as RecordsModel;
            }

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

            _memoryCache.Add(cachedKey, headToHeadRecordList, policy);

            return headToHeadRecordList;
        }


        private object GetCachedItem(string key)
        {
            return _memoryCache.Contains(key) ? _memoryCache.Get(key) : null;
        }
    }
}