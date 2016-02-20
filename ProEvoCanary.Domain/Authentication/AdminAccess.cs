using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ProEvoCanary.Domain.Authentication
{
    public class AccessAuthorize : AuthorizeAttribute
    {
        private readonly UserType _userType;

        public AccessAuthorize(UserType userType)
        {
            _userType = userType;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            
            var user = HttpContext.Current.User as ClaimsPrincipal;
            if (user != null && (user.HasClaim(ClaimTypes.Role, ((int)_userType).ToString()) || user.HasClaim(ClaimTypes.Role, ((int)UserType.Admin).ToString())))
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