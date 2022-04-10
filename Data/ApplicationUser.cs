using Microsoft.AspNetCore.Identity;

namespace RopeyDVDManagementSystem.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserType { get; set; }
    }
}
