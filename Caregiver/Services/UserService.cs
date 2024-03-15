using Caregiver.Dtos;
using Caregiver.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Caregiver.Services
{
	public class UserService : IUserService
	{
		private UserManager<IdentityUser> _userManager;
		private readonly string secretKey;
		private readonly ApplicationDBContext _db;

		public UserService(UserManager<IdentityUser> userManager, ApplicationDBContext db, IConfiguration configuration)
		{
			_db = db;
			_userManager = userManager;
			secretKey = configuration.GetValue<string>("ApiSettings:secret");

		}


		public async Task<LoginResDTO> Login(LoginReqDTO loginReqDTO)
		{

			var user = await _userManager.FindByEmailAsync(loginReqDTO.Email);
			//var user = _db.Users.FirstOrDefault(u => u.UserName.ToLower() == loginReqDTO.UserName.ToLower());
			bool isValid = await _userManager.CheckPasswordAsync(user, loginReqDTO.Password);



			if (user == null || isValid == false)
			{
				//return null;
				return new LoginResDTO()
				{
					Token = "",
					User = null

				};
			}

			//if found
			//var roles = await _userManager.GetRolesAsync(user);
			//var Role = roles.FirstOrDefault();

			//if found generate token... 

			//key 
			var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey);
			var key = new SymmetricSecurityKey(secretKeyInBytes);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				//new Claim(ClaimTypes.Role, Role),
			}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
			};

			var TokenHandler = new JwtSecurityTokenHandler();
			var token = TokenHandler.CreateToken(tokenDescriptor);
			var StringToken = TokenHandler.WriteToken(token);
			LoginResDTO loginResDTO = new LoginResDTO()
			{
				Token = StringToken,
				//Role = Role,
				User = new UserDTO
				{

					ID = user.Id,
					UserName = user.UserName

				}
			};
			return loginResDTO;

		}


		public async Task<UserManagerResponse> RegisterUserAsync(RegisterDTO model)
		{
			if (model == null)
				throw new NullReferenceException("Reigster Model is null");

			if (model.Password != model.ConfirmPassword)
				return new UserManagerResponse
				{
					Message = "Confirm password doesn't match the password",
					IsSuccess = false,
				};

			var identityuser = new IdentityUser
			{
				Email = model.Email,
				UserName = model.Email,
			};

			var result = await _userManager.CreateAsync(identityuser, model.Password);

			if (result.Succeeded)
				return new UserManagerResponse
				{
					Message = "User created successfully!",
					IsSuccess = true,
				};
			return new UserManagerResponse
			{
				Message = "User did not create",
				IsSuccess = false,
				Errors = result.Errors.Select(e => e.Description)
			};
		}
	}
}