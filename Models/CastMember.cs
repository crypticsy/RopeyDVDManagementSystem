using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models
{
    public class CastMember
    {
        [Key]
        public uint ActorNumber { get; set; }
        [Key]
        public uint DVDNumber { get; set; }

        public Actor Actor { get; set; }
        public DVDTitle DVDTitle { get; set; }

    }
}
