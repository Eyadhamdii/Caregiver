using Caregiver.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection;

namespace Caregiver.Models
{
	public class User : IdentityUser
	{
        // Customer 
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Gender Gender { get; set; }

        public DateTime Birthdate { get; set; }

        public string Nationality { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;

        public int PhoneNumber {  get; set; }


        // Nurse 

        public string Bio {  get; set; } = string.Empty;

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

        public byte[] CriminalRecords { get; set; }






    }
}
