using static Caregiver.Enums.Enums;
using System.ComponentModel.DataAnnotations;

namespace Caregiver.Dtos
{
	public class FormCaregiverDTO
	{
		public string[] WhatCanCaregiverDo { get; set; }
		[Required]

		public string Country { get; set; }

		[Required]

		public City City { get; set; }

		[Required]

		public CareerLevel CareerLevel { get; set; }

		[Required]

		public int YearsOfExperience { get; set; }
		[Required]

		public JobTitle JobTitle { get; set; }
		[Required]

		public City JobLocationLookingFor { get; set; }
		[Required]

		public string WhatCanYouDo { get; set; } = string.Empty;
		[Required]

		public int PricePerHour { get; set; }
		[Required]

		public int PricePerDay { get; set; }
		[Required]

		public IFormFile Resume { get; set; }
		[Required]
		public IFormFile CriminalRecords { get; set; }

	}
}
