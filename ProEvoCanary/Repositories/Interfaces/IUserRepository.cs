using ProEvoCanary.Models;

namespace ProEvoCanary.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IUser GetUser(string username);
    }
}