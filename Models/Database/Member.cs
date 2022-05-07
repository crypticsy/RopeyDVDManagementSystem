using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models
{
    public class Member
    {
        [Key]
        public uint MemberNumber { get; set; }

        [Required(ErrorMessage ="Membership Category Number is required!")]
        public uint MembershipCategoryNumber { get; set; }
        public virtual MembershipCategory MembershipCategory { get; set; }

        [Required(ErrorMessage = "First name is required!")]
        public string MemberFirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        public string MemberLastName { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        public string MemberAddress { get; set; }

        [Required(ErrorMessage = "Date of birth is required!")]
        public DateTime MemberDateOfBirth { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}
