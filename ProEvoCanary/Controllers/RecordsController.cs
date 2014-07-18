using System.Collections.Generic;
using System.Web.Mvc;
using ProEvo45.Models;
using ProEvo45.Repositories;
using ProEvo45.Repositories.Interfaces;
using ProEvoCanary.Models;

namespace ProEvoCanary.Controllers
{
    public class RecordsController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IResultRepository _resultRepository;

        public RecordsController(IPlayerRepository playerRepository, IResultRepository resultRepository)
        {
            _playerRepository = playerRepository;
            _resultRepository = resultRepository;
        }

        public RecordsController() : this(new PlayerRepository(), new ResultsRepository())
        {
            
        }

        public ActionResult HeadToHead()
        {
            var playerList = _playerRepository.GetPlayerList();
            ResultsListModel model = new ResultsListModel
            {
                Items = playerList,
                Results = new List<ResultsModel>()
            };

            return View("HeadToHead", model);
        }

        public ActionResult HeadToHeadResults(int playerOneId, int playerTwoId)
        {
            var playerList = _playerRepository.GetPlayerList();
            var model = new ResultsListModel
            {
                Items = playerList,
                Results = _resultRepository.GetHeadToHeadResults(playerOneId,playerTwoId)
            };

            return View("HeadToHead", model);
        }
    }
}