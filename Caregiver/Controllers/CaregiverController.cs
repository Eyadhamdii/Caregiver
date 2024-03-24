using AutoMapper;
using Caregiver.Dtos;
using Caregiver.Dtos.UpdateDTOs;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Caregiver.Enums.Enums;

namespace Caregiver.Controllers
{
	[Route("api/Caregiver")]
	[ApiController]
	public class CaregiverController : ControllerBase
	{
		private readonly ICaregiverRepo _dbCaregiver;
		private readonly APIResponse _response;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;

		public CaregiverController(ICaregiverRepo dbCaregiver, APIResponse response, IMapper mapper, UserManager<User> userManager)
		{
			_dbCaregiver = dbCaregiver;
			_response = response;
			_mapper = mapper;
			_userManager = userManager;
		}



		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> Update(string id, [FromBody] CaregiverUpdateDTO caregiverUpdate)
		{
			try
			{
				CaregiverUser caregiverToUpdate = await _dbCaregiver.GetAsync(a => a.Id == id);
				if (caregiverToUpdate == null)
				{
					_response.IsSuccess = false;
					_response.ErrorMessages = new List<string> { " Can't find the user by this id " };
					_response.StatusCode = System.Net.HttpStatusCode.NotFound;
					return NotFound(_response);
				}
				//It is used for updating the properties of an existing CaregiverUser object (caregiverToUpdate) with the values provided in the caregiverUpdate DTO.
				_mapper.Map(caregiverUpdate, caregiverToUpdate);

				var result = await _userManager.UpdateAsync(caregiverToUpdate);
				//db.save changes could work but usermanager is better for 
				if (result.Succeeded)
				{
					_response.IsSuccess = true;
					_response.StatusCode = System.Net.HttpStatusCode.OK;
					_response.Result = _mapper.Map<CaregiverUpdateDTO>(_mapper.Map(caregiverUpdate, caregiverToUpdate));
					return Ok(_response);
				}
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { " Can't update" };
				_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
				return BadRequest(_response);
			}
			catch (Exception e)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { e.Message };

			}
			return _response;


		}


		[HttpDelete("softDelete/{id}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> SoftDeleteCaregiver(string id)
		{
			try
			{
				CaregiverUser caregiver = await _dbCaregiver.GetAsync(a => a.Id == id);
				if (caregiver == null)
				{
					_response.IsSuccess = false;
					_response.ErrorMessages = new List<string> { " Can't find the user by this id" };
					_response.StatusCode = System.Net.HttpStatusCode.NotFound;
					return NotFound(_response);
				}

				caregiver.IsDeleted = true;
				var result = await _userManager.UpdateAsync(caregiver);
				if (result.Succeeded)
				{
					_response.IsSuccess = true;
					_response.StatusCode = System.Net.HttpStatusCode.NoContent;
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
		[HttpGet("AllCurrentCaregivers")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetAllCurrentCaregiver()
		{
			try
			{
				//services
				IEnumerable<CaregiverUser> caregivers = await _dbCaregiver.GetAllAsync(a => a.IsDeleted == false);
				//services

				IEnumerable<CaregiverCardDTO> CaregiverCards = _mapper.Map<List<CaregiverCardDTO>>(caregivers);

				_response.Result = CaregiverCards;
				_response.IsSuccess = true;
				_response.StatusCode = System.Net.HttpStatusCode.OK;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { ex.Message };

			}
			return _response;
		}

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



		[HttpGet("role/{Role}", Name = "GetCaregiverByRole")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> GetAllCaregiverByType(string Role)
		{
			try
			{
				IEnumerable<CaregiverUser> caregivers = await _dbCaregiver.GetAllAsync(a => a.JobTitle == Role && a.IsDeleted == false);
				IEnumerable<CaregiverCardDTO> CaregiverCards = _mapper.Map<List<CaregiverCardDTO>>(caregivers);
				_response.Result = CaregiverCards;
				_response.IsSuccess = true;
				_response.StatusCode = System.Net.HttpStatusCode.OK;
				return Ok(_response);
			}
			catch (Exception e)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { " invalid Role", e.Message };
				_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
				return BadRequest(_response);
			}
		}



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



		[HttpGet("{id}", Name = "GetCaregiverById")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> GeOneCaregiverById(string id)
		{
			CaregiverUser caregiver = await _dbCaregiver.GetAsync(a => a.Id == id);
			if (caregiver == null)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { " Can't find the user by this id" };
				_response.StatusCode = System.Net.HttpStatusCode.NotFound;
				return NotFound(_response);
			}

			return Ok(caregiver);
			//testing the update function
			//return Ok(_mapper.Map<CaregiverUpdateDTO>(caregiver));
		}















	}
}
