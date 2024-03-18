using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Caregiver.Enums.Enums;

namespace Caregiver.Models
{
    public class CaregiverSchedule
    {
        [Key]
        [ForeignKey("CaregiverUser")]
        public string CaregiverId { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }

        public Status Status { get; set; }
        public CaregiverUser CaregiverUser { get; set; }
    }
}
