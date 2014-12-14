using ProEvoCanary.Models.Interfaces;

namespace ProEvoCanary.Models
{
    public class UserModel : IUser
    {
        public int UserId { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }

        public UserModel() { }

        public UserModel(int userId, string forename, string surname, string username)
        {
            UserId = userId;
            Forename = forename;
            Surname = surname;
            Username = username;
        }
    }
}