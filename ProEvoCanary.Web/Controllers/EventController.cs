using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProEvoCanary.DataAccess.Repositories.Interfaces;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.EventHandlers.Events.AddEvent;
using ProEvoCanary.Domain.EventHandlers.Events.GenerateFixturesForEvent;
using ProEvoCanary.Domain.EventHandlers.Events.GetEvent;
using ProEvoCanary.Domain.EventHandlers.Players.GetPlayers;
using ProEvoCanary.Web.Models;
using EventModel = ProEvoCanary.Web.Models.EventModel;

namespace ProEvoCanary.Web.Controllers
{
	public class EventController : Controller
	{
		private readonly IQuery<List<PlayerModelDto>> _getPlayersQuery;
		private readonly IMapper _mapper;
		private readonly IQueryHandler<GetEvent, EventModelDto> _eventQuery;
		private readonly ICommandHandler<AddEventCommand, Guid> _eventCommand;
		private readonly ICommandHandler<GenerateFixturesForEventCommand, Guid> _geneQueryHandler;

		public EventController(IQuery<List<PlayerModelDto>> getPlayersQuery, 
			IMapper mapper, 
			IQueryHandler<GetEvent, EventModelDto> eventQuery,
			ICommandHandler<AddEventCommand, Guid> eventCommand,
			ICommandHandler<GenerateFixturesForEventCommand, Guid> geneQueryHandler)
		{
			_getPlayersQuery = getPlayersQuery;
			_mapper = mapper;
			_eventQuery = eventQuery;
			_eventCommand = eventCommand;
			_geneQueryHandler = geneQueryHandler;
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
			var model = _eventQuery.Handle(eventQuery);
			return View("Details", _mapper.Map<EventModel>(model));
		}

		public ActionResult GenerateFixtures(Guid id)
		{
			var eventQuery = new GetEvent(id);
			var eventModel = _eventQuery.Handle(eventQuery);
			EventModel model = _mapper.Map<EventModel>(eventModel);
			model.Users = _mapper.Map<List<PlayerModel>>(_getPlayersQuery.Handle());

			return View("GenerateFixtures", model);
		}

		[HttpPost]
		public ActionResult GenerateFixtures(Guid id, List<int> userIds)
		{
			var eventCommand = new GenerateFixturesForEventCommand(id,userIds);
			_geneQueryHandler.Handle(eventCommand);
			return RedirectToAction("Details", "Event", new { Id = id });
		}
	}
}