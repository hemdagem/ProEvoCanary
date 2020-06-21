using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProEvoCanary.Application.EventHandlers.Results.AddResult;
using ProEvoCanary.Application.EventHandlers.Results.GetResults;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProEvoCanary.Api.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class ResultsController : ControllerBase
	{
		private readonly IGetResultsQueryHandler _resultsQueryHandler;
		private readonly IAddResultCommandHandler _addResultCommandHandler;

		public ResultsController(IGetResultsQueryHandler resultsQueryHandler, IAddResultCommandHandler addResultCommandHandler)
		{
			_resultsQueryHandler = resultsQueryHandler;
			_addResultCommandHandler = addResultCommandHandler;
		}
		// GET: api/<HomeController>
		[HttpGet]
		public GetResultsModelDto Get(Guid id)
		{
			return _resultsQueryHandler.Handle(id);
		}

		[HttpGet]
		public List<GetResultsModelDto> Get()
		{
			return _resultsQueryHandler.Handle();
		}

		[HttpPut]
		public Guid Put(GetResultsModelDto result)
		{
			return _addResultCommandHandler.Handle(new AddResultCommand(result.ResultId,result.HomeScore,result.AwayScore));
		}
	}

}
