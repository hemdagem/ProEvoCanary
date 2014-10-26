using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace ProEvoCanary
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
                LoginPath = new PathString("/authentication/login")
            });
        }
    }
}