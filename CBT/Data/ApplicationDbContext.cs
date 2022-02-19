using CBT.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CBT.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
               base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users");

            builder.Entity<Doctor>()
                .HasMany(p => p.Patients)
                .WithOne(b => b.Doctor);

            builder.Entity<Patient>()
                .HasMany(p => p.Eximinations)
                .WithOne(b => b.Patient);

            builder.Entity<Eximination>()
                .HasMany(p => p.Treatments)
                .WithMany(e => e.Eximinations)
                .UsingEntity(j => j.ToTable("EximinationTreatments"));

        }
        public DbSet<Patient>Patients { get; set; }
        public DbSet<Patient> Doctors { get; set; }
        public DbSet<Patient> Eximinations { get; set; }
        public DbSet<Patient> Treatments { get; set; }


    }
}