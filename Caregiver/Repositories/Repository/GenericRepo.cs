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






	}
}
