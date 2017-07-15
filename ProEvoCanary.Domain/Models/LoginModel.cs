namespace ProEvoCanary.Domain.Models
{
    public class LoginModel
    {
        public LoginModel() { }

        public LoginModel(string username, string password)
        {
           
            Username = username;
            Password = password;
        }
        
        public string Password { get; set; }
        public string Username { get; set; }

        
    }
}