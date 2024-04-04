using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Services.IService;
using Caregiver.Services.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caregiver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
		private readonly ICustomerService _customerService;
		private readonly APIResponse _response;

		public CustomerController(ICustomerService customerService, APIResponse response)
		{
			_customerService = customerService;
			_response = response;
		}

		[HttpGet("AllCurrentCustomer")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllCurrentCustomer()
		{
			try
			{
				IEnumerable<GetCustomerDTO> Customer = await _customerService.GetAllCurrentCustomer();
				if (Customer == null)
				{
					_response.IsSuccess = false;
					_response.StatusCode = System.Net.HttpStatusCode.NotFound;
					_response.ErrorMessages = new List<string> { "The List of Customer is Null" };
					return NotFound(_response);
				}
				_response.Result = Customer;
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

		[HttpGet("{id}", Name = "GetCustomerById")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> GetOneCustomerById(string id)
		{
			try
			{

				PatientUser patient = await _customerService.GetCustomerById(id);
				if (patient == null)
				{
					_response.IsSuccess = false;
					_response.ErrorMessages = new List<string> { " Can't find the user by this id" };
					_response.StatusCode = System.Net.HttpStatusCode.NotFound;
					return NotFound(_response);
				}

				_response.Result = patient;
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
		//
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> SoftDeleteCustomer(string id)
		{
			try
			{
				var result = await _customerService.SoftDeleteCustomer(id);
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

		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> Update(string id, [FromBody] GetCustomerDTO CustomerUpdate)
		{
			try
			{
				var result = await _customerService.UpdateCustomerAsync(id, CustomerUpdate);
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
