using ProEvoCanary.Models;

namespace ProEvoCanary.Helpers.Interfaces
{
    public interface IAuthenticationHandler
    {
        void SignIn(UserModel login);
    }
}