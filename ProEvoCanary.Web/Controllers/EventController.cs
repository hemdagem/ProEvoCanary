using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.EventHandlers.Events.AddEvent;
using ProEvoCanary.Domain.EventHandlers.Events.GetEvent;
using ProEvoCanary.Domain.Repositories.Interfaces;
using ProEvoCanary.Web.Models;
using EventModel = ProEvoCanary.Web.Models.EventModel;

namespace ProEvoCanary.Web.Controllers
{
	public class EventController : Controller
	{
		private readonly IEventReadRepository _eventRepository;
		private readonly IPlayerRepository _playerRepository;
		private readonly IMapper _mapper;
		private readonly IResultRepository _resultRepository;
		private readonly IEventWriteRepository _eventWriteRepository;
		private readonly IQueryHandlerBase<GetEventQuery, Domain.EventHandlers.Events.GetEvent.EventModelDto> _eventQueryBase;
		private readonly ICommandHandlerBase<AddEventCommand, Guid> _eventCommandBase;

		public EventController(IEventReadRepository eventRepository, IPlayerRepository playerRepository, IMapper mapper, IResultRepository resultRepository, IEventWriteRepository eventWriteRepository, IQueryHandlerBase<GetEventQuery, EventModelDto> eventQueryBase, ICommandHandlerBase<AddEventCommand, Guid> eventCommandBase)
		{
			_eventRepository = eventRepository;
			_playerRepository = playerRepository;
			_mapper = mapper;
			_resultRepository = resultRepository;
			_eventWriteRepository = eventWriteRepository;
			_eventQueryBase = eventQueryBase;
			_eventCommandBase = eventCommandBase;
		}

		public ActionResult Create()
		{
			return View("Create", new AddEventModel());
		}

		[HttpPost]
		public ActionResult Create(AddEventModel model)
		{
			var addEventCommand = new AddEventCommand(model.TournamentName, model.Date);
			var id = _eventCommandBase.Handle(addEventCommand);

			return RedirectToAction("GenerateFixtures", "Event", new { id });
		}

		public ActionResult Details(Guid id)
		{
			var eventQuery = new GetEventQuery(id);
			var model = _eventQueryBase.Handle(eventQuery);
			return View("Details", _mapper.Map<EventModel>(model));
		}

		public ActionResult GenerateFixtures(Guid id)
		{
			var eventQuery = new GetEventQuery(id);
			var eventModel = _eventQueryBase.Handle(eventQuery);
			EventModel model = _mapper.Map<EventModel>(eventModel);
			model.Users = _mapper.Map<List<PlayerModel>>(_playerRepository.GetAllPlayers());

			return View("GenerateFixtures", model);
		}

		[HttpPost]
		public ActionResult GenerateFixtures(Guid id, List<int> userIds)
		{
			_eventWriteRepository.AddTournamentUsers(id, userIds);
			_eventWriteRepository.GenerateFixtures(id, userIds);
			return RedirectToAction("Details", "Event", new { Id = id });
		}

		[HttpPost]
		public JsonResult UpdateResult(Guid eventId, int resultId, ushort homeScore, ushort awayScore)
		{
			var eventModel = _eventRepository.GetEvent(eventId);

			if (eventModel.Results.FirstOrDefault(x => x.ResultId == resultId) != null)
			{
				var addResult = _resultRepository.AddResult(resultId, homeScore, awayScore);

				return Json(addResult);
			}

			throw new IndexOutOfRangeException("An error occurred");
		}
	}
}