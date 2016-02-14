using System.Security.Claims;

namespace ProEvoCanary.Authentication
{
    public class UserClaimsPrincipal : ClaimsPrincipal
    {
        public UserClaimsPrincipal(ClaimsPrincipal principal) : base(principal) { }


        public string Name
        {
            get
            {
                Claim name = FindFirst(ClaimTypes.Name);
                return name == null ? "Anonymous" : name.Value;
            }
        }

        public string Role
        {
            get
            {
                Claim role = FindFirst(ClaimTypes.Role);
                return role == null ? string.Empty : role.Value;
            }
        }

        public bool IsAdmin
        {
            get
            {
                return Role == UserType.Admin.ToString();
            }
        }

        public int Id
        {
            get
            {
                Claim role = FindFirst(ClaimTypes.NameIdentifier);
                return role == null ? 0 : int.Parse(role.Value);
            }
        }
    }
}