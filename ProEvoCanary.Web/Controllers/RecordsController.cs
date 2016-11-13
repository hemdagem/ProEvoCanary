using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using ProEvoCanary.Domain.Repositories.Interfaces;
using ProEvoCanary.Web.Models;

namespace ProEvoCanary.Web.Controllers
{
    [AllowAnonymous]
    public class RecordsController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IResultRepository _resultRepository;
        private readonly IMapper _mapper;

        public RecordsController(IPlayerRepository playerRepository, IResultRepository resultRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _resultRepository = resultRepository;
            _mapper = mapper;
        }

        public ActionResult HeadToHead()
        {
            var playerList = _playerRepository.GetAllPlayers();
            var model = new ResultsListModel
            {
                PlayerList = _mapper.Map<List<PlayerModel>>(playerList)
            };

            return View("HeadToHead", model);
        }

        [HttpGet]
        public JsonResult HeadToHeadResult(int playerOneId, int playerTwoId)
        {
            var playerOneList = _playerRepository.GetAllPlayers();

            var model = new ResultsListModel
            {
                PlayerList = _mapper.Map<List<PlayerModel>>(playerOneList),
                PlayerOne = playerOneId,
                PlayerTwo = playerTwoId,
                HeadToHead = _mapper.Map<RecordsModel>(_resultRepository.GetHeadToHeadRecord(playerOneId, playerTwoId))
            };

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HeadToHeadResults(int playerOneId, int playerTwoId)
        {
            var playerOneList = _playerRepository.GetAllPlayers();

            var model = new ResultsListModel
            {
                PlayerList = _mapper.Map<List<PlayerModel>>(playerOneList),
                PlayerOne = playerOneId,
                PlayerTwo = playerTwoId,
                HeadToHead = _mapper.Map<RecordsModel>(_resultRepository.GetHeadToHeadRecord(playerOneId, playerTwoId))
            };

            return View("HeadToHead", model);
        }
    }
}