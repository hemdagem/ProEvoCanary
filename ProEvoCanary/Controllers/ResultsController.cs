using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using ProEvoCanary.Domain.Authentication;
using ProEvoCanary.Domain.Models;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Controllers
{
    public class ResultsController : Controller
    {
        private readonly IAdminEventRepository _eventRepository;
        private readonly IResultRepository _resultRepository;
        private readonly IAppUser _currentUser;
        private readonly IMapper _mapper;

        public ResultsController(IAdminEventRepository eventRepository, IResultRepository resultRepository, IAppUser appUser, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _resultRepository = resultRepository;
            _currentUser = appUser;
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
            var eventModel = _eventRepository.GetEventForEdit(model.EventId, _currentUser.CurrentUser.Id);

            if (eventModel.Results.FirstOrDefault(x => x.ResultId == model.ResultId) != null)
            {
                _resultRepository.AddResult(model.ResultId, model.HomeScore, model.AwayScore);

                return RedirectToAction("Details", "Event", new {id = model.EventId });
            }

            throw new UnauthorizedAccessException("An error occurred");
        }
    }
}