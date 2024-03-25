using Caregiver.Dtos;
using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;

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

		[HttpPost("ForgotPassword")]
		public async Task<ActionResult<APIResponse>> ForgotPassword(string id, [FromBody] string email)
		{
			try
			{
				var result = await _userService.ForgotPassword(id, email);
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
		public async Task<ActionResult<APIResponse>> UpdatePassword([FromBody] string NewPassword)
		{
			try
			{
				string email = HttpContext.Request.Query["email"];
				string token = HttpContext.Request.Query["token"];

				var result = await _userService.UpdateForgottenPassword(email, token, NewPassword);
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


		[HttpPost("login")]
		public async Task<IActionResult> LoginAsync([FromBody] LoginReqDTO model)
		{
			var LoginRes = await _userService.LoginAsync(model);
			if (LoginRes.User == null || string.IsNullOrEmpty(LoginRes.Token))
			{

				return BadRequest(new { message = "Username or Password are Incorrect" });

			}


			return Ok(LoginRes);
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
				var result = await _userService.FormCaregiverAsync(model);
				if (result.IsSuccess)
					return Ok(result); // Status Code: 200
				return BadRequest(result);
			}
			return BadRequest("Some properties are not valid"); // Status code: 400
		}

	}
}
