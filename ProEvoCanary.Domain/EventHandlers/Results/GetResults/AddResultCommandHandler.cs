using System.Collections.Generic;
using AutoMapper;
using ProEvoCanary.DataAccess.Repositories.Interfaces;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.Infrastructure;

namespace ProEvoCanary.Domain.EventHandlers.Results.GetResults
{
	public interface IGetResultQueryHandler : IQuery<List<GetResultsModelDto>>
	{

	}
	public class GetResultQueryHandler : IGetResultQueryHandler
	{
		private readonly IResultRepository _resultRepository;
		private readonly IMapper _mapper;
		private readonly ICacheManager _cacheManager;
		const string ResultsKey ="Results";

		public GetResultQueryHandler(IResultRepository resultRepository, IMapper mapper, ICacheManager cacheManager)
		{
			_resultRepository = resultRepository;
			_mapper = mapper;
			_cacheManager = cacheManager;
		}
		public List<GetResultsModelDto> Handle()
		{
			return _mapper.Map<List<GetResultsModelDto>>(_cacheManager.AddOrGetExisting(ResultsKey,() =>_resultRepository.GetResults()));
		}
	}
}
