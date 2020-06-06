using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEvoCanary.Domain.Repositories.Interfaces;
using ProEvoCanary.Web.Models;

namespace ProEvoCanary.Web.Controllers
{
	public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        private readonly IResultRepository _resultRepository;

        public EventController(IEventRepository eventRepository, IPlayerRepository playerRepository, IMapper mapper, IResultRepository resultRepository)
        {
            _eventRepository = eventRepository;
            _playerRepository = playerRepository;
            _mapper = mapper;
            _resultRepository = resultRepository;
        }

        public ActionResult Create()
        {
            return View("Create", new AddEventModel());
        }
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var eventModel = _eventRepository.GetEvent(id);
            eventModel.Standings = _eventRepository.GetStandings(id);
            return View("Details", _mapper.Map<EventModel>(eventModel));
        }

        [HttpPost]
        public ActionResult Create(AddEventModel model)
        {
            var eventId = _eventRepository.CreateEvent(model.TournamentName, model.Date, (int)model.TournamentType);
            return RedirectToAction("GenerateFixtures", "Event",new {id= eventId });
        }

        public ActionResult GenerateFixtures(int id)
        {
            EventModel model = _mapper.Map<EventModel>(_eventRepository.GetEventForEdit(id));
            model.Users = _mapper.Map<List<PlayerModel>>(_playerRepository.GetAllPlayers());

            return View("GenerateFixtures", model);
        }

        [HttpPost]
        public ActionResult GenerateFixtures(int id, List<int> userIds)
        {
            _eventRepository.AddTournamentUsers(id, userIds);
            _eventRepository.GenerateFixtures(id, userIds);
            return RedirectToAction("Details", "Event", new { Id = id });
        }

        [HttpPost]
        public JsonResult UpdateResult(int eventId,int resultId, ushort homeScore, ushort awayScore)
        {
            var eventModel = _eventRepository.GetEventForEdit(eventId);

            if (eventModel.Results.FirstOrDefault(x => x.ResultId == resultId) != null)
            {
                var addResult = _resultRepository.AddResult(resultId, homeScore, awayScore);

                return Json(addResult);
            }

           throw new IndexOutOfRangeException("An error occurred");
        }

        public ActionResult AdminCreate()
        {
            var model = new AdminEventModel
            {
                Players = _mapper.Map<List<PlayerModel>>(_playerRepository.GetAllPlayers()),
                Date = DateTime.Today
            };

            return View("AdminCreate", model);
        }


        // POST: Authentication/Create
        [HttpPost]
        public ActionResult AdminCreate(AdminEventModel model)
        {
            if (ModelState.IsValid)
            {

                var createdEvent = _eventRepository.CreateEvent(model.TournamentName, model.Date, (int)model.TournamentType);

                if (createdEvent > 0)
                {
                    return RedirectToAction("Index", "Default");
                }
            }

            model.Players = _mapper.Map<List<PlayerModel>>(_playerRepository.GetAllPlayers());

            return View(model);
        }
    }
}