using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEvoCanary.Domain.Repositories.Interfaces;
using ProEvoCanary.Web.Models;

namespace ProEvoCanary.Web.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IRssFeedRepository _rssFeedRepository;
        private readonly IEventReadRepository _eventRepository;
        private readonly IResultRepository _resultRepository;
        private readonly IMapper _mapper;
        private const string FeedUrl = "http://newsrss.bbc.co.uk/rss/sportonline_uk_edition/football/rss.xml";

        public DefaultController(IPlayerRepository playerRepository, IRssFeedRepository rssFeedRepository, IEventReadRepository eventRepository, IResultRepository resultRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _rssFeedRepository = rssFeedRepository;
            _eventRepository = eventRepository;
            _resultRepository = resultRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var homeModel = new HomeModel
            {
                Players = _mapper.Map<List<PlayerModel>>(_playerRepository.GetTopPlayers()),
                News = _mapper.Map<List<RssFeedModel>>(_rssFeedRepository.GetFeed(FeedUrl)),
                Events = _mapper.Map<List<EventModel>>(_eventRepository.GetEvents()),
                Results = _mapper.Map<List<ResultsModel>>(_resultRepository.GetResults())
            };
            return View("Index", homeModel);
        }
    }
}
