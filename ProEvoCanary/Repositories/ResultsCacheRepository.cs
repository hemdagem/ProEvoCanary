using System.Collections.Generic;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Repositories
{
    public class ResultsCacheRepository : ICacheResultsRepository
    {
        private readonly ICacheManager _cacheManager;
        private const string RecentResultsKey = "recent_results";
        private const string HeadToHeadResultsKey = "head_to_head_results_playerOne{0}_playerTwo{1}";
        private const string HeadToHeadRecordsKey = "head_to_head_records_playerOne{0}_playerTwo{1}";

        public ResultsCacheRepository(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public ResultsCacheRepository() : this(new CachingManager()) { }

        public List<ResultsModel> GetResults()
        {
            return _cacheManager.Get(RecentResultsKey) as List<ResultsModel>;
        }

        public List<ResultsModel> GetHeadToHeadResults(int playerOne, int playerTwo)
        {
            var cachedKey = string.Format(HeadToHeadResultsKey, playerOne, playerTwo);
            return _cacheManager.Get(cachedKey) as List<ResultsModel>;
        }

        public RecordsModel GetHeadToHeadRecord(int playerOne, int playerTwo)
        {
            var cachedKey = string.Format(HeadToHeadRecordsKey, playerOne, playerTwo);
            return _cacheManager.Get(cachedKey) as RecordsModel;
        }

        public void AddToCache(string key, object value, int cacheHours)
        {
            _cacheManager.Add(key, value, cacheHours);
        }
    }
}