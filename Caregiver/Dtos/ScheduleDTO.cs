using static Caregiver.Enums.Enums;

namespace Caregiver.Dtos
{
	public class ScheduleDTO
	{
		//public DateOnly Day { get; set; }
		public DateTime FromTime { get; set; }
		public DateTime ToTime { get; set; }

		public string NurseId { get; set; }
		public Status Status { get; set; }
	}
}
