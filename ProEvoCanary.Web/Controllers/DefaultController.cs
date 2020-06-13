using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProEvoCanary.DataAccess.Repositories.Interfaces;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.EventHandlers.Events.Queries;
using ProEvoCanary.Domain.EventHandlers.Players.GetPlayers;
using ProEvoCanary.Domain.EventHandlers.Results.AddResult;
using ProEvoCanary.Domain.EventHandlers.Results.GetResults;
using ProEvoCanary.Domain.EventHandlers.RssFeeds.GetFeed;
using ProEvoCanary.Web.Models;

namespace ProEvoCanary.Web.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IGetRssFeedQueryHandler _rssFeedQueryHandler;
        private readonly IEventsQueryHandler _eventQueryBase;
        private readonly IGetPlayersQueryHandler _playerQueryBase;
        private readonly IGetResultQueryHandler _resultRepository;
        private readonly IMapper _mapper;
        private const string FeedUrl = "http://newsrss.bbc.co.uk/rss/sportonline_uk_edition/football/rss.xml";

        public DefaultController(IGetPlayersQueryHandler playerQueryBase, IGetRssFeedQueryHandler rssFeedQueryHandler, IEventsQueryHandler eventQueryBase, IGetResultQueryHandler resultRepository, IMapper mapper)
        {
	        _playerQueryBase = playerQueryBase;
            _rssFeedQueryHandler = rssFeedQueryHandler;
            _eventQueryBase = eventQueryBase;
            _resultRepository = resultRepository;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
	        
            var homeModel = new HomeModel
            {
                Players = _mapper.Map<List<PlayerModel>>(_playerQueryBase.Handle()),
                News = _mapper.Map<List<RssFeedModel>>(_rssFeedQueryHandler.Handle(new RssFeedQuery(FeedUrl))),
                Events = _mapper.Map<List<EventModel>>(_eventQueryBase.Handle()),
                Results = _mapper.Map<List<ResultsModel>>(_resultRepository.Handle())
            };
            return View("Index", homeModel);
        }
    }
}
