using System.ComponentModel.DataAnnotations;
using System.Globalization;
using static Caregiver.Enums.Enums;

namespace Caregiver.Dtos
{

	public class RegisterPatientDTO

	{
		public string UserName { get; set; } = "no user name";

		[Required]
		[StringLength(40, MinimumLength = 3)]

		public string FirstName { get; set; }

		[Required]
		[StringLength(40, MinimumLength = 3)]
		public string LastName { get; set; }

		[Required]

		public Gender Gender { get; set; }


		public DateOnly Birthdate { get; set; }

		public string Nationality { get; set; }

		[Required]
		[EmailAddress]
		[StringLength(50)]
		public string Email { get; set; }

		[Required]

		public string Password { get; set; }

		[Required]

		public string ConfirmPassword { get; set; }

		[Required]
		public int PhoneNumber { get; set; }

	}
}
