namespace ProEvoCanary.Models
{
    public interface IUser
    {
        int UserId { get; set; }
        string Forename { get; set; }
        string Surname { get; set; }
        string Username { get; set; }
    }
}