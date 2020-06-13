using System.Collections.Generic;
using AutoMapper;
using ProEvoCanary.DataAccess.Repositories;
using ProEvoCanary.DataAccess.Repositories.Interfaces;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.Infrastructure;

namespace ProEvoCanary.Domain.EventHandlers.RssFeeds.GetFeed
{
	public interface IGetRssFeedQueryHandler : IQueryHandler<RssFeedQuery, List<RssFeedModelDto>>
	{

	}
	public class GetRssFeedQueryHandler : IGetRssFeedQueryHandler
	{
		private readonly IRssRepository _rssRepository;
		private readonly IMapper _mapper;
		private readonly ICacheManager _cacheManager;

		public GetRssFeedQueryHandler(IRssRepository rssRepository, IMapper mapper, ICacheManager cacheManager)
		{
			_rssRepository = rssRepository;
			_mapper = mapper;
			_cacheManager = cacheManager;
		}

		public List<RssFeedModelDto> Handle(RssFeedQuery query)
		{
			var eventModel =  _cacheManager.AddOrGetExisting(query.Url, () => _rssRepository.Load(query.Url));

			return _mapper.Map<List<RssFeedModelDto>>(eventModel);
		}
	}

}
