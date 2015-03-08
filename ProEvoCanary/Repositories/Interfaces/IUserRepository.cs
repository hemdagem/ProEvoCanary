using System.Collections.Generic;
using ProEvoCanary.Models;
using ProEvoCanary.Models.Interfaces;

namespace ProEvoCanary.Repositories.Interfaces
{
    public interface IUserRepository 
    {
        IUser GetUser(string username);
        List<IUser> GetUsers();
        int CreateUser(string userName, string forename, string surname, string emailAddress, string password);
        UserModel Login(LoginModel loginModel);
    }
}