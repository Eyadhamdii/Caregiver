using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Caregiver.Controllers
{
	[Route("api/Caregiver")]
	[ApiController]
	public class CaregiverController : ControllerBase
	{
		private readonly IGenericRepo _genericRepo;


		public CaregiverController(IGenericRepo genericRepo)
		{
			_genericRepo = genericRepo;
		}



		[HttpGet]
		public async Task<ActionResult> getUsersByDiscriminatorAsync(string discriminator)
		{

			List<User> users = await _genericRepo.getUsersByDiscriminatorAsync(discriminator);
			return Ok(users);

		}

		[HttpGet("patients")]
		public async Task<ActionResult> getpatients()
		{

			List<CaregiverUser> users = await _genericRepo.GetPatients();
			return Ok(users);

		}










	}
}
