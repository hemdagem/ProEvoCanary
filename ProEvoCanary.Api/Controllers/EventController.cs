using System;
using Microsoft.AspNetCore.Mvc;
using ProEvoCanary.Application.EventHandlers.Events.Commands;
using ProEvoCanary.Application.EventHandlers.Events.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProEvoCanary.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EventController : ControllerBase
	{
		private readonly IEventsQueryHandler _eventsQueryHandler;
		private readonly IEventCommandHandler _eventCommand;

		public EventController(IEventsQueryHandler eventsQueryHandler,
			IEventCommandHandler eventCommand)
		{
			_eventsQueryHandler = eventsQueryHandler;
			_eventCommand = eventCommand;
		}
		// GET: api/<HomeController>
		[HttpGet]
		public EventModelDto Get(Guid id)
		{
			var eventQuery = new GetEvent(id);
			var model = _eventsQueryHandler.Handle(eventQuery);
			return model;
		}

		[HttpPut]
		public Guid Put([FromBody]EventModelDto model)
		{
			return  _eventCommand.Handle(new AddEventCommand(model.TournamentName, DateTime.Parse(model.Date)));
		}
	}

}
