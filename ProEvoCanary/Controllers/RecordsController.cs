using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProEvoCanary.Domain.Repositories.Interfaces;
using ProEvoCanary.Models;

namespace ProEvoCanary.Controllers
{
    [AllowAnonymous]
    public class RecordsController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IResultRepository _resultRepository;

        public RecordsController(IPlayerRepository playerRepository, IResultRepository resultRepository)
        {
            _playerRepository = playerRepository;
            _resultRepository = resultRepository;
        }

        public ActionResult HeadToHead()
        {
            var playerList = _playerRepository.GetAllPlayers();
            var model = new ResultsListModel
            {
                PlayerList = playerList.Select(x => new PlayerModel { GoalsPerGame = x.GoalsPerGame, MatchesPlayed = x.MatchesPlayed, PlayerId = x.PlayerId, PlayerName = x.PlayerName, PointsPerGame = x.PointsPerGame }).ToList()
            };

            return View("HeadToHead", model);
        }

        public JsonResult HeadToHeadResult(int playerOneId, int playerTwoId)
        {
            var playerOneList = _playerRepository.GetAllPlayers();

            var model = new ResultsListModel
            {
                PlayerList = playerOneList.Select(x => new PlayerModel { GoalsPerGame = x.GoalsPerGame, MatchesPlayed = x.MatchesPlayed, PlayerId = x.PlayerId, PlayerName = x.PlayerName, PointsPerGame = x.PointsPerGame }).ToList(),
                PlayerOne = playerOneId,
                PlayerTwo = playerTwoId,
                //HeadToHead = _resultRepository.GetHeadToHeadRecord(playerOneId, playerTwoId)
            };

            return Json(model);
        }

        public ActionResult HeadToHeadResults(int playerOneId, int playerTwoId)
        {
            var playerOneList = _playerRepository.GetAllPlayers();

            var model = new ResultsListModel
            {
                PlayerList = playerOneList.Select(x=> new PlayerModel()).ToList(),
                PlayerOne = playerOneId,
                PlayerTwo = playerTwoId,
                //HeadToHead = _resultRepository.GetHeadToHeadRecord(playerOneId, playerTwoId)
            };

            return View("HeadToHead", model);
        }
    }
}