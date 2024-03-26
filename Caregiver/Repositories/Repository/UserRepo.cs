using AutoMapper;
using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Caregiver.Enums.Enums;
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
		private readonly IEmailRepo _emailService;




		public UserRepo(UserManager<User> userManager, ApplicationDBContext db, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IEmailRepo emailService)
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

		
			//key 
			var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey);
			var key = new SymmetricSecurityKey(secretKeyInBytes);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				
				Subject = new ClaimsIdentity(new Claim[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Role, user.GetType().ToString().Substring(user.GetType().ToString().LastIndexOf('.') + 1))
				
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

				string resetUrl = $"http://localhost:5248/api/Auth/UpdatePassword?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(resetToken)}";
				var message = $"<h3> Click on the link and will direct you to the page to enter a new password</h3>  <a href=\"{resetUrl}\">Click Here</a>";
				string header = "Your Reset Password Link";
				var result = await _emailService.SendEmail(message,header, email);
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

			if (!result.Succeeded)
				return new UserManagerResponse
				{
					Message = "User did not create",
					IsSuccess = false,
					Errors = result.Errors.Select(e => e.Description)
				};
			var userClaims = new List<Claim>
{
	
	new Claim(ClaimTypes.Role, user.GetType().ToString().Substring(user.GetType().ToString().LastIndexOf('.') + 1)),
};


			await _userManager.AddClaimsAsync(user, userClaims);
			return new UserManagerResponse
			{
				Message = "User created successfully!",
				IsSuccess = true,
				
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
				Bio = model.Bio

			};




			var result = await _userManager.CreateAsync(user, model.Password);

	
			if (!result.Succeeded)
				return new UserManagerResponse
				{
				Message = "User did not create",
				IsSuccess = false,
				Errors = result.Errors.Select(e => e.Description)
			
				};

			var userClaims = new List<Claim>
{

	new Claim(ClaimTypes.Role, user.GetType().ToString().Substring(user.GetType().ToString().LastIndexOf('.') + 1)),
};

			await _userManager.AddClaimsAsync(user, userClaims);
			return new UserManagerResponse
			{

Message = "User created successfully!",
					IsSuccess = true,
			
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

        //     public async Task<UserManagerResponse> PersonalDetailsAsync(PersonalDetailsDTO model)
        //     {
        //var loggedInUserId = "777ab200-3f98-4f4c-a0f6-83d892a5b9bd";
        //	// _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

        //         if (model == null)
        //             throw new NullReferenceException("Please Fill The Form");

        //         var user = await _userManager.FindByIdAsync(loggedInUserId);
        //         if (user != null && user is PatientUser PatientUser && model.ReservationType == "Me")
        //         {
        //             PatientUser.FirstName = model.FirstName;
        //             PatientUser.LastName = model.LastName;
        //             PatientUser.Age = model.Age;
        //	PatientUser.Gender = model.Gender.ToString();
        //	PatientUser.EmailAddress = model.EmailAddress;
        //	PatientUser.Location = model.Location;
        //	PatientUser.PhoneNumber = model.PhoneNumber;
        //	PatientUser.ReservationNotes = model.ReservationNotes;


        //             var result = await _userManager.UpdateAsync(PatientUser);

        //             if (result.Succeeded)
        //             {
        //                 // Update successful, return success response
        //                 return new UserManagerResponse
        //                 {
        //                     IsSuccess = true,
        //                     Message = "Additional data updated successfully."
        //                 };
        //             }
        //             else
        //             {
        //                 // Update failed, return error response
        //                 return new UserManagerResponse
        //                 {
        //                     IsSuccess = false,
        //                     Message = "Failed to update additional data.",
        //                     Errors = result.Errors.Select(e => e.Description)
        //                 };
        //             }
        //         }
        //         else
        //         {
        //	// User not found, return error response


        //	//return new UserManagerResponse
        //	//{
        //	//    IsSuccess = false,
        //	//    Message = "User not found."
        //	//};
        //	var relevant = new Dependant
        //	{
        //                 PatientId = loggedInUserId,
        //                 FirstName = model.FirstName,
        //                 LastName = model.LastName,
        //                 PhoneNumber = model.PhoneNumber,
        //                 Age = model.Age,
        //                 Location = model.Location,
        //                 EmailAddress = model.EmailAddress,
        //                 Gender = model.Gender,
        //                 ReservationNotes = model.ReservationNotes
        //             };


        //         }
        //     }
        public async Task<UserManagerResponse> PersonalDetailsAsync(PersonalDetailsDTO model)
        {
            var loggedInUserId = "777ab200-3f98-4f4c-a0f6-83d892a5b9bd";
            // _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

            if (model == null)
                throw new NullReferenceException("Please Fill The Form");

            var user = await _userManager.FindByIdAsync(loggedInUserId);
            if (user != null && user is PatientUser patientUser && model.ReservationType == Enum.Parse<ReservationType>("Me"))
            {
                patientUser.FirstName = model.FirstName;
                patientUser.LastName = model.LastName;
                patientUser.Age = model.Age;
                patientUser.Gender = model.Gender.ToString();
                patientUser.Email = model.EmailAddress;
                patientUser.Location = model.Location;
                patientUser.PhoneNumber = model.PhoneNumber;
                patientUser.ReservationNotes = model.ReservationNotes;

                var result = await _userManager.UpdateAsync(patientUser);

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
                // ReservationType is not "Me" or user is not found, add logic for Dependant here

                var relevant = new Dependant
                {
                    PatientId = loggedInUserId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Age = model.Age,
                    Location = model.Location,
                    EmailAddress = model.EmailAddress,
                    Gender = model.Gender.ToString(),
                    ReservationNotes = model.ReservationNotes
                };
				 await _db.Dependants.AddAsync(relevant);
				 _db.SaveChanges();
                // Now you should add code to handle saving Dependant to your data store

                // For example, if you have a repository method to save a Dependant:
                // await _dependantRepository.AddAsync(relevant);

                // Return appropriate response
                return new UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "Dependant details added successfully."
                };
            }
        }


    }

}