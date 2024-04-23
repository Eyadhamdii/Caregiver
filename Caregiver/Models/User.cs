using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using static Caregiver.Enums.Enums;

namespace Caregiver.Models
{
	public class User : IdentityUser
	{
		// Customer 

		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		//public Gender Gender { get; set; }
		public string Gender { get; set; }

		public DateOnly Birthdate { get; set; }

		public string Nationality { get; set; } = string.Empty;

		//public string EmailAddress { get; set; } = string.Empty;

		//public string Password { get; set; } = string.Empty;

		//public string ConfirmPassword { get; set; } = string.Empty;

		//public string PhoneNumber { get; set; }


		public bool IsDeleted { get; set; } = false;

		//public bool IsActive { get; set; } = true;//user can login again with the same account  => Reactive Your Account => isDeletedbyuser to false again... 
		public bool IsDeletedByAdmin { get; set; } = false; 
		//user must create a new account with new email => can't login with his account ...


		public DateTime JoinedDate { get; set; } = DateTime.Today;

	}
}
