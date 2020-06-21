using System;
using System.Collections.Generic;
using System.Linq;
using ProEvoCanary.Application.EventHandlers.Configuration;
using ProEvoCanary.Application.Infrastructure;
using ProEvoCanary.DataAccess.Repositories.Interfaces;

namespace ProEvoCanary.Application.EventHandlers.Results.GetResults
{
	public interface IGetResultsQueryHandler : IQuery<List<GetResultsModelDto>>, IQueryHandler<Guid, GetResultsModelDto>
	{

	}
	public class GetResultsQueryHandler : IGetResultsQueryHandler
	{
		private readonly IResultRepository _resultRepository;
		private readonly ICacheManager _cacheManager;
		const string ResultsKey = "Results";

		public GetResultsQueryHandler(IResultRepository resultRepository, ICacheManager cacheManager)
		{
			_resultRepository = resultRepository;
			_cacheManager = cacheManager;
		}


		public GetResultsModelDto Handle(Guid id)
		{
			var x = _cacheManager.AddOrGetExisting($"ResultId_{id}", () => _resultRepository.GetResult(id));
			return new GetResultsModelDto
			{
				AwayScore = x.AwayScore,
				AwayTeam = x.AwayTeam,
				AwayTeamId = x.AwayTeamId,
				HomeScore = x.HomeScore,
				HomeTeam = x.HomeTeam,
				HomeTeamId = x.HomeTeamId,
				ResultId = x.ResultId,
				TournamentId = x.TournamentId
			};
		}

		public List<GetResultsModelDto> Handle()
		{
			var getResults = _cacheManager.AddOrGetExisting(ResultsKey, () => _resultRepository.GetResults());
			return getResults.Select(x => new GetResultsModelDto
			{
				AwayScore = x.AwayScore,
				AwayTeam = x.AwayTeam,
				AwayTeamId = x.AwayTeamId,
				HomeScore = x.HomeScore,
				HomeTeam = x.HomeTeam,
				HomeTeamId = x.HomeTeamId,
				ResultId = x.ResultId,
				TournamentId = x.TournamentId
			}).ToList();
		}


	}
}
