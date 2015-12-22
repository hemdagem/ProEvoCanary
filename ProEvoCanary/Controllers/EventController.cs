using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProEvoCanary.Authentication;
using ProEvoCanary.Domain.Repositories.Interfaces;
using ProEvoCanary.Models;

namespace ProEvoCanary.Controllers
{
    [AccessAuthorize(UserType.Standard)]
    public class EventController : Controller
    {
        private readonly IAdminEventRepository _eventRepository;
        private readonly IAppUser _currentUser;
        private readonly IPlayerRepository _playerRepository;

        public EventController(IAdminEventRepository eventRepository, IAppUser currentUser, IPlayerRepository playerRepository)
        {
            _eventRepository = eventRepository;
            _currentUser = currentUser;
            _playerRepository = playerRepository;
        }

        // GET: Admin/Event
        public ActionResult Create()
        {
            return View("Create", new AddEventModel());
        }

        public ActionResult Details(int id)
        {
            var eventModel = _eventRepository.GetEvent(id);

            return View("Details", eventModel);
        }

        // POST: Authentication/Create
        [HttpPost]
        public ActionResult Create(AddEventModel model)
        {
            _eventRepository.CreateEvent(model.TournamentName, model.Date, (int)model.EventType, _currentUser.CurrentUser.Id);
            return RedirectToAction("GenerateFixtures", "Event");
        }

        public ActionResult GenerateFixtures(int id)
        {
            EventModel model = new EventModel(); //_eventRepository.GetEventForEdit(id, _currentUser.CurrentUser.Id);
            model.Users = _playerRepository.GetAllPlayers().Select(x => new PlayerModel { GoalsPerGame = x.GoalsPerGame, MatchesPlayed = x.MatchesPlayed, PlayerId = x.PlayerId, PlayerName = x.PlayerName, PointsPerGame = x.PointsPerGame }).ToList();
            ;

            return View("GenerateFixtures", model);
        }

        // POST: Authentication/Create
        [HttpPost]
        public ActionResult GenerateFixtures(int id, List<int> userIds)
        {
            _eventRepository.GenerateFixtures(id, userIds);
            return RedirectToAction("Details", "Event", new { Id = id });
        }
    }
}