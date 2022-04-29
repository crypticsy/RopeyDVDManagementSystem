namespace RopeyDVDManagementSystem.Models.ViewModels
{
    public class UserDetailsViewModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
