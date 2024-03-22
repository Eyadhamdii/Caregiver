using Caregiver.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Caregiver.Enums.Enums;


namespace Caregiver.Models
{
    public class CaregiverSchedule
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CaregiverUser")]
        public string CaregiverId { get; set; }
        public DateOnly Day { get; set; }
        public int FromTime { get; set; }
        public int ToTime { get; set; }
        public Status Status { get; set; }
        public CaregiverUser CaregiverUser { get; set; }
    }
}
