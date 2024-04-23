using static Caregiver.Enums.Enums;
using System.ComponentModel.DataAnnotations;

namespace Caregiver.Dtos
{
    public class CaregiverUpdateDTO
    {
        public string Bio { get; set; }

        [Required]

        public string Country { get; set; }

        [Required]

        public City City { get; set; }
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
        public int PhoneNumber { get; set; }
        [Required]

        public CareerLevel CareerLevel { get; set; }

        [Required]

        public int YearsOfExperience { get; set; }
        [Required]

        public JobTitle JobTitle { get; set; }
        //[Required]

        //public int PricePerHour { get; set; }
        [Required]

        public int PricePerDay { get; set; }


    }
}
