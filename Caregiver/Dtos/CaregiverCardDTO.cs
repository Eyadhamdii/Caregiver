using static Caregiver.Enums.Enums;

namespace Caregiver.Dtos
{
	public class CaregiverCardDTO
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public string JobTitle { get; set; }
		public string Country { get; set; }

		public string City { get; set; }
		public int PricePerDay { get; set; }

	}
}
