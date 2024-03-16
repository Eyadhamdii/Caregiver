using Caregiver.Dtos;

namespace Caregiver.Services
{
	public interface IUserService
	{
		Task<UserManagerResponse> RegisterUserAsync(RegisterCustomerDTO model);

        Task<UserManagerResponse> RegisterCaregiverAsync(RegisterCaregiverDTO model);

        Task<LoginResDTO> Login(LoginReqDTO loginReqDTO);



	}
}
