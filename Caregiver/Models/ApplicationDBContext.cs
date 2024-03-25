using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Caregiver.Models
{
	public class ApplicationDBContext : IdentityDbContext<User>
	{
		public DbSet<CaregiverPatientReservation> Reservations { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<CaregiverUser> Caregivers { get; set; }
		public DbSet<PatientUser> Patients { get; set; }
        public DbSet<Dependant> Dependants { get; set; }

        public ApplicationDBContext(DbContextOptions options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			//if i want to remove the username from the table..
			//builder.Entity<ApplicationUser>().Ignore(u => u.UserName);


			//composite pk.. 
			base.OnModelCreating(builder);
			builder.Entity<CaregiverPatientReservation>()
		    .HasKey(p => new { p.PatientId, p.CaregiverId });


			builder.Entity<CaregiverPatientReservation>()
	   .HasOne(pn => pn.Patient)
	   .WithMany(p => p.Reservations)
	   .HasForeignKey(pn => pn.PatientId);


			builder.Entity<CaregiverPatientReservation>()
	   .HasOne(pn => pn.Caregiver)
	   .WithMany(p => p.Reservations)
	   .HasForeignKey(pn => pn.CaregiverId);


		}
		//public DbSet<User> Users { get; set; }
	}
}
