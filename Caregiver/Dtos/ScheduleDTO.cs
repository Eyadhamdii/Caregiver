using static Caregiver.Enums.Enums;

namespace Caregiver.Dtos
{
	public class ScheduleDTO
	{
		//public DateOnly Day { get; set; }
		public DateOnly Day { get; set; }
		public int FromTime { get; set; }
		public int ToTime { get; set; }
		public string NurseId { get; set; }
		public Status Status { get; set; }
	}
}
