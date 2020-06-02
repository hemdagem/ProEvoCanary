using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Web;

namespace ProEvoCanary.Domain.Authentication
{
    public class CurrentAppUser : IAppUser
    {
        private readonly HttpContext _context;

        public CurrentAppUser(HttpContext context)
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