using Caregiver.Dtos;
using Caregiver.Helpers;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using System.Reflection.Metadata;

namespace Caregiver.Repositories.Repository
{
	public class AdminRepo : IAdminRepo
	{
		private readonly UserManager<User> _userManager;
		private readonly string secretKey;

		public AdminRepo(UserManager<User> userManager, IConfiguration configuration)
		{
			secretKey = configuration.GetValue<string>("ApiSettings:secret");

			_userManager = userManager;
		}




		//public async Task<bool> AcceptRequest(CaregiverUser user)
		//{
		//	user.IsAccepted = true;

		//	var result = await _userManager.UpdateAsync(user);

		//	if (result.Succeeded) { return true; }
		//	else return false;
		//}

		public async Task<string> AcceptRequest(CaregiverUser user)
		{
			user.IsAccepted = true;

			var result = await _userManager.UpdateAsync(user);

			if (result.Succeeded) {
				string StringToken = await Handlers.GenerateToken(_userManager, secretKey, user);

				return StringToken; 
			
			}
			else return null;
		}

	}
}
