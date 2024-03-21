using System.ComponentModel.DataAnnotations;
using static Caregiver.Enums.Enums;

namespace Caregiver.Models
{
	public class CaregiverUser : User
	{
		// added this
		public string Bio { get; set; } = string.Empty;

		public string Country { get; set; } = string.Empty;

		//public City City { get; set; }

		//public CareerLevel CareerLevel { get; set; }

		public string City { get; set; }

		public string CareerLevel { get; set; }
		public int YearsOfExperience { get; set; }

		//public JobTitle JobTitle { get; set; }

		//public City JobLocationLookingFor { get; set; }
		public string JobTitle { get; set; }

		public string JobLocationLookingFor { get; set; }


		public string WhatCanYouDo { get; set; } = string.Empty;

		public int PricePerHour { get; set; }

		public int PricePerDay { get; set; }

		public byte[] Resume { get; set; }

		public byte[] CriminalRecords { get; set; }

		//public bool IsAccepted { get; set; } = false;
		public string[] WhatCanCaregiverDo { get; set; } = Array.Empty<string>();
		public ICollection<CaregiverPatientReservation> Reservations { get; set; } = null;
	}
}
