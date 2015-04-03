using System.Collections.Generic;
using System.Web.Mvc;
using ProEvoCanary.Helpers;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

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

        // POST: Authentication/Create
        [HttpPost]
        public ActionResult Create(AddEventModel model)
        {
            _eventRepository.CreateEvent(model.TournamentName, model.Date, model.EventType, _currentUser.CurrentUser.Id);
            return RedirectToAction("GenerateFixtures", "Event");
        }

        public ActionResult GenerateFixtures(int eventId)
        {
            EventModel model = _eventRepository.GetEvent(eventId, _currentUser.CurrentUser.Id);
            model.Users = _playerRepository.GetAllPlayers();

            return View("GenerateFixtures", model);
        }

        // POST: Authentication/Create
        [HttpPost]
        public ActionResult GenerateFixtures(int eventId, List<int> userIds)
        {
            _eventRepository.GenerateFixtures(eventId, userIds);
            return RedirectToAction("Details", "Event", new { EventId = eventId });
        }
    }
}