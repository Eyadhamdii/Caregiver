using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Caregiver.Models
{
	public class ApplicationDBContext : IdentityDbContext<User>
	{
		public DbSet<CaregiverPatientReservation> Reservations { get; set; }

		public ApplicationDBContext(DbContextOptions options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
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
