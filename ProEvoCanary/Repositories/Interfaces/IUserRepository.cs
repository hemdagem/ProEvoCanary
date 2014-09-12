using ProEvoCanary.Models.Interfaces;

namespace ProEvoCanary.Repositories.Interfaces
{
    public interface IUserRepository 
    {
        IUser GetUser(string username);
        int CreateUser(string userName, string forename, string surname, string emailAddress);
    }
}