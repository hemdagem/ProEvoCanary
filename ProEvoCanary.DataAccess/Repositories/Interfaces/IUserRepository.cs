using System;
using System.Collections.Generic;
using ProEvoCanary.DataAccess.Models;

namespace ProEvoCanary.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository 
    {
        UserModel GetUser(string username);
        List<UserModel> GetUsers();
        Guid CreateUser(Guid id,string userName, string forename, string surname, string emailAddress);
    }
}