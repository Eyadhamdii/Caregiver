using Caregiver.Models;

namespace Caregiver.Repositories.IRepository
{
	public interface IAdminRepo
	{
		Task<bool> AcceptRequest(CaregiverUser user);
	}
}
