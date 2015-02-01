using System;
using System.Web.Mvc;
using ProEvoCanary.Helpers;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Controllers
{
    [AccessAuthorize(UserType.Standard)]
    public class EventController : AppController
    {
        private readonly IAdminEventRepository _eventRepository;

        public EventController(IAdminEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
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
                var createdEvent = _eventRepository.CreateEvent(model.TournamentName, model.Date, model.EventType, CurrentUser.Id);

                if (createdEvent > 0)
                {
                    return RedirectToAction("Index", "Default");
                }
            }

            return View(model);
        }

    }
}