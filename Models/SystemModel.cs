using System.ComponentModel.DataAnnotations;

namespace RopeyDVDManagementSystem.Models
{
    public class SystemModel
    {
        public class Studio
        {
            [Key, Required]
            public int StudioNumber { get; set; }
            [Required]
            public String StudioName { get; set; }

            public ICollection<DVDDescription> DVDDescription { get; set; }

        }

        public class Producer
        {
            [Key, Required]
            public int ProducerNumber { get; set; }
            [Required]
            public String ProducerName { get; set; }
            public ICollection<DVDDescription> DVDDescription { get; set; }

        }

        public class Actor
        {
            public Actor()
            {
                this.DVDDescription = new HashSet<DVDDescription>();
            }

            [Key, Required]
            public int ActorNumber { get; set; }
            [Required]
            public String ActorFirstName { get; set; }

            public String ActorSurName { get; set; }
            public virtual ICollection<DVDDescription> DVDDescription { get; set; }

        }

        public class DVDCategory
        {
            [Key, Required]
            public int CategoryNumber { get; set; }
            [Required]
            public String CategoryDescription { get; set; }
            [Required]
            public bool AgeRestricted { get; set; }
            public ICollection<DVDDescription> DVDDescription { get; set; }
        }

        public class DVDDescription
        {
            public DVDDescription()
            {
                this.Actor = new HashSet<Actor>();
            }
            [Key, Required]
            public int DVDNumber { get; set; }
            [Required]
            public String DVDTitle { get; set; }
            [Required]
            public int CategoryNumber { get; set; }
            public DVDCategory DVDCategory { get; set; }
            [Required]
            public int StudioNumber { get; set; }
            public Studio Studio { get; set; }
            [Required]
            public int ProducerNumber { get; set; }
            public Producer Producer { get; set; }
            [Required]
            public DateOnly DateReleased { get; set; }
            [Required]
            public int StandardCharge { get; set; }
            [Required]
            public int PenaltyCharge { get; set; }

            public virtual ICollection<Actor> Actor { get; set; }


        }

        //public class CastMember
        //{
        //    [Key,Required]
        //    public int ActorNumber { get; set; }
        //    [Key,Required]
        //    public int DVDNumber { get; set; }
        //}

        public class DVDCopy
        {
            [Key, Required]
            public int CopyNumber { get; set; }
            [Required]
            public int DVDNumber { get; set; }
            [Required]
            public DateOnly DatePurchases { get; set; }
            public ICollection<Loan> Loans { get; set; }
        }

        public class MembershipCategory
        {
            [Key, Required]
            public int MembershipCategoryNumber { get; set; }
            [Required]
            public string MembershipCategoryDescription { get; set; }
            [Required]
            public int MembershipCategoryTotalLoans { get; set; }
            public ICollection<Member> Members { get; set; }
        }

        public class Member
        {
            [Key, Required]
            public int MemberNumber { get; set; }
            [Required]
            public int MembershipCategoryNumber { get; set; }
            public MembershipCategory membershipCategory { get; set; }
            [Required]
            public String MemberFirstName { get; set; }
            [Required]
            public String MemberLastName { get; set; }
            [Required]
            public String MemberAddress { get; set; }
            [Required]
            public DateOnly MemberDateOfBirth { get; set; }
            public ICollection<Loan> Loan { get; set; }
        }
        public class LoanType
        {
            [Key, Required]
            public int LoanTypeNumber { get; set; }
            [Required]
            public String LoanTypeTitle { get; set; }
            [Required]
            public Double LoanDuration { get; set; }
            public ICollection<Loan> Loan { get; set; }
        }

        public class Loan
        {
            [Key, Required]
            public int LoanNumber { get; set; }
            [Required]
            public int LoanTypeNumber { get; set; }
            public LoanType LoanType { get; set; }
            [Required]
            public int CopyNumber { get; set; }
            public DVDCopy DVDCopy { get; set; }
            [Required]
            public int MemberNumber { get; set; }
            public Member Member { get; set; }
            [Required]
            public DateOnly DateOut { get; set; }
            [Required]
            public DateOnly DateDue { get; set; }
            [Required]
            public DateOnly DateReturned { get; set; }

        }
        public class User
        {
            [Key,Required]
            public int UserNumber { get; set; }
            [Required]
            public String UserName { get; set; }
            [Required]
            public String UserType { get; set; }
            [Required]
            public String UserPassword { get; set; }

        }
    }

}
