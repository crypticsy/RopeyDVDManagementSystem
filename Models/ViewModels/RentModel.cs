using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models.ViewModels
{
    public class RentModel
    {
        public string? DVDTitleName { get; set; }

        public string? DVDCategory { get; set; }

        public uint CopyNumber { get; set; }

        public bool AgeRestricted { get; set; }

        public int LoanTypeNumber { get; set; }

        public int MemberNumber { get; set; }


    }
}
