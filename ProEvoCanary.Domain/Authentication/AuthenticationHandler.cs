using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace ProEvoCanary.Domain.Authentication
{
    public class AuthenticationHandler : IAuthenticationHandler
    {
	    private readonly HttpContext _context;

	    public AuthenticationHandler(HttpContext context)
	    {
		    _context = context;
	    }
        public void SignIn(string name, string role, int userId)
        {
            //var identity = ClaimsIdentityFactory.Create(name, role,userId);
            //IOwinContext ctx = AuthenticationHttpContextExtensions.AuthenticateAsync().Result;
            //IAuthenticationManager authManager = ctx.Authentication;
            //authManager.SignIn(identity);
        }

        public void SignOut()
        {
            //IOwinContext ctx = _context.GetOwinContext();
            //IAuthenticationManager authManager = ctx.Authentication;
            //authManager.SignOut();
        }
    }
}