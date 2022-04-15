﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Data
{

    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<CastMember>().HasKey(cm => new
            {
                cm.ActorNumber,
                cm.DVDNumber
            });

            builder.Entity<CastMember>().HasOne(cm => cm.Actor).WithMany(cm => cm.CastMembers).HasForeignKey(m => m.ActorNumber);
            builder.Entity<CastMember>().HasOne(cm => cm.DVDTitle).WithMany(cm => cm.CastMembers).HasForeignKey(m => m.DVDNumber);

            base.OnModelCreating(builder);
        }
    }
}
