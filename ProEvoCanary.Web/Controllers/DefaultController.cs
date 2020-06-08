using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEvoCanary.DataAccess.Repositories.Interfaces;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.EventHandlers.Events.GetEvents;
using ProEvoCanary.Domain.EventHandlers.Players.GetPlayers;
using ProEvoCanary.Web.Models;

namespace ProEvoCanary.Web.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IRssFeedRepository _rssFeedRepository;
        private readonly IQuery<List<EventModelDto>> _eventQueryBase;
        private readonly IQuery<List<PlayerModelDto>> _playerQueryBase;
        private readonly IResultRepository _resultRepository;
        private readonly IMapper _mapper;
        private const string FeedUrl = "http://newsrss.bbc.co.uk/rss/sportonline_uk_edition/football/rss.xml";

        public DefaultController(IQuery<List<PlayerModelDto>> playerQueryBase, IRssFeedRepository rssFeedRepository, IQuery<List<EventModelDto>> eventQueryBase, IResultRepository resultRepository, IMapper mapper)
        {
	        _playerQueryBase = playerQueryBase;
            _rssFeedRepository = rssFeedRepository;
            _eventQueryBase = eventQueryBase;
            _resultRepository = resultRepository;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
	        
            var homeModel = new HomeModel
            {
                Players = _mapper.Map<List<PlayerModel>>(_playerQueryBase.Handle()),
                News = _mapper.Map<List<RssFeedModel>>(_rssFeedRepository.GetFeed(FeedUrl)),
                Events = _mapper.Map<List<EventModel>>(_eventQueryBase.Handle()),
                Results = _mapper.Map<List<ResultsModel>>(_resultRepository.GetResults())
            };
            return View("Index", homeModel);
        }
    }
}
