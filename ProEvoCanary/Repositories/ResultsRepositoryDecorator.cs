using System.Collections.Generic;
using ProEvoCanary.Helpers;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Repositories
{
    public class ResultsRepositoryDecorator : ICacheResultsRepository
    {
        private readonly ICacheResultsRepository _cacheResultsRepository;
        private readonly IResultRepository _resultRepository;
        private readonly ICacheManager _cacheManager;
        private const string EventsListCacheKey = "EventsListCache";

        public ResultsRepositoryDecorator() : this(new ResultsCacheRepository(),new ResultsRepository() ) { }

        public ResultsRepositoryDecorator(ICacheResultsRepository cacheResultsRepository, IResultRepository resultRepository)
        {
            _cacheResultsRepository = cacheResultsRepository;
            _resultRepository = resultRepository;
        }


        public List<ResultsModel> GetResults()
        {
            throw new System.NotImplementedException();
        }

        public List<ResultsModel> GetHeadToHeadResults(int playerOne, int playerTwo)
        {
            throw new System.NotImplementedException();
        }

        public RecordsModel GetHeadToHeadRecord(int playerOne, int playerTwo)
        {
            throw new System.NotImplementedException();
        }

        public void AddToCache(string key, object value, int cacheHours)
        {
            throw new System.NotImplementedException();
        }
    }
}