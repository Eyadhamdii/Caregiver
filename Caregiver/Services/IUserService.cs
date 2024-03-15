using Caregiver.Dtos;

namespace Caregiver.Services
{
    public interface IUserService
    {
         Task<UserManagerResponse> RegisterUserAsync(RegisterDTO model);

    }
}
