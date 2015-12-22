using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace ProEvoCanary.Authentication
{
    public class AuthenticationHandler : IAuthenticationHandler
    {
        public void SignIn(string name, string role, int userId)
        {
            var identity = ClaimsIdentityFactory.Create(name, role,userId);
            IOwinContext ctx = HttpContext.Current.Request.GetOwinContext();
            IAuthenticationManager authManager = ctx.Authentication;
            authManager.SignIn(identity);
        }

        public void SignOut()
        {
            IOwinContext ctx = HttpContext.Current.Request.GetOwinContext();
            IAuthenticationManager authManager = ctx.Authentication;
            authManager.SignOut();
        }
    }
}