namespace ProEvoCanary.Domain.Authentication
{
    public interface IAppUser
    {
         UserClaimsPrincipal CurrentUser { get; }
    }
}
