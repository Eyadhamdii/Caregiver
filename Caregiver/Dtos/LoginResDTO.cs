namespace Caregiver.Dtos
{
	public class LoginResDTO
	{
		public UserDTO User { get; set; }
		//public string Role { get; set; }
		public string Token { get; set; }
		public string Type { get; set; }

		//disscrimator -- type of user .. 
	}
}
