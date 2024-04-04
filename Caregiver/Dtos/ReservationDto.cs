using Caregiver.Models;
using static Caregiver.Enums.Enums;

namespace Caregiver.Dtos
{
    public class ReservationDto
    {
        public int OrderId { get; set; }

        public string CaregiverFirstName { get; set; } = string.Empty; 
        public string CaregiverLastName { get; set; } = string.Empty;

        public string PatientFirstName { get; set; } = string.Empty; 
        public string PatientLastName { get; set; } = string.Empty;
		//    public Gender Gender { get; set; }
		public string CaregiverGender { get; set; }
        public string PatientGender { get; set; }

        public string CaregiverEmailAddress { get; set; } = string.Empty;
        public string PatientEmailAddress { get; set; } = string.Empty;
        public string CaregiverPhoneNumber { get; set; }
        public string PatientPhoneNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PatientId { get; set; }
        public string Status { get; set; }

        public int TotalPrice { get; set; }
        public byte[] Photo { get; set; }

        public int PricePerDay { get; set; }
        public string Country { get; set; } = string.Empty;

        public City City { get; set; }
        public DateOnly Birthdate { get; set; }

        public double TotalPriceWithfees { get; set; }
        public double Fees { get; set; }
        public string JobTitle { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }


    }
}
