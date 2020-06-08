using System.Collections.Generic;
using AutoMapper;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.Helpers;

namespace ProEvoCanary.Domain.EventHandlers.RssFeeds.GetFeed
{
	public class GetRssFeedQueryHandler : IQueryHandler<RssFeedQuery,List<RssFeedModelDto>>
	{
		private readonly IRssLoader _rssLoader;
		private readonly IMapper _mapper;
		private readonly ICacheManager _cacheManager;

		public GetRssFeedQueryHandler(IRssLoader rssLoader, IMapper mapper, ICacheManager cacheManager)
		{
			_rssLoader = rssLoader;
			_mapper = mapper;
			_cacheManager = cacheManager;
		}

		public List<RssFeedModelDto> Handle(RssFeedQuery query)
		{
			var eventModel =  _cacheManager.AddOrGetExisting(query.Url, () => _rssLoader.Load(query.Url));

			return _mapper.Map<List<RssFeedModelDto>>(eventModel);
		}
	}

}
