using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models
{
    public class DVDCategory
    {
        [Key]
        public uint CategoryNumber { get; set; }

        [Required]
        public string? CategoryDescription { get; set; }

        [Required]
        public bool AgeRestricted { get; set; }

        public ICollection<DVDTitle> DVDTitles { get; set; }
    }
}
