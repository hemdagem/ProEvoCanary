using System.Web.Mvc;
using ProEvoCanary.Domain.Authentication;

namespace ProEvoCanary.AdminWeb.Controllers
{
    [AccessAuthorize(UserType.Admin)]
    public class DefaultController : Controller
    {
        // GET: Admin/Default
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}