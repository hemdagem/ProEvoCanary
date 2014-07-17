using System.Web.Mvc;
using ProEvo45.Repositories;
using ProEvo45.Repositories.Interfaces;

namespace ProEvoTests
{
    public class RecordsController : Controller
    {
        private readonly IPlayerRepository _playerRepository;

        public RecordsController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public RecordsController() : this(new PlayerRepository())
        {
            
        }

        public ActionResult HeadToHead()
        {
            var model = _playerRepository.GetPlayerList();

            return View("HeadToHead", model);
        }
    }
}