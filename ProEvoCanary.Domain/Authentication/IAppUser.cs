namespace ProEvoCanary.Authentication
{
    public interface IAppUser
    {
         UserClaimsPrincipal CurrentUser { get; }
    }
}
