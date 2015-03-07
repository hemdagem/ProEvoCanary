using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationHandler _authenticationHandler;

        public AuthenticationController(IUserRepository userRepository, IAuthenticationHandler authenticationHandler)
        {
            _userRepository = userRepository;
            _authenticationHandler = authenticationHandler;
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

                if (login != null)
                {
                    _authenticationHandler.SignIn(login);

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        Redirect(returnUrl);
                    }
                    else
                    {
                        RedirectToAction("Index", "Default");
                    }
                }
            }

            return View();
        }

        public ActionResult Signout()
        {
            IOwinContext ctx = System.Web.HttpContext.Current.Request.GetOwinContext();
            IAuthenticationManager authManager = ctx.Authentication;

            authManager.SignOut();
            return RedirectToAction("Index", "Default");
        }


    }
}
