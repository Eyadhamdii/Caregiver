﻿using System.ComponentModel.DataAnnotations;
using static Caregiver.Enums.Enums;

namespace Caregiver.Models
{
	public class CaregiverUser : User
	{
		// added this
		public string Bio { get; set; } = string.Empty;

		public string Country { get; set; } = string.Empty;

		public City City { get; set; }

		public CareerLevel CareerLevel { get; set; }

		public int YearsOfExperience { get; set; }

		public JobTitle JobTitle { get; set; }

		public City JobLocationLookingFor { get; set; }

		public string WhatCanYouDo { get; set; } = string.Empty;

		public int PricePerHour { get; set; }

		public int PricePerDay { get; set; }

		public byte[] Resume { get; set; }

		public byte[] Photo { get; set; }
		public byte[] CriminalRecords { get; set; }

		public ICollection<CaregiverPatientReservation> Reservations { get; set; } = null;
		public ICollection<CaregiverSchedule> CaregiverSchedules { get; set; } = null;
	}
}
