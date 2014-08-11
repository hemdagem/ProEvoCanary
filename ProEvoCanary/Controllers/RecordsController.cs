using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using ProEvoCanary.Repositories;
using ProEvoCanary.Repositories.Interfaces;
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

        public RecordsController()
            : this(new PlayerRepository(), new ResultsRepository())
        {

        }

        public ActionResult HeadToHead()
        {
            var playerList = _playerRepository.GetPlayerList();
            var model = new ResultsListModel
            {
                PlayerOneList = playerList,
                Results = new List<ResultsModel>(),
                PlayerTwoList = new SelectListModel
            {
                ListItems = playerList.ListItems
            }
            };

            return View("HeadToHead", model);
        }

        public ActionResult HeadToHeadResults(int playerOneId, int playerTwoId)
        {
            var playerOneList = _playerRepository.GetPlayerList();
            playerOneList.SelectedItem = playerOneId.ToString(CultureInfo.InvariantCulture);


            var model = new ResultsListModel
            {
                PlayerOneList = playerOneList,
                PlayerTwoList = new SelectListModel
            {
                ListItems = playerOneList.ListItems,
                SelectedItem = playerTwoId.ToString(CultureInfo.InvariantCulture)
            },
                Results = _resultRepository.GetHeadToHeadResults(playerOneId, playerTwoId),
                HeadToHead = _resultRepository.GetHeadToHeadRecord(playerOneId, playerTwoId)
            };

            return View("HeadToHead", model);
        }
    }
}