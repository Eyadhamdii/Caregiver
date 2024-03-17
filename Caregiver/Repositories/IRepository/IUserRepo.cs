using Caregiver.Dtos;

namespace Caregiver.Repositories.IRepository
{
    public interface IUserRepo
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterPatientDTO model);

        Task<UserManagerResponse> RegisterCaregiverAsync(RegisterCaregiverDTO model);

        Task<LoginResDTO> LoginAsync(LoginReqDTO loginReqDTO);



    }
}
