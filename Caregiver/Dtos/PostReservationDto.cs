using Caregiver.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static Caregiver.Enums.Enums;

namespace Caregiver.Dtos
{
    public class PostReservationDto
    {
        //[JsonIgnore]
        //public string CaregiverId { get; set; }
        //[JsonIgnore]
        //public string PatientId { get; set; }
        //
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int DependentId { get; set; }

        //[JsonIgnore]
        //public ReservationStatus Status { get; set; }
        //[JsonIgnore]
        //public int totalPrice { get; set; }
    }
}
