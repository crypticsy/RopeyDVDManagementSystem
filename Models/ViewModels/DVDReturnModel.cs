using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models.ViewModels
{
    public class DVDReturnModel
    {
        public string? DVDTitleName { get; set; }

        public string? DVDCategory { get; set; }

        public uint CopyNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime DateOut { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime DateDue { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime DateReturned { get; set; }

        public string? MemberName { get; set; }

        public int TotalLoan { get; set; }

        public decimal Payment { get; set; }

        public uint LoanNumber { get; set; }

        public int OverDue { get; set; }

        public decimal StandardCharge { get; set; }
        
        public decimal PenaltyCharge { get; set; }
    }

}
