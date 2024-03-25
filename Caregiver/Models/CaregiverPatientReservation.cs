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

		[Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

	}
}
