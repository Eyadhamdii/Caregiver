using System.ComponentModel.DataAnnotations.Schema;

namespace Caregiver.Models
{
	public class CaregiverPatientReservation
	{
		[ForeignKey("Caregiver")]
		public string CaregiverId { get; set; }
		public CaregiverUser Caregiver { get; set; }
		

		[ForeignKey("Patient")]
		public string PatientId { get; set; }
		public PatientUser Patient { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

	}
}
