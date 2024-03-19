using AutoMapper;
using Caregiver.Dtos;
using Caregiver.Dtos.UpdateDTOs;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
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
		private readonly ApplicationDBContext _db;
		private readonly UserManager<User> _m;

		public CaregiverController(ICaregiverRepo dbCaregiver, APIResponse response, IMapper mapper, ApplicationDBContext db, UserManager<User> m)
		{
			_dbCaregiver = dbCaregiver;
			_response = response;
			_mapper = mapper;
			_db = db;
			_m = m;
		}

		[HttpPut]
		public async Task<IActionResult> Update(string id, [FromBody] CaregiverUpdateDTO caregiverUpdate)
		{
			CaregiverUser caregiverToUpdate = await _dbCaregiver.GetAsync(a => a.Id == id);
			if (caregiverToUpdate == null)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { " Can't find the user by this id" };
				_response.StatusCode = System.Net.HttpStatusCode.NotFound;
				return NotFound(_response);
			}

			//caregiverToUpdate.Country = caregiverUpdate.Country;
			//caregiverToUpdate.City = caregiverUpdate.City;
			//caregiverToUpdate.Gender = caregiverUpdate.Gender;
			//caregiverToUpdate.Bio = caregiverUpdate.Bio;
			//caregiverToUpdate.Birthdate = caregiverUpdate.Birthdate;
			//caregiverToUpdate.Email = caregiverUpdate.Email;
			//caregiverToUpdate.Nationality = caregiverUpdate.Nationality;
			//caregiverToUpdate.PhoneNumber = caregiverUpdate.PhoneNumber;
			//caregiverToUpdate.PricePerDay = caregiverUpdate.PricePerDay;
			//caregiverToUpdate.PricePerHour = caregiverUpdate.PricePerHour;
			//caregiverToUpdate.JobTitle = caregiverUpdate.JobTitle;
			//caregiverToUpdate.YearsOfExperience = caregiverUpdate.YearsOfExperience;
			//caregiverToUpdate.CareerLevel = caregiverUpdate.CareerLevel;
			_mapper.Map(caregiverUpdate, caregiverToUpdate);

			//var result = await _m.UpdateAsync(caregiverToUpdate);

			if (_db.SaveChanges() > 0)
			{
				_response.IsSuccess = true;
				_response.StatusCode = System.Net.HttpStatusCode.OK;
				return Ok(_response);
			}
			_response.IsSuccess = false;
			_response.ErrorMessages = new List<string> { " Can't update" };
			_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
			return BadRequest(_response);


		}



		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetAllCaregiver()
		{
			try
			{
				IEnumerable<CaregiverUser> caregivers = await _dbCaregiver.GetAllAsync();

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



		[HttpGet("role/{Role}", Name = "GetCaregiverByRole")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> GetAllCaregiverByType(string Role)
		{
			if (Enum.TryParse<JobTitle>(Role, out JobTitle jobTitle))
			{
				IEnumerable<CaregiverUser> caregivers = await _dbCaregiver.GetAllAsync(a => a.JobTitle == jobTitle);
				IEnumerable<CaregiverCardDTO> CaregiverCards = _mapper.Map<List<CaregiverCardDTO>>(caregivers);
				_response.Result = CaregiverCards;
				_response.IsSuccess = true;
				_response.StatusCode = System.Net.HttpStatusCode.OK;
				return Ok(_response);
			}
			else
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { " invalid Role" };
				_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
				return BadRequest(_response);
			}
		}



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

			//	return Ok(caregiver);
			return Ok(_mapper.Map<CaregiverUpdateDTO>(caregiver));
		}















	}
}
