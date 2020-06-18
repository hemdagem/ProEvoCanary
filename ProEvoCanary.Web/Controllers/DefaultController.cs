using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProEvoCanary.Domain.EventHandlers.Events.Queries;
using ProEvoCanary.Domain.EventHandlers.Players.GetPlayers;
using ProEvoCanary.Domain.EventHandlers.Results.GetResults;
using ProEvoCanary.Domain.EventHandlers.RssFeeds.GetFeed;
using ProEvoCanary.Web.Models;

namespace ProEvoCanary.Web.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IGetRssFeedQueryHandler _rssFeedQueryHandler;
        private readonly IEventsQueryHandler _eventsQueryHandler;
        private readonly IGetPlayersQueryHandler _playerQueryBase;
        private readonly IGetResultsQueryHandler _resultsQueryHandler;
        private readonly IMapper _mapper;
        private const string FeedUrl = "http://newsrss.bbc.co.uk/rss/sportonline_uk_edition/football/rss.xml";

        public DefaultController(IGetPlayersQueryHandler playerQueryBase, IGetRssFeedQueryHandler rssFeedQueryHandler, IEventsQueryHandler eventsQueryHandler, IGetResultsQueryHandler resultsQueryHandler, IMapper mapper)
        {
	        _playerQueryBase = playerQueryBase;
            _rssFeedQueryHandler = rssFeedQueryHandler;
            _eventsQueryHandler = eventsQueryHandler;
            _resultsQueryHandler = resultsQueryHandler;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
	        
            var homeModel = new HomeModel
            {
                Players = _mapper.Map<List<Models.PlayerModel>>(_playerQueryBase.Handle()),
                News = _mapper.Map<List<RssFeedModel>>(_rssFeedQueryHandler.Handle(new RssFeedQuery(FeedUrl))),
                Events = _mapper.Map<List<EventModel>>(_eventsQueryHandler.Handle()),
                Results = _mapper.Map<List<Models.ResultsModel>>(_resultsQueryHandler.Handle())
            };
            return View("Index", homeModel);
        }
    }
}
