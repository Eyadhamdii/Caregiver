using AutoMapper;
using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Caregiver.Repositories.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Caregiver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IReservationsRepo reservationsRepo;
        private UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper mapper;


        public ReservationsController(ApplicationDBContext context, IReservationsRepo _reservationsRepo, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IMapper _mapper)
        {
            reservationsRepo= _reservationsRepo;
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }


        #region Get all reservations to admin
        [HttpGet]
        public async Task <IActionResult> GetAllReservations()
        {
            var loggedInUserId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            var reservations = await reservationsRepo.GetAll();
            if (reservations == null) return BadRequest();
            return Ok(reservations);
        }
        #endregion

        #region Get all reservations to patient
        [HttpGet("PatientReservations")]
        public async Task<IActionResult> GetAllReservationsToPatient()
        {
            
            var reservations = await reservationsRepo.GetPatientAllReservations();


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
                    PatientId = reservation.PatientId,
                    NurseFirstName = reservation.Caregiver.FirstName,
                    NurseLastName = reservation.Caregiver.LastName,
                    PatientFirstName = reservation.Patient.FirstName,
                    PatientLastName = reservation.Patient.LastName,
                    CaregiverEmailAddress = reservation.Caregiver.Email,
                    PhoneNumber = reservation.Caregiver.PhoneNumber,
                    OrderId = reservation.OrderId,
                    Gender = reservation.Caregiver.Gender,
                    Status = reservation.Status,
                    TotalPrice= reservation.totalPrice
                };

                return Ok(dto);

            }
                
               
        }   
        #endregion
         
        #region Get Patient reservation by id
        [HttpGet("PatientReservation")]
        public async Task<IActionResult> GetPatientReservation(int id)
        {
            
            var reservation = await reservationsRepo.GetPatientReservationById(id);


            if (reservation == null)
            {
                return NotFound();
            }
            else
            {
                var dto = new ReservationDto
                {
                    PatientId = reservation.PatientId,
                    NurseFirstName = reservation.Caregiver.FirstName,
                    NurseLastName = reservation.Caregiver.LastName,
                    PatientFirstName = reservation.Patient.FirstName,
                    PatientLastName = reservation.Patient.LastName,
                    CaregiverEmailAddress = reservation.Caregiver.Email,
                    PhoneNumber = reservation.Caregiver.PhoneNumber,
                    OrderId = reservation.OrderId,
                    Gender = reservation.Caregiver.Gender,
                    Status = reservation.Status,
                    TotalPrice = reservation.totalPrice

                };

                return Ok(dto);

            }


        }
        #endregion
    }
}
