namespace Caregiver.Dtos
{
	public class AdminCaregiverDTO
	{
        public string Id { get; set; }
		public string Photo { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string JobTitle { get; set; }

		public int PricePerDay { get; set; }
        public int TotalCustomers { get; set; }
        public int  TotalRevenu { get; set; }
        public int OngoingOrders { get; set; }
        public int CompletedOrders { get; set; }
        public int CanceledOrders { get; set; }
        public string JoinedDate { get; set; }
        public string Status { get; set; }
    }
}
