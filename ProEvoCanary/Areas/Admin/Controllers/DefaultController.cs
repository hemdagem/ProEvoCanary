using System.Security.Claims;
using System.Web.Mvc;
using ProEvoCanary.Helpers;

namespace ProEvoCanary.Areas.Admin.Controllers
{
    [AdminAuthorize(ClaimTypes.Role, "Admin")]
    public class DefaultController : Controller
    {
        // GET: Admin/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}