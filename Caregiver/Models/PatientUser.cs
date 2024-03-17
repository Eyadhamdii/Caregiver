using Caregiver.Enums;
using System.ComponentModel.DataAnnotations;

namespace Caregiver.Models
{
	public class PatientUser : User
	{

		public ICollection<CaregiverPatientReservation> Reservations { get; set; } = null;

	}
}
