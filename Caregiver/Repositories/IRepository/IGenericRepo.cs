using Caregiver.Enums;
using Caregiver.Models;

namespace Caregiver.Repositories.IRepository
{
	public interface IGenericRepo
	{

		Task<List<User>> getUsersByDiscriminatorAsync(string Discriminator = null);
		Task<List<CaregiverUser>> GetPatients();
	}
}
