using System.ComponentModel.DataAnnotations;
using ProEvoCanary.Models;

namespace ProEvoCanary.ViewModels
{
    public class LoginModel
    {
        public LoginModel() { }

        public LoginModel(string forename, string surname, string username, string emailAddress, int teamId)
        {
            Forename = forename;
            Surname = surname;
            Username = username;
            EmailAddress = emailAddress;
            TeamId = teamId;
        }

        public int UserId { get; set; }
        [Required]
        public string Forename { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        public int TeamId { get; set; }
        
    }
}