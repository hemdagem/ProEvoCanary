using System.Web.Mvc;
using ProEvoCanary.Repositories;
using ProEvoCanary.Repositories.Interfaces;
using ProEvoCanary.Models;

namespace ProEvoCanary.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IRssFeedRepository _rssFeedRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IResultRepository _resultRepository;
        private const string URL = "http://newsrss.bbc.co.uk/rss/sportonline_uk_edition/football/rss.xml";

        public DefaultController(IPlayerRepository playerRepository, IRssFeedRepository rssFeedRepository, IEventRepository eventRepository, IResultRepository resultRepository)
        {
            _playerRepository = playerRepository;
            _rssFeedRepository = rssFeedRepository;
            _eventRepository = eventRepository;
            _resultRepository = resultRepository;
        }

        public DefaultController() : this(new PlayerRepositoryDecorator(), new RssFeedRepositoryDecorator(), new EventRepositoryDecorator(), new ResultsRepository()) { }
        
        [AllowAnonymous]
        public ActionResult Index()
        {
            var homeModel = new HomeModel
            {
                Players = _playerRepository.GetTopPlayers(),
                News = _rssFeedRepository.GetFeed(URL),
                Events = _eventRepository.GetEvents(),
                Results = _resultRepository.GetResults()
            };

            return View("Index", homeModel);
        }

    }
}
