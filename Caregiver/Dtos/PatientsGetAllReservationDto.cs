using static Caregiver.Enums.Enums;

namespace Caregiver.Dtos
{
    public class PatientsGetAllReservationDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PatientId { get; set; }
        public ReservationStatus Status { get; set; }
        public int OrderId { get; set; }
        public byte[] Photo { get; set; }
        public int TotalPrice { get; set; }
    }
}
