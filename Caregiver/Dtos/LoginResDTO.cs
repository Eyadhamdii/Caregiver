namespace Caregiver.Dtos
{
	public class LoginResDTO
	{
		public UserDTO User { get; set; }
		//public string Role { get; set; }
		public string Token { get; set; }

        //public string Status { get; set; }
	public	bool isFormCompleted { get; set; }
	public	bool isAccepted { get; set; }
	public	bool isBlocked { get; set; }
	public	bool isDeactivated { get; set; }
		

		//disscrimator -- type of user .. 
	}
}
