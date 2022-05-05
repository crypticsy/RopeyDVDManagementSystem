using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models
{
    public class Loan
    {
        [Key]
        public uint LoanNumber { get; set; }

        [Required]
        public uint LoanTypeNumber { get; set; }
        public virtual LoanType LoanType { get; set; }

        [Required]
        public uint CopyNumber { get; set; }
        public virtual DVDCopy DVDCopy { get; set; }

        [Required]
        public uint MemberNumber { get; set; }
        public virtual Member Member { get; set; }

        [Required]
        public DateTime DateOut { get; set; }

        [Required]
        public DateTime DateDue { get; set; }

        public DateTime DateReturned { get; set; }

        public decimal ReturnAmount { get; set; }
    }
}
