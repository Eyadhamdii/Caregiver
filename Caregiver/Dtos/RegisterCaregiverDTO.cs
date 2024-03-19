using System.ComponentModel.DataAnnotations;
using static Caregiver.Enums.Enums;

namespace Caregiver.Dtos
{
    public class RegisterCaregiverDTO
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]

        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        public string Bio {  get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public DateOnly Birthdate { get; set; }

        public string Nationality { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }

        [Required]
        public int PhoneNumber { get; set; }

        [Required]

        public string Country {  get; set; }

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

		[Required]
		public IFormFile Photo { get; set; }
	}
}
