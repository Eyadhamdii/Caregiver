using System.ComponentModel.DataAnnotations;
using static Caregiver.Enums.Enums;

namespace Caregiver.Dtos
{
	public class RegisterCaregiverDTO
	{
		[Required]
		[StringLength(50, MinimumLength = 3)]

		public string FirstName { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 3)]
		public string LastName { get; set; }

		public string Bio { get; set; }

		[Required]
		public Gender Gender { get; set; }

		[Required]
		public DateOnly Birthdate { get; set; }

		public string Nationality { get; set; }

		[Required]
		[EmailAddress]
		[StringLength(50)]
		public string Email { get; set; }

		[Required]
		[StringLength(40, MinimumLength = 5)]
		public string Password { get; set; }

		[Required]
		[StringLength(40, MinimumLength = 5)]
		public string ConfirmPassword { get; set; }

		[Required]
		public int PhoneNumber { get; set; }
		public string Country { get; set; }

		public City City { get; set; }


		public CareerLevel CareerLevel { get; set; }

		public int YearsOfExperience { get; set; }

		public JobTitle JobTitle { get; set; }

		public City JobLocationLookingFor { get; set; }

		public string WhatCanYouDo { get; set; } = string.Empty;

		public int PricePerHour { get; set; }

		public int PricePerDay { get; set; }
		public IFormFile Resume { get; set; }
		public IFormFile CriminalRecords { get; set; }

		public string[] WhatCanCaregiverDo { get; set; }
	}
}
