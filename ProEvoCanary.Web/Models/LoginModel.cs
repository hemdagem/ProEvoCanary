using System.ComponentModel.DataAnnotations;

namespace ProEvoCanary.Web.Models
{
    public class LoginModel
    {
        public LoginModel() { }

        public LoginModel(string username, string password)
        {
           
            Username = username;
            Password = password;
        }

        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }

        
    }
}