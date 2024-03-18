using Caregiver.Enums;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Caregiver.Repositories.Repository
{
	public class GenericRepo<T> : IGenericRepo<T> where T : class
	{
		private readonly ApplicationDBContext _db;
		private readonly DbSet<T> _dbSet;

		public GenericRepo(ApplicationDBContext db)
		{
			_db = db;
			_dbSet = _db.Set<T>();

		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null)
		{
			IQueryable<T> query = _dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}
			return await query.FirstOrDefaultAsync();
		}

		public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
		{
			IQueryable<T> query = _dbSet;
			if (filter != null)
			{
				query = query.Where(filter);
			}
			return await query.ToListAsync();
		}



		//public async Task<List<User>> getUsersByDiscriminatorAsync(string Discriminator = null)
		//{
		//	IQueryable<User> query = _db.Users;


		//	if (Discriminator != null)
		//	{
		//		query = query.Where(e => EF.Property<string>(e, "Discriminator") == Discriminator);
		//	}

		//	List<User> Users = await query.ToListAsync();
		//	return Users;

		//}


		//public async Task<List<CaregiverUser>> GetPatients()
		//{
		//	var query = await _db.Caregivers.FirstOrDefaultAsync(a => a.Nationality == "jkjbh");

		//	//List<CaregiverUser> Users = await query.ToListAsync(); ;
		//	return Users;

		//}

		//public async Task<List<CaregiverUser>> GetPatients()
		//{
		//	var query = _db.Caregivers.Where(a => a.Nationality == "jkjbh");

		//	//List<CaregiverUser> Users = await query.ToListAsync(); ;
		//	return query.ToList();

		//}


	}
}
