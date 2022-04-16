using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models
{
    public class Member
    {
        [Key]
        public uint MemberNumber { get; set; }

        [Required]
        public uint MembershipCategoryNumber { get; set; }
        public virtual MembershipCategory MembershipCategory { get; set; }

        [Required]
        public string MemberFirstName { get; set; }

        [Required]
        public string MemberLastName { get; set; }

        [Required]
        public string MemberAddress { get; set; }

        [Required]
        public DateTime MemberDateOfBirth { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}
