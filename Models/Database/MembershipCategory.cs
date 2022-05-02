using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models
{
    public class MembershipCategory
    {
        [Key]
        public uint MembershipCategoryNumber { get; set; }

        [Required]
        public string MembershipCategoryName { get; set; }

        [Required]
        public string MembershipCategoryDescription { get; set; }

        [Required]
        public uint MembershipCategoryTotalLoans { get; set; }

        public ICollection<Member> Members { get; set; }
    }
}
