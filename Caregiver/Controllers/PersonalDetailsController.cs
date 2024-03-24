using Caregiver.Dtos;
using Caregiver.Repositories.IRepository;
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

        public PersonalDetailsController(IUserRepo userService, IEmailRepo emailService, APIResponse response)
        {
            _userService = userService;
            _emailService = emailService;
            _response = response;
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

    }
}
