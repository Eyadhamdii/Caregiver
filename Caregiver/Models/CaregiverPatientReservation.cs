using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using static Caregiver.Enums.Enums;

namespace Caregiver.Models
{
    public class CaregiverPatientReservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

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
        public DateTime LastStatusUpdate { get; set; }

        public string Status { get; set; }

        public int TotalPrice { get; set; }
        public double TotalPriceWithfees { get; set;}
        public double Fees { get; set; }

        public string PaymentStatus { get; set; }

		[ForeignKey("Dependant")]
		public int DependentId { get; set; }
		public Dependant Dependant { get; set; }


	}
}
