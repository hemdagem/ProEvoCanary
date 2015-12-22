using System.Security.Claims;

namespace ProEvoCanary.Authentication
{
    public class ClaimsIdentityFactory
    {
        const string AuthenticationType = "ApplicationCookie";

        public static ClaimsIdentity Create(string name, string role, int userId)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier,userId.ToString()) 
            },
            AuthenticationType);
            return identity;
        }
    }
}