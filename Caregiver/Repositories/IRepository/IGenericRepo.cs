using Caregiver.Enums;
using Caregiver.Models;
using System.Linq.Expressions;

namespace Caregiver.Repositories.IRepository
{
	public interface IGenericRepo<T>
	{

		Task<T> GetAsync(Expression<Func<T, bool>> filter = null);
		Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);

	}
}
