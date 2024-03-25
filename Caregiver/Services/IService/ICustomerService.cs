using Caregiver.Dtos;
using Caregiver.Models;

namespace Caregiver.Services.IService
{
	public interface ICustomerService
	{
		Task<IEnumerable<GetCustomerDTO>> GetAllCurrentCustomer();
		Task<PatientUser> GetCustomerById(string id);
	//	Task<CaregiverUpdateDTO> UpdateCaregiverAsync(string id, CaregiverUpdateDTO caregiverUpdate);
		Task<bool> SoftDeleteCustomer(string id);
	}
}
