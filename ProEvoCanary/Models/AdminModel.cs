using ProEvoCanary.Models.Interfaces;

namespace ProEvoCanary.Models
{
    public class AdminModel : IUser
    {
        public int UserId { get;  set; }
        public string Forename { get;  set; }
        public string Surname { get;  set; }
        public string Username { get;  set; }

        public AdminModel(int userId, string forename, string surname, string username)
        {
            UserId = userId;
            Forename = forename;
            Surname = surname;
            Username = username;
        }
    }
}