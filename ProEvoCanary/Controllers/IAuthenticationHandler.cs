using System.Security.Claims;

namespace ProEvoCanary.Controllers
{
    public interface IAuthenticationHandler
    {
        void SignIn(ClaimsIdentity identity);
    }
}