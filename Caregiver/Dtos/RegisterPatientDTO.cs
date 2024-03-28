using System.ComponentModel.DataAnnotations;
using System.Globalization;
using static Caregiver.Enums.Enums;

namespace Caregiver.Dtos
{

	public class RegisterPatientDTO

	{
		public string UserName { get; set; } = "no user name";

		[Required]
		[StringLength(50, MinimumLength = 3)]

		public string FirstName { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 3)]
		public string LastName { get; set; }

		[Required]

		public Gender Gender { get; set; }

		[Required]

		public DateOnly Birthdate { get; set; }

		public string Nationality { get; set; }

		[Required]
		[EmailAddress]
		[StringLength(50)]
		public string Email { get; set; }
       // public int Age { get; set; }

        [Required]
		[StringLength(50, MinimumLength = 5)]
		public string Password { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 5)]
		public string ConfirmPassword { get; set; }

		[Required]
		[RegularExpression(@"^01.*", ErrorMessage = "Phone number must start with '01'")]

		public string PhoneNumber { get; set; }

	}
}
