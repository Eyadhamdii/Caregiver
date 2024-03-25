using AutoMapper;
using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
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
		private readonly ICaregiverService _caregiverService;
		private readonly APIResponse _response;
		private readonly AdminService s;

		public AdminController(ICaregiverService caregiverService, APIResponse response, AdminService s)
		{
			_caregiverService = caregiverService;
			_response = response;
			this.s = s;
		}


		[HttpGet]
		public async Task<IActionResult> getAll()
		{

			var ss = await s.AllCaregiver();
			return Ok(ss);
		}

		////get all (nurses or baby sitters or caregivers ) which  thier request still didn't accept
		////seperate the caregiver based on the ui page.. 
		//[HttpGet("role/{Role}", Name = "GetPendingCaregiver")]
		//[ProducesResponseType(StatusCodes.Status200OK)]
		//public async Task<ActionResult> GetAllCaregiverByType(string Role)
		//{
		//	if (Enum.TryParse<JobTitle>(Role, out JobTitle jobTitle))
		//	{
		//		IEnumerable<CaregiverUser> caregivers = await _dbCaregiver.GetAllAsync(a => a.JobTitle == jobTitle && a.IsAccepted == false);
		//		IEnumerable<CaregiverCardDTO> CaregiverCards = _mapper.Map<List<CaregiverCardDTO>>(caregivers);
		//		_response.Result = CaregiverCards;
		//		_response.IsSuccess = true;
		//		_response.StatusCode = System.Net.HttpStatusCode.OK;
		//		return Ok(_response);
		//	}
		//	else
		//	{
		//		_response.IsSuccess = false;
		//		_response.ErrorMessages = new List<string> { " invalid Role" };
		//		_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
		//		return BadRequest(_response);
		//	}
		//}



		////accept request of a caregiver is accepted -> true
		////this method will work with the 3 types of Caregivers
		//[HttpPut("{id}")]
		//public async Task<ActionResult<APIResponse>> AcceptCaregiver(string id)
		//{
		//	try
		//	{
		//		CaregiverUser caregiver = await _dbCaregiver.GetAsync(a => a.Id == id);
		//		if (caregiver == null)
		//		{
		//			_response.IsSuccess = false;
		//			_response.ErrorMessages = new List<string> { " Can't find the user by this id" };
		//			_response.StatusCode = System.Net.HttpStatusCode.NotFound;
		//			return NotFound(_response);
		//		}

		//		caregiver.IsAccepted = true;
		//		var result = await _userManager.UpdateAsync(caregiver);
		//		if (result.Succeeded)
		//		{
		//			_response.IsSuccess = true;
		//			_response.StatusCode = System.Net.HttpStatusCode.NoContent;
		//			return Ok(_response);

		//		}
		//	}
		//	catch (Exception e)
		//	{
		//		_response.IsSuccess = false;
		//		_response.ErrorMessages = new List<string> { e.Message };

		//	}
		//	return _response;
		//}


		////when request is denied -> delete Caregiver or when i want to delete the caregiver ..
		////delete nurse completely from database 
		//[HttpDelete("DeleteCaregiver/{id}")]
		//[ProducesResponseType(StatusCodes.Status404NotFound)]
		//[ProducesResponseType(StatusCodes.Status200OK)]
		//public async Task<ActionResult<APIResponse>> HardDeleteCaregiver(string id)
		//{
		//	try
		//	{
		//		CaregiverUser caregiver = await _dbCaregiver.GetAsync(a => a.Id == id);
		//		if (caregiver == null)
		//		{
		//			_response.IsSuccess = false;
		//			_response.ErrorMessages = new List<string> { " Can't find the user by this id" };
		//			_response.StatusCode = System.Net.HttpStatusCode.NotFound;
		//			return NotFound(_response);
		//		}

		//		var result = await _userManager.DeleteAsync(caregiver);
		//		if (result.Succeeded)
		//		{
		//			_response.IsSuccess = true;
		//			_response.StatusCode = System.Net.HttpStatusCode.NoContent;
		//			return Ok(_response);

		//		}
		//	}
		//	catch (Exception e)
		//	{
		//		_response.IsSuccess = false;
		//		_response.ErrorMessages = new List<string> { e.Message };

		//	}
		//	return _response;
		//}

		// hamo








		////get all caregivers which has account or deleted thier account ... 
		//[HttpGet("Caregivers")]
		//[ProducesResponseType(StatusCodes.Status200OK)]
		//public async Task<ActionResult<APIResponse>> GetAllCaregiverInAllTimes()
		//{
		//	try
		//	{
		//		IEnumerable<CaregiverUser> caregivers = await _dbCaregiver.GetAllAsync();

		//		IEnumerable<CaregiverCardDTO> CaregiverCards = _mapper.Map<List<CaregiverCardDTO>>(caregivers);

		//		_response.Result = CaregiverCards;
		//		_response.IsSuccess = true;
		//		_response.StatusCode = System.Net.HttpStatusCode.OK;
		//		return Ok(_response);
		//	}
		//	catch (Exception ex)
		//	{
		//		_response.IsSuccess = false;
		//		_response.ErrorMessages = new List<string> { ex.Message };

		//	}
		//	return _response;
		//}






		////i can add to check if isdeleted == false.. but i think it won't be necessary now
		//[HttpGet("{id}", Name = "GetCaregiverById")]
		//[ProducesResponseType(StatusCodes.Status200OK)]
		//[ProducesResponseType(StatusCodes.Status404NotFound)]
		//public async Task<ActionResult> GeOneCaregiverById(string id)
		//{
		//	CaregiverUser caregiver = await _dbCaregiver.GetAsync(a => a.Id == id);
		//	if (caregiver == null)
		//	{
		//		_response.IsSuccess = false;
		//		_response.ErrorMessages = new List<string> { " Can't find the user by this id" };
		//		_response.StatusCode = System.Net.HttpStatusCode.NotFound;
		//		return NotFound(_response);
		//	}

		//	return Ok(caregiver);
		//	//testing the update function
		//	//return Ok(_mapper.Map<CaregiverUpdateDTO>(caregiver));
		//}



		//[HttpDelete("hardDelete/{id}")]
		//[ProducesResponseType(StatusCodes.Status404NotFound)]
		//[ProducesResponseType(StatusCodes.Status200OK)]
		//public async Task<ActionResult<APIResponse>> HardDeleteCaregiver(string id)
		//{
		//	try
		//	{
		//		CaregiverUser caregiver = await _dbCaregiver.GetAsync(a => a.Id == id);
		//		if (caregiver == null)
		//		{
		//			_response.IsSuccess = false;
		//			_response.ErrorMessages = new List<string> { " Can't find the user by this id" };
		//			_response.StatusCode = System.Net.HttpStatusCode.NotFound;
		//			return NotFound(_response);
		//		}

		//		var result = await _userManager.DeleteAsync(caregiver);
		//		if (result.Succeeded)
		//		{
		//			_response.IsSuccess = true;
		//			_response.StatusCode = System.Net.HttpStatusCode.NoContent;
		//			return Ok(_response);

		//		}
		//	}
		//	catch (Exception e)
		//	{
		//		_response.IsSuccess = false;
		//		_response.ErrorMessages = new List<string> { e.Message };

		//	}
		//	return _response;
		//}



		//[Authorize(Policy = "Caregiver")]






		//[HttpGet("AllTimeCaregivers")]
		//[ProducesResponseType(StatusCodes.Status200OK)]
		//public async Task<ActionResult<APIResponse>> GetAllCaregiverInAllTimes()
		//{
		//	try
		//	{
		//		IEnumerable<CaregiverUser> caregivers = await _dbCaregiver.GetAllAsync();

		//		IEnumerable<CaregiverCardDTO> CaregiverCards = _mapper.Map<List<CaregiverCardDTO>>(caregivers);

		//		_response.Result = CaregiverCards;
		//		_response.IsSuccess = true;
		//		_response.StatusCode = System.Net.HttpStatusCode.OK;
		//		return Ok(_response);
		//	}
		//	catch (Exception ex)
		//	{
		//		_response.IsSuccess = false;
		//		_response.ErrorMessages = new List<string> { ex.Message };

		//	}
		//	return _response;
		//}






		//public async Task<ActionResult> GetAllCaregiverByType(string Role)
		//{
		//	if (Enum.TryParse<JobTitle>(Role, out JobTitle jobTitle))
		//	{
		//		IEnumerable<CaregiverUser> caregivers = await _dbCaregiver.GetAllAsync(a => a.JobTitle == jobTitle && a.IsDeleted == false);
		//		IEnumerable<CaregiverCardDTO> CaregiverCards = _mapper.Map<List<CaregiverCardDTO>>(caregivers);
		//		_response.Result = CaregiverCards;
		//		_response.IsSuccess = true;
		//		_response.StatusCode = System.Net.HttpStatusCode.OK;
		//		return Ok(_response);
		//	}
		//	else
		//	{
		//		_response.IsSuccess = false;
		//		_response.ErrorMessages = new List<string> { " invalid Role" };
		//		_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
		//		return BadRequest(_response);
		//	}
		//}



		//i can add to check if isdeleted == false.. but i think it won't be necessary now

	}
}
