using Caregiver.Dtos;
using Caregiver.Dtos.UpdateDTOs;
using Caregiver.Models;

namespace Caregiver.Services.IService
{
	public interface ICaregiverService
	{
		 Task<IEnumerable<CaregiverCardDTO>> GetAllCurrentCaregiver();
		Task<IEnumerable<CaregiverCardDTO>> GetAllCaregiverByType(string Role);
		Task<CaregiverUser> GetCaregiverById(string id);
		Task<CaregiverUpdateDTO> UpdateCaregiverAsync(string id, CaregiverUpdateDTO caregiverUpdate);
		Task<bool> SoftDeleteCaregiver(string id);
		//
		//

	}
}
