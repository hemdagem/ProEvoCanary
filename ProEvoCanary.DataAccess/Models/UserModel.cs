namespace ProEvoCanary.DataAccess.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public int UserType { get; set; }

        public UserModel() { }

        public UserModel(int userId, string forename, string surname, string username, int userType)
        {
            UserType = userType;
            UserId = userId;
            Forename = forename;
            Surname = surname;
            Username = username;
        }
    }
}