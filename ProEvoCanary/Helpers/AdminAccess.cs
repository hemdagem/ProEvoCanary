using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ProEvoCanary.Helpers
{
    public class AdminAuthorize : AuthorizeAttribute
    {
        private readonly string _claimType;
        private readonly string _claimValue;

        public AdminAuthorize(string type, string value)
        {
            _claimType = type;
            _claimValue = value;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = HttpContext.Current.User as ClaimsPrincipal;
            if (user != null && user.HasClaim(_claimType, _claimValue))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}