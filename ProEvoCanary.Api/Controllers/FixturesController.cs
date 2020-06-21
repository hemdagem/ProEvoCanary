using System;
using Microsoft.AspNetCore.Mvc;
using ProEvoCanary.Application.EventHandlers.Events.Commands;
using ProEvoCanary.Application.EventHandlers.Events.Queries;
using ProEvoCanary.Application.EventHandlers.Players.GetPlayers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProEvoCanary.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FixturesController : ControllerBase
	{
		private readonly IGetPlayersQueryHandler _getPlayersQuery;
		private readonly IEventsQueryHandler _eventsQueryHandler;
		private readonly IEventCommandHandler _eventCommand;

		public FixturesController(IGetPlayersQueryHandler getPlayersQuery,
			IEventsQueryHandler eventsQueryHandler,
			IEventCommandHandler eventCommand)
		{
			_getPlayersQuery = getPlayersQuery;
			_eventsQueryHandler = eventsQueryHandler;
			_eventCommand = eventCommand;
		}
		// GET: api/<FixturesController>
		[HttpGet]
		public EventModelDto Get(Guid id)
		{
			var eventQuery = new GetEvent(id);
			var eventModel = _eventsQueryHandler.Handle(eventQuery);
			eventModel.Users = _getPlayersQuery.Handle();

			return eventModel;
		}

		[HttpPut]
		public Guid Put(GenerateFixturesForEventCommand command)
		{
			return  _eventCommand.Handle(command);
		}
	}

}
