using Caregiver.Models;
using static Caregiver.Enums.Enums;

namespace Caregiver.Dtos
{
    public class ReservationDto
    {

        public string NurseFirstName { get; set; } = string.Empty; 
        public string NurseLastName { get; set; } = string.Empty;

        public string PatientFirstName { get; set; } = string.Empty; 
        public string PatientLastName { get; set; } = string.Empty;
        public Gender Gender { get; set; }

        public string CaregiverEmailAddress { get; set; } = string.Empty;
        public string PatientEmailAddress { get; set; } = string.Empty;

        public int PhoneNumber { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PatientId { get; set; }
        public ReservationStatus Status { get; set; }
        public int OrderId { get; set; }
        public int TotalPrice { get; set; }
    }
}
