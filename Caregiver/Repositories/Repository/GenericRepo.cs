using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Caregiver.Repositories.Repository
{
	public class GenericRepo<T> : IGenericRepo<T> where T : class
	{
		private readonly ApplicationDBContext _db;
		private readonly DbSet<T> _dbSet;
		private readonly UserManager<User> _userManager;

		public GenericRepo(ApplicationDBContext db, UserManager<User> userManager)
		{
			_db = db;
			_dbSet = _db.Set<T>();
			_userManager = userManager;
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

		public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
		{
			IQueryable<T> query = _dbSet;
			if (filter != null)
			{
				query = query.Where(filter);
			}
			return await query.ToListAsync();
		}



		public async Task<bool> SoftDeleteUser(User user)
		{
			user.IsDeleted = true;
			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded) return true;
			else return false;
		}


		public async Task<User> UpdateUserAsync(User user)
		{

			var result = await _userManager.UpdateAsync(user);
			//db.save changes could work but usermanager is better for 
			if (result.Succeeded)
			{
				return user ;
			}
			return null;
		}

		public  List<T> GetAllWithNavAsync(
				 string includes, Expression<Func<T, bool>> filter = null)
		{
			
			IQueryable<T> query = _dbSet;
			

			// Include navigation properties
			if (includes!= null)
			{
				query = query.Include(includes);
			}
			if (filter != null)
			{
				query = query.Where(filter);
			}

			return  query.ToList();
		}



		public async Task<bool> HardDeleteUser(User user)
		{

			var result = await _userManager.DeleteAsync(user);
			if (result.Succeeded)
			{
				return true;
			}
			return false;
		}


		public async Task<bool> AdminDeleteUser(User user)
		{
			user.IsDeletedByAdmin = true;
			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded) return true;
			else return false;
		}



		//if user contact us and want to get back his account again
			public async Task<bool> AdminReturnUser(User user)
		{
			user.IsDeletedByAdmin = false;
			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded) return true;
			else return false;
		}



	}
}
