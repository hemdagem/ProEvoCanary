using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public AuthenticationController() : this(new UserRepository()) { }

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

        // GET: Authentication
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("Login");
        }

        // POST: Authentication
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserModel login = _userRepository.Login(model);

                var identity = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.Name, login.Forename),
                    new Claim(ClaimTypes.Role,Enum.Parse(typeof(UserType),login.UserType.ToString()).ToString()), 
                }, "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    Redirect(returnUrl);
                }
            }


            return View();
        }
    }
}
