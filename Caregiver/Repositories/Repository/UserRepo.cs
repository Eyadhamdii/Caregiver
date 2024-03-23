using AutoMapper;
using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Caregiver.Repositories.Repository
{
	public class UserRepo : IUserRepo
	{
		private UserManager<User> _userManager;
		private readonly string secretKey;
		private readonly ApplicationDBContext _db;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
        private new List<string> _allowedExt = new List<string> { ".jpg", ".png", ".pdf" };
		private readonly IEmailService _emailService;




		public UserRepo(UserManager<User> userManager, ApplicationDBContext db, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IEmailService emailService)
		{
			_db = db;
			_userManager = userManager;
			secretKey = configuration.GetValue<string>("ApiSettings:secret");
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
			_emailService = emailService;
		}

		public async Task<LoginResDTO> LoginAsync(LoginReqDTO loginReqDTO)
		{

			var user = await _userManager.FindByEmailAsync(loginReqDTO.Email);
			bool isValid = await _userManager.CheckPasswordAsync(user, loginReqDTO.Password);



			if (user == null || isValid == false)
			{
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
					Email = user.Email,
					Type = user.GetType().ToString().Substring(user.GetType().ToString().LastIndexOf('.') + 1)
					//get type => get the class type . 
				}
			};
			return loginResDTO;
		}


		public async Task<string> ForgotPassword(string id, string email)
		{
			User user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return null;
			}

			if (email == user.Email)
			{
				string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

				var result = await _emailService.SendEmail(resetToken, email);
				if (result == "Success")
				{
					return resetToken;
				}
				else return null;
			}
			return null;

		}
		public async Task<string> UpdateForgottenPassword(string email, string resetToken, string newPassword)
		{
			User user = await _userManager.FindByEmailAsync(email);
			IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
			if (passwordChangeResult.Succeeded)
			{
				return "success";
			}
			return "Failed";
		}




		public async Task<UserManagerResponse> RegisterUserAsync(RegisterPatientDTO model)
		{
			if (model == null)
				throw new NullReferenceException("Reigster Model is null");

			if (model.Password != model.ConfirmPassword)
				return new UserManagerResponse
				{
					Message = "Confirm password doesn't match the password",
					IsSuccess = false,
				};


			var user = _mapper.Map<PatientUser>(model);

			var result = await _userManager.CreateAsync(user, model.Password);

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


		public async Task<UserManagerResponse> RegisterCaregiverAsync([FromBody] RegisterCaregiverDTO model)
		{


			if (model == null)
				throw new NullReferenceException("Reigster Model is null");

			if (model.Password != model.ConfirmPassword)
				return new UserManagerResponse
				{
					Message = "Confirm password doesn't match the password",
					IsSuccess = false,
				};

			var user = new CaregiverUser
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Gender = model.Gender.ToString(),
				Birthdate = model.Birthdate,
				Nationality = model.Nationality,
				UserName = model.Email,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,


			};




			var result = await _userManager.CreateAsync(user, model.Password);

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
                

        public async Task<UserManagerResponse> FormCaregiverAsync([FromForm] FormCaregiverDTO model)
		{
			if (!_allowedExt.Contains(Path.GetExtension(model.UploadPhoto.FileName).ToLower()))

                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "return only valid ext"
                };

            using var datastream = new MemoryStream();

			await model.Resume.CopyToAsync(datastream);

			using var datastream1 = new MemoryStream();

			await model.CriminalRecords.CopyToAsync(datastream1);

            using var datastream2 = new MemoryStream();

            await model.UploadPhoto.CopyToAsync(datastream2);

            var loggedInUserId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

			if (model == null)
				throw new NullReferenceException("Register Model is null");

			var user = await _userManager.FindByIdAsync(loggedInUserId);
			if (user != null && user is CaregiverUser caregiverUser)
			{
				caregiverUser.City = model.City.ToString();
				caregiverUser.Country = model.Country;
				caregiverUser.JobTitle = model.JobTitle.ToString();
				caregiverUser.PricePerDay = model.PricePerDay;
				caregiverUser.PricePerHour = model.PricePerHour;

				caregiverUser.YearsOfExperience = model.YearsOfExperience;
				caregiverUser.Resume = datastream.ToArray();
				caregiverUser.CriminalRecords = datastream1.ToArray();
				caregiverUser.Photo = datastream2.ToArray();

                caregiverUser.WhatCanCaregiverDo = model.WhatCanCaregiverDo;

				var result = await _userManager.UpdateAsync(caregiverUser);

				if (result.Succeeded)
				{
					// Update successful, return success response
					return new UserManagerResponse
					{
						IsSuccess = true,
						Message = "Additional data updated successfully."
					};
				}
				else
				{
					// Update failed, return error response
					return new UserManagerResponse
					{
						IsSuccess = false,
						Message = "Failed to update additional data.",
						Errors = result.Errors.Select(e => e.Description)
					};
				}
			}
			else
			{
				// User not found, return error response
				return new UserManagerResponse
				{
					IsSuccess = false,
					Message = "User not found."
				};

			}
		}
	}
}