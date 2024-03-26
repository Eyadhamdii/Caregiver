using Caregiver.Dtos;

namespace Caregiver.Repositories.IRepository
{
	public interface IUserRepo
	{
		Task<string> ForgotPassword( string email);
		Task<string> UpdateForgottenPassword(string id, string resetToken, string newPassword);
		Task<UserManagerResponse> RegisterUserAsync(RegisterPatientDTO model);

		Task<UserManagerResponse> RegisterCaregiverAsync(RegisterCaregiverDTO model);

		Task<UserManagerResponse> FormCaregiverAsync(FormCaregiverDTO model);

		Task<LoginResDTO> LoginAsync(LoginReqDTO loginReqDTO);

        Task<UserManagerResponse> PersonalDetailsAsync(PersonalDetailsDTO model);



    }
}
