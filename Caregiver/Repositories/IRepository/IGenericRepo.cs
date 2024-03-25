using Caregiver.Enums;
using Caregiver.Models;
using System.Linq.Expressions;

namespace Caregiver.Repositories.IRepository
{
	public interface IGenericRepo<T>
	{

		Task<T> GetAsync(Expression<Func<T, bool>> filter = null);
		Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);

		Task<bool> SoftDeleteUser(User user);

		Task<User> UpdateUserAsync(User user);
		//admin
		Task<List<T>> GetAllWithNavAsync(
				 string[] includes, Expression<Func<T, bool>> filter = null);
	}
}
