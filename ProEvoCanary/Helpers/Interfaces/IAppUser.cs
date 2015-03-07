namespace ProEvoCanary.Helpers.Interfaces
{
    public interface IAppUser
    {
         UserClaimsPrincipal CurrentUser { get; }
    }
}
