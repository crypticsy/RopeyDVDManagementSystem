using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models
{
    public class Loan
    {
        [Key]
        public uint LoanNumber { get; set; }

        [Required]
        public uint LoanTypeNumber { get; set; }
        public LoanType LoanType { get; set; }

        [Required]
        public uint CopyNumber { get; set; }
        public DVDCopy DVDCopy { get; set; }

        [Required]
        public uint MemberNumber { get; set; }
        public Member Member { get; set; }

        [Required]
        public DateOnly DateOut { get; set; }

        [Required]
        public DateOnly DateDue { get; set; }

      
        public DateOnly DateReturned { get; set; }
    }
}
