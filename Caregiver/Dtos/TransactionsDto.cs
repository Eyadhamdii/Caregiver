namespace Caregiver.Dtos
{
    public class TransactionsDto
    {
        public string PatientId { get; set; }
        public DateTime LastStatusUpdate { get; set; }

        public string Status { get; set; }

        public double TotalPriceWithfees { get; set; }
    }
}
