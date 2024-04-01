using Caregiver.Dtos;

namespace Caregiver.Services.IService
{
	public interface IAdminService
	{

		Task<List<AdminCaregiverDTO>> GetAllCaregivers();
		Task<List<AdminCaregiverDTO>> GetRequested();
		Task<List<AdminCaregiverDTO>> GetCaregiversJobTitle(string title);
		Task<bool> AcceptRequestAsync(string id);
		Task<bool> HardDeleteCaregiver(string id);
		Task<bool> SoftDeleteCaregiver(string id);
		Task<bool> AdminDeleteCaregiver(string id);
		Task<bool> AdminReturnCaregiver(string id);
	}
}
