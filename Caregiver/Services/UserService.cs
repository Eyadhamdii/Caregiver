﻿using Caregiver.Dtos;
using Caregiver.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Caregiver.Services
{
	public class UserService : IUserService
	{
		private UserManager<User> _userManager;
		private readonly string secretKey;
		private readonly ApplicationDBContext _db;

		public UserService(UserManager<User> userManager, ApplicationDBContext db, IConfiguration configuration)
		{
			_db = db;
			_userManager = userManager;
			secretKey = configuration.GetValue<string>("ApiSettings:secret");

		}


		public async Task<LoginResDTO> Login(LoginReqDTO loginReqDTO)
		{

			var user = await _userManager.FindByEmailAsync(loginReqDTO.UserName);
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


		public async Task<UserManagerResponse> RegisterUserAsync(RegisterCustomerDTO model)
		{
			if (model == null)
				throw new NullReferenceException("Reigster Model is null");

			if (model.Password != model.ConfirmPassword)
				return new UserManagerResponse
				{
					Message = "Confirm password doesn't match the password",
					IsSuccess = false,
				};

			var user = new User
			{
			 FirstName = model.FirstName,
			 LastName = model.LastName,
			 Gender = model.Gender,
			 Birthdate = model.Birthdate,
			 Nationality = model.Nationality,
             UserName = model.Email,
			 Email = model.Email,
             PhoneNumber = model.PhoneNumber,
			 Resume = null,
			 CriminalRecords = null,
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

        public async Task<UserManagerResponse> RegisterCaregiverAsync([FromForm] RegisterCaregiverDTO model)
        {
            using var datastream = new MemoryStream();
            await model.Resume.CopyToAsync(datastream);

            var datastream1 = new MemoryStream();
            await model.CriminalRecords.CopyToAsync(datastream1);



            if (model == null)
                throw new NullReferenceException("Reigster Model is null");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false,
                };

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                Birthdate = model.Birthdate,
                Nationality = model.Nationality,
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
				Bio = model.Bio,
				City = model.City,
				Country = model.Country,
				JobTitle = model.JobTitle,
				PricePerDay = model.PricePerDay,
				PricePerHour = model.PricePerHour,
				WhatCanYouDo = model.WhatCanYouDo,
				YearsOfExperience = model.YearsOfExperience,
				Resume = datastream.ToArray(),
				CriminalRecords = datastream1.ToArray(),

               
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

    }
}