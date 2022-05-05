using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models.ViewModels
{
    public class DVDPreviewModel
    {
        public string? DVDTitleName { get; set; }

        public string? DVDPoster { get; set; }

        public string? DVDCategory { get; set; }

        public string? CastMember { get; set; }

        public decimal StandardCharge { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime DateReleased { get; set; }

        public int AvailableQuantity { get; set; }

        public string? DVDType { get; set; }
    }
}
