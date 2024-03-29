using Caregiver.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caregiver.Helpers
{
	public  static class Handlers
	{
       


		public static int CalculateAge(DateOnly birthDate)
		{
			DateOnly currentDate = DateOnly.FromDateTime(DateTime.Today);
			int age = currentDate.Year - birthDate.Year;

			// Check if the birthday has occurred this year
			DateOnly nextBirthday = birthDate.AddYears(age);
			if (nextBirthday > currentDate)
				age--;

			return age;
		}


	}
}
