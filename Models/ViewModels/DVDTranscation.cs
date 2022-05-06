namespace RopeyDVDManagementSystem.Models.ViewModels
{
    public class DVDTranscation
    {
        public string? DVDTitleName { get; set; }

        public uint CopyNumber { get; set; }

        public DateTime DateOut { get; set; }

        public DateTime DateDue { get; set; }

        public DateTime DateReturned { get; set; }

        public string MemberFirstName { get; set; }

        public string MemberLastName { get; set; }

        public string MemberName { get; set; }

        public uint TotalLoan { get; set; }
    }

}
