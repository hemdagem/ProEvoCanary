using System.Collections.Generic;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Models;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Domain.Repositories
{
    public class ResultsRepositoryDecorator : IResultRepository
    {
        private readonly ICacheManager _cacheResultsRepository;
        private readonly IResultRepository _resultRepository;
        private const string RecentResultsKey = "recent_results";
        private const string HeadToHeadResultsKey = "{0}_{1}";
        private const int CacheHours = 30;

        public ResultsRepositoryDecorator(ICacheManager cacheResultsRepository, IResultRepository resultRepository)
        {
            _cacheResultsRepository = cacheResultsRepository;
            _resultRepository = resultRepository;
        }

        public List<ResultsModel> GetResults()
        {
	        return _cacheResultsRepository.AddOrGetExisting(RecentResultsKey, () => _resultRepository.GetResults());
        }

        public RecordsModel GetHeadToHeadRecord(int playerOne, int playerTwo)
        {
	        return _cacheResultsRepository.AddOrGetExisting(string.Format(HeadToHeadResultsKey, playerOne, playerTwo), () => _resultRepository.GetHeadToHeadRecord(playerOne,playerTwo));
        }

        public int AddResult(int id, int homeScore, int awayScore)
        {
            return _resultRepository.AddResult(id, homeScore, awayScore);
        }

        public ResultsModel GetResult(int id)
        {
	        return _cacheResultsRepository.AddOrGetExisting(id.ToString(), () => _resultRepository.GetResult(id));
        }
    }
}