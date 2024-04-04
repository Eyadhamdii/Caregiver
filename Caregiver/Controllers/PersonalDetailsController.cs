using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Caregiver.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caregiver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalDetailsController : ControllerBase

    {

        private readonly IUserRepo _userService;
        private readonly IEmailRepo _emailService;
        private readonly APIResponse _response;
        private readonly ICustomerService _customerService;
        

        public PersonalDetailsController(IUserRepo userService, IEmailRepo emailService, APIResponse response, ICustomerService customerService)
        {
            _userService = userService;
            _emailService = emailService;
            _response = response;
            _customerService = customerService;
        }

        [HttpPut("UpdatePersonalDetails")]
        public async Task<ActionResult> UpdatePersonalDetails(PersonalDetailsDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.PersonalDetailsAsync(model);
                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid"); // Status code: 400
        }
        [HttpGet("GetPersonalDetailsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GePersonalDetailsById()
        {
            try
            {

                DependantDetailsDTO patient = await _customerService.GetDependantDetails();
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
    }
}
