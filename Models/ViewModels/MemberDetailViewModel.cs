using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models.ViewModels
{
    public class MemberDetailViewModel
    {

        public uint MemberNumber { get; set; }

        public string? MembershipCategory { get; set; }

        public string? MemberFirstName { get; set; }

        public string? MemberLastName { get; set; }

        public string? MemberAddress { get; set; }

        public int LoanCount { get; set; }

        public string? Remarks { get; set; }

        public string? LastLoanDVDTitle { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime LastLoanDate { get; set; }

        public int LastActivity { get; set; }

    }

}
