using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models
{
    public class Actor
    {
       /* public Actor()
        {
            this.DVDTitle = new HashSet<DVDTitle>();
        }*/

        [Key]
        public uint ActorNumber { get; set; }

        [Required]
        public string ActorFirstName { get; set; }

        public string ActorSurName { get; set; }
        public ICollection<CastMember> CastMembers { get; set; }
    }
}
