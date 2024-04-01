using static Caregiver.Enums.Enums;

namespace Caregiver.Dtos
{
	public class CaregiverDataDTO
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public string Bio { get; set; }
		public string Country { get; set; }

		public string City { get; set; }
		public string Gender { get; set; }

		public DateOnly Birthdate { get; set; }

		public string Nationality { get; set; }
		public string Email { get; set; }
		[Required]
		public int PhoneNumber { get; set; }

		public string CareerLevel { get; set; }
		public int YearsOfExperience { get; set; }

		public string JobTitle { get; set; }
		public int PricePerDay { get; set; }
		public string Resume { get; set; }
		public string Photo { get; set; }

		public string CriminalRecords { get; set; }

	}
}
