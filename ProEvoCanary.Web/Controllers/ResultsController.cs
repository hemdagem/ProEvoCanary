using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Web.Controllers
{
    public class ResultsController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IResultRepository _resultRepository;
        private readonly IMapper _mapper;

        public ResultsController(IEventRepository eventRepository, IResultRepository resultRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _resultRepository = resultRepository;
            _mapper = mapper;
        }

        // GET: Results
        public ActionResult Update(int id)
        {
            var resultsModel = _mapper.Map<Models.ResultsModel>(_resultRepository.GetResult(id));
            return View(resultsModel);
        }

        [HttpPost]
        public ActionResult Update(Models.ResultsModel model)
        {
            var eventModel = _eventRepository.GetEventForEdit(model.EventId);

            if (eventModel.Results.FirstOrDefault(x => x.ResultId == model.ResultId) != null)
            {
                _resultRepository.AddResult(model.ResultId, model.HomeScore, model.AwayScore);

                return RedirectToAction("Details", "Event", new {id = model.EventId });
            }

            throw new UnauthorizedAccessException("An error occurred");
        }
    }
}