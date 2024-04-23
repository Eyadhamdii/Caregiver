using System.ComponentModel.DataAnnotations.Schema;

namespace Caregiver.Models
{
    public class ReservationDates
    {
        [ForeignKey("CaregiverPatientReservation")]
        public int OrderId { get; set; }
        public DateTime ReservationDate { get; set;}

        [ForeignKey("Caregiver")]
        public string CaregiverId { get; set; }
        public CaregiverUser Caregiver { get; set; }
        public CaregiverPatientReservation CaregiverPatientReservation { get; set; }
        
    }
}
