using Caregiver.Dtos;
using Caregiver.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Caregiver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        public AuthController(IUserService userService) {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO model) {

            if (ModelState.IsValid)
            { 
            var result = await _userService.RegisterUserAsync(model);
            if (result.IsSuccess)
                return Ok(result); // Status Code: 200
            return BadRequest(result);
        }
            return BadRequest("Some properties are not valid"); // Status code: 400
            }
    }
    }
