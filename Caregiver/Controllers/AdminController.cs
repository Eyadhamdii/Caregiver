using AutoMapper;
using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Caregiver.Repositories.Repository;
using Caregiver.Services.IService;
using Caregiver.Services.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Caregiver.Enums.Enums;

namespace Caregiver.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly IAdminService _adminService;
		private readonly APIResponse _response;

		public AdminController(IAdminService adminService, APIResponse response)
		{
			_response = response;
			_adminService = adminService;
		}



		[HttpGet("AllCaregivers")]
		public async Task<IActionResult> getAll()
		{
			var ss = await _adminService.GetAllCaregivers();
			//var ss =  s.getCaregivers();
			return Ok(ss);
		}
		[HttpGet("RequestedCaregivers")]
		public async Task<IActionResult> getAllRequests()
		{
			var ss = await _adminService.GetRequested();
			//var ss =  s.getCaregivers();
			return Ok(ss);
		}


		[HttpGet("Caregiver/{title}")]
		public async Task<IActionResult> getAll(string title)
		{
			var ss = await _adminService.GetCaregiversJobTitle(title);
			//var ss =  s.getCaregivers();
			return Ok(ss);
		}


		[HttpPut("AcceptRequest/{id}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> AcceptRequest(string id)
		{//.IsAccepted = true;
			try
			{
				var result = await _adminService.AcceptRequestAsync(id);
				if (result == true)
				{
					_response.IsSuccess = true;
					_response.StatusCode = System.Net.HttpStatusCode.NoContent;
					
					return Ok(_response);
				}
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { " Can't find the user by this id or error in Accept it" };
				_response.StatusCode = System.Net.HttpStatusCode.NotFound;
				return NotFound(_response);

			}
			catch (Exception e)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { e.Message };
				_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
				return BadRequest(_response);

			}
		}


		[HttpDelete("DeclineRequest/{id}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> DeclineRequestCaregiver(string id)
		{//DeleteAsync(user);
			try
			{
				var result = await _adminService.HardDeleteCaregiver(id);
				if (result == true)
				{
					_response.IsSuccess = true;
					_response.StatusCode = System.Net.HttpStatusCode.NoContent;
					return Ok(_response);
				}
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { " Can't find the user by this id or error in delete it" };
				_response.StatusCode = System.Net.HttpStatusCode.NotFound;
				return NotFound(_response);

			}
			catch (Exception e)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { e.Message };
				_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
				return BadRequest(_response);

			}
		}


		[HttpDelete("AdminDeleteCaregiver/{id}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> AdminDeleteCaregiver(string id)
		{//IsDeletedByAdmin = true
			try
			{
				var result = await _adminService.AdminDeleteCaregiver(id);
				if (result == true)
				{
					_response.IsSuccess = true;
					_response.StatusCode = System.Net.HttpStatusCode.NoContent;
					return Ok(_response);
				}
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { " Can't find the user by this id or error in delete it" };
				_response.StatusCode = System.Net.HttpStatusCode.NotFound;
				return NotFound(_response);

			}
			catch (Exception e)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { e.Message };
				_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
				return BadRequest(_response);

			}
		}

		[HttpPut("AdminReturnDeletedCaregiver/{id}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> AdminReturnDeletedCaregiver(string id)
		{//IsDeletedByAdmin = false
			try
			{
				var result = await _adminService.AdminReturnCaregiver(id);
				if (result == true)
				{
					_response.IsSuccess = true;
					_response.StatusCode = System.Net.HttpStatusCode.NoContent;
					return Ok(_response);
				}
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { " Can't find the user by this id or error in delete it" };
				_response.StatusCode = System.Net.HttpStatusCode.NotFound;
				return NotFound(_response);

			}
			catch (Exception e)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { e.Message };
				_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
				return BadRequest(_response);

			}
		}


		//[HttpDelete("DeleteCaregiver{id}")]
		//[ProducesResponseType(StatusCodes.Status404NotFound)]
		//[ProducesResponseType(StatusCodes.Status400BadRequest)]
		//[ProducesResponseType(StatusCodes.Status200OK)]
		//public async Task<ActionResult<APIResponse>> SoftDeleteCaregiver(string id)
		//{
		//	try
		//	{
		//		var result = await _adminService.SoftDeleteCaregiver(id);
		//		if (result == true)
		//		{
		//			_response.IsSuccess = true;
		//			_response.StatusCode = System.Net.HttpStatusCode.NoContent;
		//			return Ok(_response);
		//		}
		//		_response.IsSuccess = false;
		//		_response.ErrorMessages = new List<string> { " Can't find the user by this id or error in delete it" };
		//		_response.StatusCode = System.Net.HttpStatusCode.NotFound;
		//		return NotFound(_response);

		//	}
		//	catch (Exception e)
		//	{
		//		_response.IsSuccess = false;
		//		_response.ErrorMessages = new List<string> { e.Message };
		//		_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
		//		return BadRequest(_response);

		//	}
		//}





	}
}
