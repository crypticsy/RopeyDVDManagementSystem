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
        [Display(Name = "LoanCopyNumber")]
        public uint CopyNumber { get; set; }
        public virtual DVDCopy DVDCopy { get; set; }

        [Required]
        [Display(Name = "LoanMemberNumber")]
        public uint MemberNumber { get; set; }
        public virtual Member Member { get; set; }

        [Required]
        [Display(Name = "LoanDateOut")]
        public DateTime DateOut { get; set; }

        [Required]
        [Display(Name = "LoanDateDue")]
        public DateTime DateDue { get; set; }

        [Display(Name = "LoanDateReturned")]
        public DateTime DateReturned { get; set; }

        public decimal ReturnAmount { get; set; }
    }
}
