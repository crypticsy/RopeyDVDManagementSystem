    using System.ComponentModel.DataAnnotations;

    namespace RopeyDVDManagementSystem.Models
    {
        public class Actor
        {
            [Key]
            public uint ActorNumber { get; set; }

            [Required]
            public string? ActorFirstName { get; set; }

            [Required]  
            public string? ActorSurName { get; set; }

            public string? ActorFullName => ActorFirstName + " " + ActorSurName;

            public ICollection<CastMember> CastMembers { get; set; }

        }
    }
