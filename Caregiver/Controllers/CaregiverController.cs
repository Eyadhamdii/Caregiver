using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Caregiver.Controllers
{
    [Route("api/Caregiver")]
	[ApiController]
	public class CaregiverController : ControllerBase
	{
		private readonly ICaregiverService _caregiverService;
		private readonly APIResponse _response;

		public CaregiverController(ICaregiverService caregiverService,  APIResponse response)
		{
			_caregiverService = caregiverService;
			_response = response;
		}
		
		
		[HttpGet("AllCurrentCaregivers")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllCurrentCaregiver()
		{
			try
			{
				IEnumerable<CaregiverCardDTO> CaregiverCards = await _caregiverService.GetAllCurrentCaregiver(); 
				if (CaregiverCards == null)
				{
					_response.IsSuccess = false;
					_response.StatusCode = System.Net.HttpStatusCode.NotFound;
					_response.ErrorMessages = new List<string> { "The List of Caregivers is Null" };
					return NotFound(_response);
				}
				_response.Result = CaregiverCards;
				_response.IsSuccess = true;
				_response.StatusCode = System.Net.HttpStatusCode.OK;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { ex.Message };
			_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
				return BadRequest(_response);

			}				
		}



		[HttpGet("role/{Role}", Name = "GetCaregiverByRole")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllCaregiverByType(string Role)
		{
			try
			{
				IEnumerable<CaregiverCardDTO> CaregiverCards = await _caregiverService.GetAllCaregiverByType(Role);
				if (CaregiverCards == null )
				{
					_response.IsSuccess = false;
					_response.StatusCode = System.Net.HttpStatusCode.NotFound;
					_response.ErrorMessages = new List<string> { "The List of Caregivers is Null or invalid Role" };
					return NotFound(_response);
				}
				_response.Result = CaregiverCards;
				_response.IsSuccess = true;
				_response.StatusCode = System.Net.HttpStatusCode.OK;
				return Ok(_response);
			}
			catch (Exception e)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { e.Message };
				_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
				return BadRequest(_response);
			}
		}



     	[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> SoftDeleteCaregiver(string id)
		{
			try
			{
				var result = await _caregiverService.SoftDeleteCaregiver(id);
				if(result == true)
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



		[HttpGet("{id}", Name = "GetCaregiverById")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> GeOneCaregiverById(string id)
		{
			try {

				CaregiverUser caregiver = await _caregiverService.GetCaregiverById(id);
			   if (caregiver == null)
				{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { " Can't find the user by this id" };
				_response.StatusCode = System.Net.HttpStatusCode.NotFound;
				return NotFound(_response);
				}

				_response.Result = caregiver;
				_response.IsSuccess = true;
				_response.StatusCode = System.Net.HttpStatusCode.OK;
				return Ok(_response);
				//to test the update function use this line
				//return Ok(_mapper.Map<CaregiverUpdateDTO>(caregiver));

			}
			catch (Exception e)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { e.Message };
				_response.StatusCode = System.Net.HttpStatusCode.BadRequest;
				return BadRequest(_response);

			}
		}




		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> Update(string id, [FromBody] CaregiverUpdateDTO caregiverUpdate)
		{
			try
			{
				var result = await _caregiverService.UpdateCaregiverAsync(id, caregiverUpdate);
 				if (result != null)
				{
					_response.IsSuccess = true;
					_response.StatusCode = System.Net.HttpStatusCode.OK;
					_response.Result = result;
					return Ok(_response);
				}
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { " Not Found " };
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

	}
}
