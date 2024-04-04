using Caregiver.Dtos;
using Caregiver.Models;

namespace Caregiver.Services.IService
{
    public interface ICaregiverService
	{
		 Task<IEnumerable<CaregiverCardDTO>> GetAllCurrentCaregiver();
		Task<IEnumerable<CaregiverCardDTO>> GetAllCaregiverByType(string Role);
		Task<CaregiverDataDTO> GetCaregiverById(string id);
		Task<CaregiverUpdateDTO> UpdateCaregiverAsync(string id, CaregiverUpdateDTO caregiverUpdate);
		Task<bool> SoftDeleteCaregiver(string id);

	}
}
