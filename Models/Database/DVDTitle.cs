﻿using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models
{
    public class DVDTitle
    {
        [Key]
        public uint DVDNumber { get; set; }

        [Required]
        public string? DVDTitleName { get; set; }

        [Required]
        public uint CategoryNumber { get; set; }
        public virtual DVDCategory DVDCategory { get; set; }

        [Required]
        public uint StudioNumber { get; set; }
        public virtual Studio Studio { get; set; }

        [Required]
        public uint ProducerNumber { get; set; }
        public virtual Producer Producer { get; set; }

        [Required]
        public DateTime DateReleased { get; set; }

        [Required]
        public decimal StandardCharge { get; set; }

        [Required]
        public decimal PenaltyCharge { get; set; }

        public ICollection<DVDCopy> DVDCopys { get; set; }
        
        public ICollection<CastMember> CastMembers { get; set; }
    }
}