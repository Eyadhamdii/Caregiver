using Caregiver.Enums;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Caregiver.Dtos
{

    public class RegisterCustomerDTO

    {
        [Required]
        [StringLength(50, MinimumLength = 3)]

        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]

        public Gender Gender { get; set; }

        [Required]

        public DateTime Birthdate { get; set; }

        public string Nationality { get; set; } 

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50 , MinimumLength =5)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }

        [Required]
        public int PhoneNumber { get; set; }

    }
}
