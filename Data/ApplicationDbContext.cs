using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RopeyDVDManagementSystem.Models;
using RopeyDVDManagementSystem.Models.Identity;

namespace RopeyDVDManagementSystem.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Relationship mapping for CastMember
            builder.Entity<CastMember>().HasKey(cm => new
            {
                cm.ActorNumber,
                cm.DVDNumber
            });
            builder.Entity<CastMember>().HasOne(cm => cm.Actor).WithMany(cm => cm.CastMembers).HasForeignKey(m => m.ActorNumber);
            builder.Entity<CastMember>().HasOne(cm => cm.DVDTitle).WithMany(cm => cm.CastMembers).HasForeignKey(m => m.DVDNumber);


            // Relationship mapping for DVDCopy
            builder.Entity<DVDCopy>().HasOne(dt => dt.DVDTitle).WithMany(dt => dt.DVDCopys).HasForeignKey(dt => dt.DVDNumber);


            // Relationship mapping for DVDTitle
            builder.Entity<DVDTitle>().HasOne(dt => dt.DVDCategory).WithMany(dt => dt.DVDTitles).HasForeignKey(dt => dt.CategoryNumber);
            builder.Entity<DVDTitle>().HasOne(dt => dt.Producer).WithMany(dt => dt.DVDTitles).HasForeignKey(dt => dt.ProducerNumber);
            builder.Entity<DVDTitle>().HasOne(dt => dt.Studio).WithMany(dt => dt.DVDTitles).HasForeignKey(dt => dt.StudioNumber);
            
            // Property restrictions for DVDTitle
            builder.Entity<DVDTitle>().Property(dt => dt.PenaltyCharge).HasPrecision(10,2);
            builder.Entity<DVDTitle>().Property(dt => dt.StandardCharge).HasPrecision(10, 2);


            // Relationship mapping for Loan
            builder.Entity<Loan>().HasOne(dt => dt.DVDCopy).WithMany(dt => dt.Loans).HasForeignKey(dt => dt.CopyNumber);
            builder.Entity<Loan>().HasOne(dt => dt.LoanType).WithMany(dt => dt.Loans).HasForeignKey(dt => dt.LoanTypeNumber);
            builder.Entity<Loan>().HasOne(dt => dt.Member).WithMany(dt => dt.Loans).HasForeignKey(dt => dt.MemberNumber);


            // Relationship mapping for Members
            builder.Entity<Member>().HasOne(dt => dt.MembershipCategory).WithMany(dt => dt.Members).HasForeignKey(dt => dt.MembershipCategoryNumber);
            
            base.OnModelCreating(builder);
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<CastMember> CastMembers { get; set; }
        public DbSet<DVDCategory> DVDCategories { get; set; }
        public DbSet<DVDCopy> DVDCopies { get; set; }
        public DbSet<DVDTitle> DVDTitles { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanType> LoanTypes { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MembershipCategory> MembershipCategories { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Studio> Studios { get; set; }
    }
}
