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
    [AccessAuthorize(UserType.Standard)]
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

        // GET: Admin/Event
        public ActionResult Create()
        {
            return View("Create", new AddEventModel());
        }

        public ActionResult Details(int id)
        {
            var eventModel = _eventRepository.GetEvent(id);

            return View("Details", _mapper.Map<EventModel>(eventModel));
        }

        // POST: Authentication/Create
        [HttpPost]
        public ActionResult Create(AddEventModel model)
        {
            _eventRepository.CreateEvent(model.TournamentName, model.Date, (int)model.EventType, _currentUser.CurrentUser.Id);
            return RedirectToAction("GenerateFixtures", "Event");
        }

        public ActionResult GenerateFixtures(int id)
        {
            var eventForEdit = _eventRepository.GetEventForEdit(id, _currentUser.CurrentUser.Id);
            EventModel model = new EventModel
            {
                Users = _mapper.Map<List<PlayerModel>>(_playerRepository.GetAllPlayers()),
                Completed = eventForEdit.Completed,
                FixturesGenerated = eventForEdit.FixturesGenerated
            };


            return View("GenerateFixtures", model);
        }

        // POST: Authentication/Create
        [HttpPost]
        public ActionResult GenerateFixtures(int id, List<int> userIds)
        {
            _eventRepository.GenerateFixtures(id, userIds);
            return RedirectToAction("Details", "Event", new { Id = id });
        }
    }
}