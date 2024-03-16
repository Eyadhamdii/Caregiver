using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Caregiver.Models
{
	public class ApplicationDBContext : IdentityDbContext<User>
	{

		public ApplicationDBContext(DbContextOptions options) : base(options)
		{

		}
		public DbSet<User> Users { get; set; }
	}
}
