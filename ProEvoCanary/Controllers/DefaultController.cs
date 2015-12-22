using System.Linq;
using System.Web.Mvc;
using ProEvoCanary.Domain.Repositories.Interfaces;
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

        [AllowAnonymous]
        public ActionResult Index()
        {
            var homeModel = new HomeModel
            {
                Players = _playerRepository.GetTopPlayers().Select(x => new PlayerModel { GoalsPerGame = x.GoalsPerGame, MatchesPlayed = x.MatchesPlayed, PlayerId = x.PlayerId, PlayerName = x.PlayerName, PointsPerGame = x.PointsPerGame }).ToList()
,
                News = _rssFeedRepository.GetFeed(URL).Select(x=> new RssFeedModel()).ToList(),
                Events = _eventRepository.GetEvents().Select(x=> new EventModel()).ToList(),
                Results = _resultRepository.GetResults().Select(x=> new ResultsModel()).ToList()
            };
            return View("Index", homeModel);
        }
    }
}
