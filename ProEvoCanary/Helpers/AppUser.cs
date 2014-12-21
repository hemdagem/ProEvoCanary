﻿using System.Security.Claims;
using ProEvoCanary.Models;

namespace ProEvoCanary.Helpers
{
    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal) : base(principal) { }


        public string Name
        {
            get
            {
                Claim name = FindFirst(ClaimTypes.Name);
                return name == null ? string.Empty : name.Value;
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
    }
}