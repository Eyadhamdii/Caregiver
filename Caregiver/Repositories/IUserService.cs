using Caregiver.Dtos;

namespace Caregiver.Services
{
	public interface IUserService
	{
		Task<UserManagerResponse> RegisterUserAsync(RegisterPatientDTO model);

		Task<UserManagerResponse> RegisterCaregiverAsync(RegisterCaregiverDTO model);

		Task<LoginResDTO> LoginAsync(LoginReqDTO loginReqDTO);



	}
}
