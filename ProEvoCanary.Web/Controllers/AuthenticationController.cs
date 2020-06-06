using Microsoft.AspNetCore.Mvc;
using ProEvoCanary.Domain.Repositories.Interfaces;
using ProEvoCanary.Web.Models;

namespace ProEvoCanary.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: Authentication/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Authentication/Create
        [HttpPost]
        public ActionResult Create(CreateUserModel model)
        {
	        if (ModelState.IsValid)
	        {
		        if (_userRepository.CreateUser(model.Username, model.Forename, model.Surname, model.EmailAddress, model.Password) > 0)
		        {
			        return RedirectToAction("Index", "Default");
		        }
	        }

	        return View(model);
        }


    }
}
