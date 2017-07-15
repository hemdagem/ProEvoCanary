using System.ComponentModel.DataAnnotations;

namespace ProEvoCanary.AdminWeb.Models
{
    public class CreateUserModel
    {
        public CreateUserModel() { }

        public CreateUserModel(string forename, string surname, string username, string emailAddress, string password)
        {
            Password = password;
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
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}