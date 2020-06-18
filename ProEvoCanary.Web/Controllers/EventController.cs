using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProEvoCanary.Domain.EventHandlers.Events.Commands;
using ProEvoCanary.Domain.EventHandlers.Events.Queries;
using ProEvoCanary.Domain.EventHandlers.Players.GetPlayers;
using ProEvoCanary.Web.Models;
using EventModel = ProEvoCanary.Web.Models.EventModel;

namespace ProEvoCanary.Web.Controllers
{
	public class EventController : Controller
	{
		private readonly IGetPlayersQueryHandler _getPlayersQuery;
		private readonly IMapper _mapper;
		private readonly IEventsQueryHandler _eventsQueryHandler;
		private readonly IEventCommandHandler _eventCommand;

		public EventController(IGetPlayersQueryHandler getPlayersQuery, 
			IMapper mapper,
			IEventsQueryHandler eventsQueryHandler,
			IEventCommandHandler eventCommand)
		{
			_getPlayersQuery = getPlayersQuery;
			_mapper = mapper;
			_eventsQueryHandler = eventsQueryHandler;
			_eventCommand = eventCommand;
		}

		public ActionResult Create()
		{
			return View("Create", new AddEventModel());
		}

		[HttpPost]
		public ActionResult Create(AddEventModel model)
		{
			var addEventCommand = new AddEventCommand(model.TournamentName, model.Date);
			var id = _eventCommand.Handle(addEventCommand);

			return RedirectToAction("GenerateFixtures", "Event", new { id });
		}

		public ActionResult Details(Guid id)
		{
			var eventQuery = new GetEvent(id);
			var model = _eventsQueryHandler.Handle(eventQuery);
			return View("Details", _mapper.Map<EventModel>(model));
		}

		public ActionResult GenerateFixtures(Guid id)
		{
			var eventQuery = new GetEvent(id);
			var eventModel = _eventsQueryHandler.Handle(eventQuery);
			EventModel model = _mapper.Map<EventModel>(eventModel);
			var playerModelDtos = _getPlayersQuery.Handle();
			model.Users = _mapper.Map<List<Models.PlayerModel>>(playerModelDtos);

			return View("GenerateFixtures", model);
		}

		[HttpPost]
		public ActionResult GenerateFixtures(Guid id, List<int> userIds)
		{
			var eventCommand = new GenerateFixturesForEventCommand(id,userIds);
			_eventCommand.Handle(eventCommand);
			return RedirectToAction("Details", "Event", new { Id = id });
		}
	}
}