﻿using Caregiver.Enums;
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

		public int PhoneNumber { get; set; }






	}
}
