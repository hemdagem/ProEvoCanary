using System.Collections.Generic;
using AutoMapper;
using ProEvoCanary.Application.EventHandlers.Configuration;
using ProEvoCanary.Application.Infrastructure;
using ProEvoCanary.DataAccess.Repositories.Interfaces;

namespace ProEvoCanary.Application.EventHandlers.RssFeeds.GetFeed
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
