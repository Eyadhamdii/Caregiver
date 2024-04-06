using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Caregiver.Enums.Enums;

namespace Caregiver.Models
{
    public class Dependant
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public string EmailAddress { get; set; }
        public string Gender { get; set; }
        public string ReservationNotes { get; set; }

        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        public PatientUser Patient { get; set; }

        //chage
        public  int ReservationNo { get; set; }
    }
}
