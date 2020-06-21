using System.Collections.Generic;
using System.Linq;
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
		private readonly ICacheManager _cacheManager;

		public GetRssFeedQueryHandler(IRssRepository rssRepository, ICacheManager cacheManager)
		{
			_rssRepository = rssRepository;
			_cacheManager = cacheManager;
		}

		public List<RssFeedModelDto> Handle(RssFeedQuery query)
		{
			var eventModel =  _cacheManager.AddOrGetExisting(query.Url, () => _rssRepository.Load(query.Url));

			return eventModel.Select(x=> new RssFeedModelDto {LinkTitle = x.LinkTitle,LinkDescription = x.LinkDescription,LinkUrl = x.LinkUrl}).ToList();
		}
	}

}
