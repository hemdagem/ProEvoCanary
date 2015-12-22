using System.Collections.Generic;
using ProEvoCanary.Domain.Models;

namespace ProEvoCanary.Domain.Repositories.Interfaces
{
    public interface IUserRepository 
    {
        UserModel GetUser(string username);
        List<UserModel> GetUsers();
        int CreateUser(string userName, string forename, string surname, string emailAddress, string password);
        UserModel Login(LoginModel loginModel);
    }
}