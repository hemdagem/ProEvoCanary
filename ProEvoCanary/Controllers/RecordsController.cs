using System.Globalization;
using System.Web.Mvc;
using ProEvoCanary.Repositories;
using ProEvoCanary.Repositories.Interfaces;
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
                PlayerList = playerList,
            };

            return View("HeadToHead", model);
        }

        public ActionResult HeadToHeadResults(int playerOneId, int playerTwoId)
        {
            var playerOneList = _playerRepository.GetAllPlayers();


            var model = new ResultsListModel
            {
                PlayerList = playerOneList,
                PlayerOne = playerOneId,
                PlayerTwo = playerTwoId,
                HeadToHead = _resultRepository.GetHeadToHeadRecord(playerOneId, playerTwoId)
            };

            return View("HeadToHead", model);
        }
    }
}