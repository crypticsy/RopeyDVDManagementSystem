using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models
{
    public class LoanType
    {
        [Key]
        public uint LoanTypeNumber { get; set; }

        [Required]
        public string LoanTypeTitle { get; set; }

        [Required]
        public uint LoanDuration { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}
