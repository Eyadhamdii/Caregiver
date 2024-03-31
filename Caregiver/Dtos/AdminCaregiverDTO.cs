namespace Caregiver.Dtos
{
	public class AdminCaregiverDTO
	{
        public string Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public int PricePerDay { get; set; }
        public int TotalCustomers { get; set; }
        public int  TotalRevenu { get; set; }
        public int OngoingOrders { get; set; }
        public int CompletedOrders { get; set; }
        public int CanceledOrders { get; set; }
        public DateTime JoinedDate { get; set; }
        public string Status { get; set; }
    }
}
