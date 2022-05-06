using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models
{
    public class UserRegisterModel
    {
        public string? UserType { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "User Name is Required")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }
    }
}
