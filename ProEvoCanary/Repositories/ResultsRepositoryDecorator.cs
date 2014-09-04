using System.Collections.Generic;
using ProEvoCanary.Helpers;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Repositories
{
    public class ResultsRepositoryDecorator : IResultRepository
    {
        private readonly ICacheResultsRepository _cacheResultsRepository;
        private readonly IResultRepository _resultRepository;
        private const string RecentResultsKey = "recent_results";
        private const string headToHeadResultsKey = "{0}_{1}";

        private const int CacheHours = 30;
        public ResultsRepositoryDecorator() : this(new ResultsCacheRepository(), new ResultsRepository()) { }

        public ResultsRepositoryDecorator(ICacheResultsRepository cacheResultsRepository, IResultRepository resultRepository)
        {
            _cacheResultsRepository = cacheResultsRepository;
            _resultRepository = resultRepository;
        }

        public List<ResultsModel> GetResults()
        {
            List<ResultsModel> results = _cacheResultsRepository.GetResults();

            if (results == null)
            {
                results = _resultRepository.GetResults();
                _cacheResultsRepository.AddToCache(RecentResultsKey, results, CacheHours);
            }

            return results;
        }

        public RecordsModel GetHeadToHeadRecord(int playerOne, int playerTwo)
        {

            RecordsModel results = _cacheResultsRepository.GetHeadToHeadRecord(playerOne, playerTwo);

            if (results == null)
            {
                results = _resultRepository.GetHeadToHeadRecord(playerOne, playerTwo);

                var key = string.Format(headToHeadResultsKey, playerOne, playerTwo);

                _cacheResultsRepository.AddToCache(key, results, CacheHours);
            }

            return results;
        }
    }
}