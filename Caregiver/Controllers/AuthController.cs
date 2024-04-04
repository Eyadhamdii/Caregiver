using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Caregiver.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IUserRepo _userService;
		private readonly IEmailRepo _emailService;
		private readonly APIResponse _response;

		public AuthController(IUserRepo userService, IEmailRepo emailService, APIResponse response)
		{
			_userService = userService;
			_emailService = emailService;
			_response = response;
		}

        [HttpPost("CaregiverRegister")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterCaregiverDTO model)
        {


            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterCaregiverAsync(model);
                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid"); // Status code: 400
        }

        [HttpPost("CaregiverForm")]
        public async Task<IActionResult> FormAsync([FromForm] FormCaregiverDTO model)
        {


            if (ModelState.IsValid)
            {
                var result = await _userService.FormCaregiverAsync(model , Request);
                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid"); // Status code: 400
        }

        [HttpPost("PatientRegister")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterPatientDTO model)
        {

            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);
                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid"); // Status code: 400
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginReqDTO model)
        {
			try
			{
				var LoginRes = await _userService.LoginAsync(model);
				if (LoginRes.User == null || string.IsNullOrEmpty(LoginRes.Token))
				{

					return BadRequest(new { message = "Username or Password are Incorrect" });

				}


				return Ok(LoginRes);
			} catch(Exception e)
			{
				return BadRequest(e.Message);
			}
        }

        [HttpPost("ForgotPassword")]
		public async Task<ActionResult<APIResponse>> ForgotPassword( [FromBody] ForgotPasswordDTO model)
	{
			try
			{
				var result = await _userService.ForgotPassword(model.Email);
				if (result != null)
				{
					_response.StatusCode = System.Net.HttpStatusCode.OK;
					_response.Result = result;
					_response.IsSuccess = true;
					return Ok(_response);
				}
			}
			catch (Exception e)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { e.Message };

			}
			return _response;
		}

		[HttpPut("UpdatePassword")]
		public async Task<ActionResult<APIResponse>> UpdatePassword([FromBody] UpdatePasswordDTO model)
		{
			try
			{

				string decodedToken = HttpUtility.UrlDecode(model.Token);
				string decodedEmail = Uri.UnescapeDataString(model.Email);

				//string email = HttpContext.Request.Query["email"];
				//string token = HttpContext.Request.Query["token"];

				var result = await _userService.UpdateForgottenPassword(decodedEmail, decodedToken, model.NewPassword);
				if (result == "success")
				{
					_response.StatusCode = System.Net.HttpStatusCode.OK;
					_response.Result = result;
					_response.IsSuccess = true;
					return Ok(_response);
				}
			}
			catch (Exception e)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { e.Message };

			}
			return _response;

		}

		[HttpPost("logout")]
		public async Task<IActionResult> LogoutAsync()
		{

				var result = await _userService.LogoutAsync();
				if (result.IsSuccess)
					return Ok(result); // Status Code: 200
			
			// Handle the case where the username is null or empty
			return BadRequest("Invalid username for logout."); // Statu
		}
	}
}
