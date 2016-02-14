using System.Collections.Generic;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Models;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Domain.Repositories
{
    public class ResultsCacheRepository : ICacheResultsRepository
    {
        private readonly ICacheManager _cacheManager;
        private const string RecentResultsKey = "recent_results";
        private const string HeadToHeadRecordsKey = "{0}_{1}";

        public ResultsCacheRepository(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public List<ResultsModel> GetResults()
        {
            return _cacheManager.Get(RecentResultsKey) as List<ResultsModel>;
        }

        public RecordsModel GetHeadToHeadRecord(int playerOne, int playerTwo)
        {
            var cachedKey = string.Format(HeadToHeadRecordsKey, playerOne, playerTwo);
            return _cacheManager.Get(cachedKey) as RecordsModel;
        }

        public int AddResult(int id, int homeScore, int awayScore)
        {
            throw new System.NotImplementedException();
        }

        public void AddToCache(string key, object value, int cacheHours)
        {
            _cacheManager.Add(key, value, cacheHours);
        }
    }
}