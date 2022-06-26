﻿using CBT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CBT.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        { 
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users", "Security");
            builder.Entity<IdentityRole>().ToTable("Roles", "Security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("Roleclaims ", "Security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Security");

           

            #region old code for relation
            /*
            builder.Entity<Patient>(p => { 
                p.HasOne<Doctor>()
                .WithMany()
                .HasForeignKey(x => x.doctor_id).IsRequired();
       
            });


            builder.Entity<Eximination>(ex => {
                ex.HasOne<Patient>()
                .WithMany()
                .HasForeignKey(s => s.patient_Id).IsRequired();
            });

            builder.Entity<Patient>(pp =>
            {
                pp.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(s => s.UserId).IsRequired();
            });


            builder.Entity<Eximination>()
                .HasMany(p => p.Treatments)
                .WithMany(e => e.Eximinations)
                .UsingEntity(j => j.ToTable("EximinationTreatments"));*/
            #endregion

        }

        public  DbSet<Patient>Patients { get; set; }
        public  DbSet<Doctor> Doctors { get; set; }
        public  DbSet<Eximination> Eximinations { get; set; }
        public  DbSet<Treatment> Treatments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-08TND6D;Initial Catalog=CBT_DB;Integrated Security=True; MultipleActiveResultSets=true");
        }

    }
}