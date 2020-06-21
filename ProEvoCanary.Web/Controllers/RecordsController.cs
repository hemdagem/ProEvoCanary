using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEvoCanary.DataAccess.Repositories.Interfaces;
using ProEvoCanary.Web.Models;

namespace ProEvoCanary.Web.Controllers
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
                PlayerList = playerList.Select(x=> new PlayerModel
                {
	                
                }).ToList()
            };

            return View("HeadToHead", model);
        }

        [HttpGet]
        public JsonResult HeadToHeadResult(int playerOneId, int playerTwoId)
        {
            var playerOneList = _playerRepository.GetAllPlayers();
            var headToHeadRecord = _resultRepository.GetHeadToHeadRecord(playerOneId, playerTwoId);
            var model = new ResultsListModel
            {
                PlayerList = playerOneList.Select(x => new PlayerModel
                {

                }).ToList(),
                PlayerOne = playerOneId,
                PlayerTwo = playerTwoId,
                HeadToHead = new RecordsModel
                {
                    PlayerOneWins = headToHeadRecord.PlayerOneWins,PlayerTwoWins = headToHeadRecord.PlayerTwoWins,
                    Results = headToHeadRecord.Results.Select(x=> new ResultsModel()).ToList(),TotalDraws = headToHeadRecord.TotalDraws,TotalMatches = headToHeadRecord.TotalMatches
                }
            };

            return Json(model);
        }

        public ActionResult HeadToHeadResults(int playerOneId, int playerTwoId)
        {
	        var playerOneList = _playerRepository.GetAllPlayers();
	        var headToHeadRecord = _resultRepository.GetHeadToHeadRecord(playerOneId, playerTwoId);
	        var model = new ResultsListModel
	        {
		        PlayerList = playerOneList.Select(x => new PlayerModel
		        {

		        }).ToList(),
		        PlayerOne = playerOneId,
		        PlayerTwo = playerTwoId,
		        HeadToHead = new RecordsModel
		        {
			        PlayerOneWins = headToHeadRecord.PlayerOneWins,
			        PlayerTwoWins = headToHeadRecord.PlayerTwoWins,
			        Results = headToHeadRecord.Results.Select(x => new ResultsModel()).ToList(),
			        TotalDraws = headToHeadRecord.TotalDraws,
			        TotalMatches = headToHeadRecord.TotalMatches
		        }
	        };

            return View("HeadToHead", model);
        }
    }
}