using System;
using Microsoft.AspNetCore.Mvc;
using ProEvoCanary.DataAccess.Repositories.Interfaces;
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
				_userRepository.CreateUser(Guid.NewGuid(), model.Username, model.Forename, model.Surname,
					model.EmailAddress);
				return RedirectToAction("Index", "Default");
			}

			return View(model);
		}


	}
}
