using System.Collections.Generic;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Repositories
{
    public class ResultsCacheRepository : ICacheResultsRepository
    {
        private readonly ICache _cache;
        private const string RecentResultsKey = "recent_results";
        private const string HeadToHeadResultsKey = "head_to_head_results_playerOne{0}_playerTwo{1}";
        private const string HeadToHeadRecordsKey = "head_to_head_records_playerOne{0}_playerTwo{1}";

        public ResultsCacheRepository(ICache cache)
        {
            _cache = cache;
        }

        public ResultsCacheRepository() : this(new CachingManager()) { }

        public List<ResultsModel> GetResults()
        {
            return _cache.Get(RecentResultsKey) as List<ResultsModel>;
        }

        public List<ResultsModel> GetHeadToHeadResults(int playerOne, int playerTwo)
        {
            var cachedKey = string.Format(HeadToHeadResultsKey, playerOne, playerTwo);
            return _cache.Get(cachedKey) as List<ResultsModel>;
        }

        public RecordsModel GetHeadToHeadRecord(int playerOne, int playerTwo)
        {
            var cachedKey = string.Format(HeadToHeadRecordsKey, playerOne, playerTwo);
            return _cache.Get(cachedKey) as RecordsModel;
        }

        public void AddToCache(string key, object value, int cacheHours)
        {
            _cache.Add(key, value, cacheHours);
        }
    }
}