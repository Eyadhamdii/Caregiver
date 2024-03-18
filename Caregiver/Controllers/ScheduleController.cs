using Caregiver.Dtos;
using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caregiver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepo _ScheduleRepo;
        public ScheduleController(IScheduleRepo ScheduleRepo)
        {
            _ScheduleRepo = ScheduleRepo;
        }

        [HttpPost("AddSchedule")]
        public async Task<IActionResult> ScheduleAsync(ScheduleDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _ScheduleRepo.AddScheduleAsync(model);
                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200
                return BadRequest(result);

            }
            return BadRequest("Some properties are not valid"); // Status code: 400
        }
    }
}
