using ProEvoCanary.Models;

namespace ProEvoCanary.Helpers
{
    public interface IAuthenticationHandler
    {
        void SignIn(UserModel login);
    }
}