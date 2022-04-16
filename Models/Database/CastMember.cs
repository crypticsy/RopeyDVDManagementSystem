using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models
{
    public class CastMember
    {
        [Key]
        public uint ActorNumber { get; set; }
        public virtual Actor Actor { get; set; }

        [Key]
        public uint DVDNumber { get; set; }
        public virtual DVDTitle DVDTitle { get; set; }

    }
}
