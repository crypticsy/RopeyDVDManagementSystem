using System.ComponentModel.DataAnnotations;
namespace RopeyDVDManagementSystem.Models
{
    public class Producer
    {
        [Key]
        public uint ProducerNumber { get; set; }

        [Required]
        public string? ProducerName { get; set; }

        public ICollection<DVDTitle> DVDTitles { get; set; }
    }
}
