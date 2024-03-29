﻿using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models
{
    public class DVDCopy
    {
        [Key]
        public uint CopyNumber { get; set; }

        [Required]
        public uint DVDNumber { get; set; }

        public DVDTitle DVDTitle { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime DatePurchased { get; set; }

        public bool IsOnLoan { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}
