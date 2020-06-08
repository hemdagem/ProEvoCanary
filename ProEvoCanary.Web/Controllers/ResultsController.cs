using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProEvoCanary.DataAccess.Repositories.Interfaces;

namespace ProEvoCanary.Web.Controllers
{
	public class ResultsController : Controller
	{
		private readonly IResultRepository _resultRepository;
		private readonly IMapper _mapper;

		public ResultsController(IResultRepository resultRepository, IMapper mapper)
		{
			_resultRepository = resultRepository;
			_mapper = mapper;
		}

		// GET: Results
		public ActionResult Update(Guid id)
		{
			var resultsModel = _mapper.Map<Models.ResultsModel>(_resultRepository.GetResult(id));
			return View(resultsModel);
		}

		[HttpPost]
		public ActionResult Update(Models.ResultsModel model)
		{
			_resultRepository.AddResult(model.ResultId, model.HomeScore, model.AwayScore);

			return RedirectToAction("Details", "Event", new { id = model.EventId });

		}
	}
}