using System;
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

        public EventController(IAdminEventRepository eventRepository, IAppUser currentUser)
        {
            _eventRepository = eventRepository;
            _currentUser = currentUser;
        }

        // GET: Admin/Event
        public ActionResult Create()
        {
            var model = new AddEventModel { Date = DateTime.Today };
            return View("Create", model);
        }

        // POST: Authentication/Create
        [HttpPost]
        public ActionResult Create(AddEventModel model)
        {
            if (ModelState.IsValid)
            {
                int createdEvent = _eventRepository.CreateEvent(model.TournamentName, model.Date, model.EventType, _currentUser.CurrentUser.Id);

                if (createdEvent > 0)
                {
                    return RedirectToAction("GenerateFixtures", "Event");
                }
            }

            return View(model);
        }

        public ActionResult GenerateFixtures(int eventId)
        {
            if (eventId < 0)
                throw new IndexOutOfRangeException();

            EventModel model = _eventRepository.GetEvent(eventId);

            if (model == null || (_currentUser.CurrentUser.Id != model.OwnerId))
                throw new NullReferenceException();

            return View("GenerateFixtures", model);
        }
    }
}