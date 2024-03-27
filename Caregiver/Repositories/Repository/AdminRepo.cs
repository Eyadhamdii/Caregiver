using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

namespace Caregiver.Repositories.Repository
{
	public class AdminRepo : IAdminRepo
	{
		private readonly UserManager<User> _userManager;

		public AdminRepo(UserManager<User> userManager)
		{
			_userManager = userManager;
		}




		public async Task<bool> AcceptRequest(CaregiverUser user)
		{
			user.IsAccepted = true;
			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded) return true;
			else return false;
		}


	}
}
