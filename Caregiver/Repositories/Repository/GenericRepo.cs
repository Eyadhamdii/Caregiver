using Caregiver.Enums;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Caregiver.Repositories.Repository
{
	public class GenericRepo : IGenericRepo
	{
		private readonly ApplicationDBContext _db;

		public GenericRepo(ApplicationDBContext db)
		{
			_db = db;
		}


		public async Task<List<User>> getUsersByDiscriminatorAsync(string Discriminator = null)
		{
			IQueryable<User> query = _db.Users;


			if (Discriminator != null)
			{
				query = query.Where(e => EF.Property<string>(e, "Discriminator") == Discriminator);
			}

			List<User> Users = await query.ToListAsync();
			return Users;

		}


		//public async Task<List<CaregiverUser>> GetPatients()
		//{
		//	var query = await _db.Caregivers.FirstOrDefaultAsync(a => a.Nationality == "jkjbh");

		//	//List<CaregiverUser> Users = await query.ToListAsync(); ;
		//	return Users;

		//}

		public async Task<List<CaregiverUser>> GetPatients()
		{
			var query = _db.Caregivers.Where(a => a.Nationality == "jkjbh");

			//List<CaregiverUser> Users = await query.ToListAsync(); ;
			return query.ToList();

		}


	}
}
