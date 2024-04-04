namespace Caregiver.Dtos
{
	public class UpdatePasswordDTO
	{
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
