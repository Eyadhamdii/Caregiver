namespace Caregiver.Dtos
{
	public class GetCustomerDTO
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
        public string Location { get; set; }
		public string Email { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
		public DateOnly Birthdate { get; set; }
		public int PhoneNumber { get; set; }

	}
}
