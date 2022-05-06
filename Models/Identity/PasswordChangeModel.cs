using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models.Identity
{
    public class PasswordChangeModel
    {
        [Required(ErrorMessage = "Current Password is Required"), DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }


        [Required(ErrorMessage = "New Password is Required"), DataType(DataType.Password)]
        public string? NewPassword { get; set; }


        [Required(ErrorMessage = "Confirm Password is Required"), DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
    }
}
