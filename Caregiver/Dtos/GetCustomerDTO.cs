namespace Caregiver.Dtos
{
	public class GetCustomerDTO
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Gender { get; set; }
		public DateOnly Birthdate { get; set; }
		public int PhoneNumber { get; set; }

	}
}
