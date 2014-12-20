using System.Security.Claims;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace ProEvoCanary.Controllers
{
   public class AuthenticationHandler : IAuthenticationHandler
    {
        public void SignIn(ClaimsIdentity identity)
        {
            IOwinContext ctx = HttpContext.Current.Request.GetOwinContext();
            IAuthenticationManager authManager = ctx.Authentication;
            authManager.SignIn(identity);
        }
    }
}