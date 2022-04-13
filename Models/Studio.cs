using System.ComponentModel.DataAnnotations;
namespace RopeyDVDManagementSystem.Models
{
    public class Studio
    {
        [Key]
        public uint StudioNumber { get; set; }

        [Required]
        public string StudioName { get; set; }

        public ICollection<DVDTitle> DVDTitles { get; set; }

    }
}
