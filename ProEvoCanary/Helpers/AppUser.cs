using System.Security.Claims;

namespace ProEvoCanary.Helpers
{
    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal) : base(principal) { }


        public string Name
        {
            get
            {
                return FindFirst(ClaimTypes.Name).Value;
            }
        }

        public string Role
        {
            get
            {
                return FindFirst(ClaimTypes.Role).Value;
            }
        }
    }
}