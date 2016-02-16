using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using ProEvoCanary.Domain.Authentication;
using ProEvoCanary.Domain.Repositories.Interfaces;
using EventModel = ProEvoCanary.AdminWeb.Models.EventModel;

namespace ProEvoCanary.AdminWeb.Controllers
{
    [AccessAuthorize(UserType.Admin)]
    public class EventController : Controller
    {
        private readonly IAdminEventRepository _eventRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public EventController(IAdminEventRepository eventRepository, IPlayerRepository playerRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        // GET: Admin/Event
        public ActionResult Create()
        {
            var model = new EventModel
            {
                Players = _mapper.Map<List<Models.PlayerModel>>(_playerRepository.GetAllPlayers()),
                Date = DateTime.Today
            };

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

            model.Players = _mapper.Map<List<Models.PlayerModel>>(_playerRepository.GetAllPlayers());

            return View(model);
        }
    }
}
