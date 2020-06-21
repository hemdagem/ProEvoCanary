using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProEvoCanary.Application.EventHandlers.Events.Queries;
using ProEvoCanary.Application.EventHandlers.Players.GetPlayers;
using ProEvoCanary.Application.EventHandlers.Results.GetResults;
using ProEvoCanary.Application.EventHandlers.RssFeeds.GetFeed;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProEvoCanary.Api.Controllers
{
	public class HomeModel
	{
		public List<PlayerModelDto> Players { get; }
		public List<RssFeedModelDto> News { get; }
		public List<EventModelDto> Events { get; }
		public List<GetResultsModelDto> Results { get; }

		public HomeModel(List<PlayerModelDto> players, List<RssFeedModelDto> news, List<EventModelDto> events, List<GetResultsModelDto> results)
		{
			Players = players;
			News = news;
			Events = events;
			Results = results;
		}
	}

	[Route("api/[controller]")]
	[ApiController]
	public class HomeController : ControllerBase
	{
		private readonly IGetRssFeedQueryHandler _rssFeedQueryHandler;
		private readonly IEventsQueryHandler _eventsQueryHandler;
		private readonly IGetPlayersQueryHandler _playerQueryBase;
		private readonly IGetResultsQueryHandler _resultsQueryHandler;
		private const string FeedUrl = "http://newsrss.bbc.co.uk/rss/sportonline_uk_edition/football/rss.xml";
		public HomeController(IGetPlayersQueryHandler playerQueryBase, IGetRssFeedQueryHandler rssFeedQueryHandler, IEventsQueryHandler eventsQueryHandler, IGetResultsQueryHandler resultsQueryHandler)
		{
			_playerQueryBase = playerQueryBase;
			_rssFeedQueryHandler = rssFeedQueryHandler;
			_eventsQueryHandler = eventsQueryHandler;
			_resultsQueryHandler = resultsQueryHandler;
		}
		// GET: api/<HomeController>
		[HttpGet]
		public HomeModel Get()
		{
			return new HomeModel(_playerQueryBase.Handle(), _rssFeedQueryHandler.Handle(new RssFeedQuery(FeedUrl)), _eventsQueryHandler.Handle(), _resultsQueryHandler.Handle());
		}
	}

}
