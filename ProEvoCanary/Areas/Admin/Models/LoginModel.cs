using System.ComponentModel.DataAnnotations;

namespace ProEvoCanary.Areas.Admin.Models
{
    public class LoginModel
    {
        public LoginModel() { }

        public LoginModel(string forename, string surname, string username, string emailAddress)
        {
            Forename = forename;
            Surname = surname;
            Username = username;
            EmailAddress = emailAddress;
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
        
    }
}