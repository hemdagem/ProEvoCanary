using System.Security.Claims;
using System.Web.Mvc;

namespace ProEvoCanary.Helpers
{
    public abstract class AppController : Controller
    {
        public virtual UserClaimsPrincipal CurrentUser
        {
            get
            {
                return new UserClaimsPrincipal(this.User as ClaimsPrincipal);
            }
        }
    }
}