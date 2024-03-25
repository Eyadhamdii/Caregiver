using Azure;
using Caregiver.Dtos.UpdateDTOs;
using Caregiver.Enums;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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




	}
}
