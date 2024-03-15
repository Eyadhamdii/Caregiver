using System.ComponentModel.DataAnnotations;

namespace Caregiver.Dtos
{
    public class RegisterDTO
    {
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
    }
}
