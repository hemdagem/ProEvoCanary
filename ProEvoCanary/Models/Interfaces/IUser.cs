namespace ProEvoCanary.Models.Interfaces
{
    public interface IUser
    {
        int UserId { get; set; }
        string Forename { get; set; }
        string Surname { get; set; }
        string Username { get; set; }
    }
}