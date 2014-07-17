using System.Runtime.Caching;
using System.Web.Mvc;
using ProEvo45.Helpers;
using ProEvo45.Models;
using ProEvo45.Repositories;
using ProEvo45.Repositories.Interfaces;
using ProEvo45Tests;

namespace ProEvo45.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IRssFeedRepository _rssFeedRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IResultRepository _resultRepository;
        private readonly MemoryCache _memoryCache;
        private readonly ILoader _loader;
        private const string url = "http://newsrss.bbc.co.uk/rss/sportonline_uk_edition/football/rss.xml";

        public DefaultController(IPlayerRepository playerRepository, IRssFeedRepository rssFeedRepository, IEventRepository eventRepository, IResultRepository resultRepository, MemoryCache memoryCache, ILoader loader)
        {
            _playerRepository = playerRepository;
            _rssFeedRepository = rssFeedRepository;
            _eventRepository = eventRepository;
            _resultRepository = resultRepository;
            _memoryCache = memoryCache;
            _loader = loader;
        }

        public DefaultController()
            : this(new PlayerRepository(), new RssFeedRepository(), new EventRepository(), new ResultsRepository(), MemoryCache.Default, new Loader())
        {

        }

        // GET: Default
        public ActionResult Index()
        {
            var homeModel = new HomeModel
            {
                Players = _playerRepository.GetPlayers(),
                News = _rssFeedRepository.GetFeed(url, _memoryCache, _loader),
                Events = _eventRepository.GetEvents(),
                Results = _resultRepository.GetResults()
            };

            return View("Index", homeModel);
        }

    }
}
