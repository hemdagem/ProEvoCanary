using System;
using System.Linq;
using System.Web.Mvc;
using ProEvoCanary.Authentication;
using ProEvoCanary.Domain.Repositories.Interfaces;
using ProEvoCanary.ModelBuilders;
using ProEvoCanary.Models;
using EventModel = ProEvoCanary.Areas.Admin.Models.EventModel;

namespace ProEvoCanary.Areas.Admin.Controllers
{
    [AccessAuthorize(UserType.Admin)]
    public class EventController : Controller
    {
        private readonly IAdminEventRepository _eventRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IPlayerModelBuilder _playerModelBuilder;

        public EventController(IAdminEventRepository eventRepository, IPlayerRepository playerRepository, IPlayerModelBuilder playerModelBuilder)
        {
            _eventRepository = eventRepository;
            _playerRepository = playerRepository;
            _playerModelBuilder = playerModelBuilder;
        }

        // GET: Admin/Event
        public ActionResult Create()
        {
            
            var model = new EventModel { Players = _playerRepository.GetAllPlayers()
                .Select(x=> _playerModelBuilder.BuildViewModel(x)).ToList(), Date = DateTime.Today };
            return View("Create", model);
        }

        // POST: Authentication/Create
        [HttpPost]
        public ActionResult Create(EventModel model)
        {
            if (ModelState.IsValid)
            {
                var ownerId = model.OwnerId;

                var createdEvent = _eventRepository.CreateEvent(model.TournamentName, model.Date, (int)model.EventType, ownerId);

                if (createdEvent > 0)
                {
                    return RedirectToAction("Index", "Default");
                }
            }

            model.Players = _playerRepository.GetAllPlayers().Select(x => _playerModelBuilder.BuildViewModel(x)).ToList();

            return View(model);
        }

    }
}