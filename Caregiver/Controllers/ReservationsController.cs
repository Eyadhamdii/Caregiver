using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Caregiver.Repositories.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Caregiver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IReservationsRepo reservationsRepo;
        public ReservationsController(ApplicationDBContext context, IReservationsRepo _reservationsRepo)
        {
            reservationsRepo= _reservationsRepo;
            _context = context;
        }


        #region Get all reservations
        [HttpGet]
        public async Task <IActionResult> GetAllReservations()
        {
            var reservations = await reservationsRepo.GetAll();
            if (reservations == null) return BadRequest();
            return Ok(reservations);
        }
        #endregion

        #region Get reservation by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationbyId(int id)
        {
            var reservation = await reservationsRepo.GetReservationById(id);


            if (reservation == null)
            {
                return NotFound();
            }
            else
            {
                var dto = new ReservationDto {

                    NurseFirstName = reservation.Caregiver.FirstName,
                    NurseLastName = reservation.Caregiver.LastName,
                    PatientFirstName = reservation.Patient.FirstName,
                    PatientLastName = reservation.Patient.LastName,
                    EmailAddress = reservation.Caregiver.Email,
                    PhoneNumber = reservation.Caregiver.PhoneNumber,
                    OrderId = reservation.OrderId,
                    Gender = reservation.Caregiver.Gender,
                    Status = reservation.Status,
             
                };

 
                return Ok(dto);

            }
                
               
        }
        #endregion
    }
}
