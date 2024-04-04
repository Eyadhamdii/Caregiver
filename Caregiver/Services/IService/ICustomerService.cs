using Caregiver.Dtos;
using Caregiver.Models;

namespace Caregiver.Services.IService
{
	public interface ICustomerService
	{
		Task<IEnumerable<GetCustomerDTO>> GetAllCurrentCustomer();
		Task<PatientUser> GetCustomerById(string id);
	    Task<GetCustomerDTO> UpdateCustomerAsync(string id, GetCustomerDTO  CustomerUpdate);
		Task<bool> SoftDeleteCustomer(string id);

        Task <DependantDetailsDTO> GetDependantDetails();
       
    }
}
