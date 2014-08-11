namespace ProEvoCanary.Models
{
    public class AdminModel : IUser
    {
        public int UserId { get; private set; }
        public string Forename { get; private set; }
        public string Surname { get; private set; }
        public string Username { get; private set; }

        public AdminModel(int userId, string forename, string surname, string username)
        {
            UserId = userId;
            Forename = forename;
            Surname = surname;
            Username = username;
        }
    }
}