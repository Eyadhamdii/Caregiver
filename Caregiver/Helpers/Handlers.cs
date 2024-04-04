using Caregiver.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Caregiver.Helpers
{
	public  static class Handlers
	{


        public static int CalculateAge(DateOnly birthDate)
		{
			DateOnly currentDate = DateOnly.FromDateTime(DateTime.Today);
			int age = currentDate.Year - birthDate.Year;

			// Check if the birthday has occurred this year
			DateOnly nextBirthday = birthDate.AddYears(age);
			if (nextBirthday > currentDate)
				age--;

			return age;
		}
		public static string DetermineStatus(CaregiverUser src)
		{
			if (!src.IsFormCompleted && !src.IsDeleted && !src.IsDeletedByAdmin && !src.IsAccepted)
			{
				return "form incomplete";
			}
			else if (src.IsFormCompleted && !src.IsDeleted && !src.IsDeletedByAdmin && !src.IsAccepted)
			{
				return "pending";
			}
			else if (src.IsFormCompleted && !src.IsDeleted && !src.IsDeletedByAdmin && src.IsAccepted)
			{
				return "active";
			}
			else if (src.IsFormCompleted && src.IsDeleted && !src.IsDeletedByAdmin && src.IsAccepted)
			{
				return "not active";
			}
			else if (src.IsDeletedByAdmin)
			{
				return "blocked";
			}
			else
			{
				return "";
			}
		}

		public static Claim DetermineStatusClaim(CaregiverUser src)
		{
			string status = DetermineStatus(src); // Assuming DetermineStatus is your existing method

			// Create a claim for the status
			return new Claim("Status", status);
		}


		public static async Task<string> GenerateToken( UserManager<User> _userManager,string secretKey,User user)
		{
			var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey);
			var key = new SymmetricSecurityKey(secretKeyInBytes);


			var userClaim = await _userManager.GetClaimsAsync(user);

			var cc = userClaim.FirstOrDefault();

			var tokenDescriptor = new SecurityTokenDescriptor
			{


				Subject = new ClaimsIdentity(new Claim[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				//new Claim(ClaimTypes.Role, user.GetType().ToString().Substring(user.GetType().ToString().LastIndexOf('.') + 1)),
				new Claim(cc.Type, cc.Value),
					user is CaregiverUser Careuser ? new Claim("Status", Helpers.Handlers.DetermineStatus(Careuser)) : null,

				//new Claim("Status", Helpers.Handlers.DetermineStatus(user))
				//Helpers.Handlers.DetermineStatusClaim(user)

			}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
			};




			var TokenHandler = new JwtSecurityTokenHandler();
			var token = TokenHandler.CreateToken(tokenDescriptor);
			var StringToken = TokenHandler.WriteToken(token);

			return StringToken;
		}

	}
}
