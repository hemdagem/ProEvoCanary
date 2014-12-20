using System;
using System.Security.Claims;
using ProEvoCanary.Models;

namespace ProEvoCanary.Helpers
{
    public class ClaimsIdentityFactory
    {
        const string AuthenticationType = "ApplicationCookie";

        public static ClaimsIdentity Create(UserModel login)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, login.Forename),
                new Claim(ClaimTypes.Role, Enum.Parse(typeof (UserType), login.UserType.ToString()).ToString()),
            }, AuthenticationType);
            return identity;
        }
    }
}