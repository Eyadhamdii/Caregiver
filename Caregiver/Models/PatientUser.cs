using Caregiver.Enums;
using System.ComponentModel.DataAnnotations;

namespace Caregiver.Models
{
	public class PatientUser : User
	{
        public int  Age { get; set; }
        public string Location { get; set; }
        public string ReservationNotes { get; set; }
        public ICollection<CaregiverPatientReservation> Reservations { get; set; } = null;
        public ICollection<Dependant> Dependants { get; set; } = null;

    }
}
