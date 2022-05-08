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
        [Display(Name = "LoanDateOut"), DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime DateOut { get; set; }

        [Required]
        [Display(Name = "LoanDateDue"), DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime DateDue { get; set; }

        [Display(Name = "LoanDateReturned"), DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime DateReturned { get; set; }

        public decimal ReturnAmount { get; set; }
    }
}
