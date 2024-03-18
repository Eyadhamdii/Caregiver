using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Caregiver.Repositories.Repository
{
	public class CaregiverRepo : GenericRepo<CaregiverUser>, ICaregiverRepo
	{

		public CaregiverRepo(ApplicationDBContext db) : base(db)
		{

		}

	}
}
