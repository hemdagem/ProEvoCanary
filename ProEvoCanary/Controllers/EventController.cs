using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using ProEvoCanary.Authentication;
using ProEvoCanary.Domain.Repositories.Interfaces;
using ProEvoCanary.Models;
using UserType = ProEvoCanary.Authentication.UserType;

namespace ProEvoCanary.Controllers
{
    
    public class EventController : Controller
    {
        private readonly IAdminEventRepository _eventRepository;
        private readonly IAppUser _currentUser;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public EventController(IAdminEventRepository eventRepository, IAppUser currentUser, IPlayerRepository playerRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _currentUser = currentUser;
            _playerRepository = playerRepository;
            _mapper = mapper;
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
            return View("Details", _mapper.Map<EventModel>(eventModel));
        }

        [AccessAuthorize(UserType.Standard)]
        [HttpPost]
        public ActionResult Create(AddEventModel model)
        {
            _eventRepository.CreateEvent(model.TournamentName, model.Date, (int)model.EventType, _currentUser.CurrentUser.Id);
            return RedirectToAction("GenerateFixtures", "Event");
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
            _eventRepository.GenerateFixtures(id, userIds);
            return RedirectToAction("Details", "Event", new { Id = id });
        }
    }
}