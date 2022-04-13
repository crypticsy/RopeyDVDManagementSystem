using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models
{
    public class Actor
    {
        [Key]
        public uint ActorNumber { get; set; }

        [Required]
        public string ActorFirstName { get; set; }

        public string ActorSurName { get; set; }
        public ICollection<CastMember> CastMembers { get; set; }
    }
}
