using System;
using System.Web.Mvc;
using ProEvoCanary.Helpers;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;
using EventModel = ProEvoCanary.Areas.Admin.Models.EventModel;

namespace ProEvoCanary.Areas.Admin.Controllers
{
    [AccessAuthorize(UserType.Admin)]
    public class EventController : Controller
    {
        private readonly IAdminEventRepository _eventRepository;
        private readonly IPlayerRepository _playerRepository;

        public EventController(IAdminEventRepository eventRepository, IPlayerRepository playerRepository)
        {
            _eventRepository = eventRepository;
            _playerRepository = playerRepository;
        }

        // GET: Admin/Event
        public ActionResult Create()
        {
            var model = new EventModel { Players = _playerRepository.GetAllPlayers(), Date = DateTime.Today };
            return View("Create", model);
        }

        // POST: Authentication/Create
        [HttpPost]
        public ActionResult Create(EventModel model)
        {
            if (ModelState.IsValid)
            {
                var ownerId = model.OwnerId;

                var createdEvent = _eventRepository.CreateEvent(model.TournamentName, model.Date, model.EventType, ownerId);

                if (createdEvent > 0)
                {
                    return RedirectToAction("Index", "Default");
                }
            }

            model.Players = _playerRepository.GetAllPlayers();

            return View(model);
        }

    }
}