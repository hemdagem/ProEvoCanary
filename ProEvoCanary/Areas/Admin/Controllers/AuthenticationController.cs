using System.Web.Mvc;
using ProEvoCanary.Areas.Admin.Models;
using ProEvoCanary.Repositories;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Areas.Admin.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public AuthenticationController() : this(new UserRepository())
        {

        }

        // GET: Authentication/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Authentication/Create
        [HttpPost]
        public ActionResult Create(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userRepository.CreateUser(model.Username, model.Forename, model.Surname,
                        model.EmailAddress) > 0)
                {
                    return RedirectToAction("Index", "Default");
                }
                
            }

            return View(model);
        }

    }
}
