using System;
using System.Web.Mvc;
using ProEvoCanary.Helpers;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories;
using ProEvoCanary.Repositories.Interfaces;
using EventModel = ProEvoCanary.Areas.Admin.Models.EventModel;

namespace ProEvoCanary.Controllers
{
    [AccessAuthorize(UserType.Standard)]
    public class EventController : Controller
    {
        private readonly IAdminEventRepository _eventRepository;
        private readonly IPlayerRepository _userRepository;

        public EventController(IAdminEventRepository eventRepository, IPlayerRepository userRepository)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        public EventController() : this(new AdminEventRepository(), new PlayerRepositoryDecorator()) { }

        // GET: Admin/Event
        public ActionResult Create()
        {
            var model = new EventModel { UserSelectListModel = _userRepository.GetAllPlayers(), Date = DateTime.Today };

            return View("Create", model);
        }


        // POST: Authentication/Create
        [HttpPost]
        public ActionResult Create(EventModel model)
        {
            if (ModelState.IsValid)
            {
                var ownerId = int.Parse(model.UserSelectListModel.SelectedItem);

                var createdEvent = _eventRepository.CreateEvent(model.TournamentName, model.Date, model.EventType, ownerId);

                if (createdEvent > 0)
                {
                    return RedirectToAction("Index", "Default");
                }

            }

            model.UserSelectListModel = _userRepository.GetAllPlayers();

            return View(model);
        }

    }
}