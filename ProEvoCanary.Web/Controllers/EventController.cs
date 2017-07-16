using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using ProEvoCanary.Domain.Authentication;
using ProEvoCanary.Domain.Repositories.Interfaces;
using ProEvoCanary.Web.Models;
using UserType = ProEvoCanary.Domain.Authentication.UserType;

namespace ProEvoCanary.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IAppUser _currentUser;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        private readonly IResultRepository _resultRepository;

        public EventController(IEventRepository eventRepository, IAppUser currentUser, IPlayerRepository playerRepository, IMapper mapper, IResultRepository resultRepository)
        {
            _eventRepository = eventRepository;
            _currentUser = currentUser;
            _playerRepository = playerRepository;
            _mapper = mapper;
            _resultRepository = resultRepository;
        }

        [AccessAuthorize(UserType.Standard)]
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

        [AccessAuthorize(UserType.Standard)]
        [HttpPost]
        public ActionResult Create(AddEventModel model)
        {
            var eventId = _eventRepository.CreateEvent(model.TournamentName, model.Date, (int)model.TournamentType, _currentUser.CurrentUser.Id);
            return RedirectToAction("GenerateFixtures", "Event",new {id= eventId });
        }

        [AccessAuthorize(UserType.Standard)]
        public ActionResult GenerateFixtures(int id)
        {
            EventModel model = _mapper.Map<EventModel>(_eventRepository.GetEventForEdit(id, _currentUser.CurrentUser.Id));
            model.Users = _mapper.Map<List<PlayerModel>>(_playerRepository.GetAllPlayers());

            return View("GenerateFixtures", model);
        }

        [AccessAuthorize(UserType.Standard)]
        [HttpPost]
        public ActionResult GenerateFixtures(int id, List<int> userIds)
        {
            _eventRepository.AddTournamentUsers(id, userIds);
            _eventRepository.GenerateFixtures(id, userIds);
            return RedirectToAction("Details", "Event", new { Id = id });
        }

        [AccessAuthorize(UserType.Standard)]
        [HttpPost]
        public JsonResult UpdateResult(int eventId,int resultId, ushort homeScore, ushort awayScore)
        {
            var eventModel = _eventRepository.GetEventForEdit(eventId,_currentUser.CurrentUser.Id);

            if (eventModel.Results.FirstOrDefault(x => x.ResultId == resultId) != null)
            {
                var addResult = _resultRepository.AddResult(resultId, homeScore, awayScore);

                return Json(addResult);
            }

           throw new IndexOutOfRangeException("An error occurred");
        }

        [AccessAuthorize(UserType.Admin)]
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
        [AccessAuthorize(UserType.Admin)]
        [HttpPost]
        public ActionResult AdminCreate(AdminEventModel model)
        {
            if (ModelState.IsValid)
            {
                var ownerId = model.OwnerId;

                var createdEvent = _eventRepository.CreateEvent(model.TournamentName, model.Date, (int)model.TournamentType, ownerId);

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