using System.Security.Claims;
using System.Web;

namespace ProEvoCanary.Authentication
{
    public class CurrentAppUser : IAppUser
    {
        private readonly HttpContextBase _context;

        public CurrentAppUser(HttpContextBase context)
        {
            _context = context;
        }

        public UserClaimsPrincipal CurrentUser
        {
            get
            {
                return new UserClaimsPrincipal(_context.User as ClaimsPrincipal);
            }
        }
    }
}