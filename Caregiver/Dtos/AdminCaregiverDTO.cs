namespace Caregiver.Dtos
{
	public class AdminCaregiverDTO
	{

		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public int PricePerDay { get; set; }
		public DateTime JoinedDate { get; set; }
	}
}
