using Caregiver.Dtos;

namespace Caregiver.Services
{
	public interface IUserService
	{
		Task<UserManagerResponse> RegisterUserAsync(RegisterDTO model);
		Task<LoginResDTO> Login(LoginReqDTO loginReqDTO);



	}
}
