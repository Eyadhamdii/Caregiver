using Caregiver.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Caregiver.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReservationsController : ControllerBase
	{
		private readonly ApplicationDBContext _db;

		public ReservationsController(ApplicationDBContext db)
		{
			_db = db;
		}



		[HttpGet]
		public IActionResult Get()
		{
			var res = _db.Reservations.ToList();
			var res1 = _db.Reservations.Include(a => a.Caregiver).Include(e => e.Patient);

			return Ok(res);
		}

	}
}
