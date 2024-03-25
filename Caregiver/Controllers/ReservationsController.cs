using AutoMapper;
using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Caregiver.Repositories.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Caregiver.Enums.Enums;

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
		//  private readonly ICaregiverRepo _dbCaregiver;
		private readonly IGenericRepo<CaregiverUser> _dbCaregiver;
        private readonly IEmailRepo _IEmailRepo;


        public ReservationsController(ApplicationDBContext context, IReservationsRepo _reservationsRepo, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, /*ICaregiverRepo dbCaregiver*/   IGenericRepo<CaregiverUser> dbCaregiver, IEmailRepo iEmailRepo)
        {
            reservationsRepo = _reservationsRepo;
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _dbCaregiver = dbCaregiver;
            _IEmailRepo = iEmailRepo;
        }


        #region Get all reservations to admin
        [HttpGet]
        public async Task <IActionResult> GetAllReservations()
        {
            var reservations = await reservationsRepo.GetAll();
            if (reservations == null) return BadRequest();
            else
            {

                _IEmailRepo.SendEmail("Hello Eman","Trying this function","sohilaafify23@gmail.com");
            return Ok(reservations);

        }

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


        #region Get all reservations to patient
        [HttpGet("CaregiverReservations")]
        public async Task<IActionResult> GetAllReservationsToCaregiver()
        {

            var reservations = await reservationsRepo.GetCaregiverAllReservations();


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
                    CaregiverFirstName = reservation.Caregiver.FirstName,
                    CaregiverLastName = reservation.Caregiver.LastName,
                    PatientFirstName = reservation.Patient.FirstName,
                    PatientLastName = reservation.Patient.LastName,
                    CaregiverEmailAddress = reservation.Caregiver.Email,
                    CaregiverPhoneNumber = reservation.Caregiver.PhoneNumber,
                    PatientEmailAddress = reservation.Patient.Email,
                    PatientPhoneNumber = reservation.Patient.PhoneNumber,
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    Photo= reservation.Caregiver.Photo,
                    Country=reservation.Caregiver.Country,
                    OrderId = reservation.OrderId,
                    Gender = reservation.Caregiver.Gender,
                    Status = reservation.Status.ToString(),
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
            /*
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
                             CaregiverFirstName = reservation.Caregiver.FirstName,
                             CaregiverLastName = reservation.Caregiver.LastName,
                             PatientFirstName = reservation.Patient.FirstName,
                             PatientLastName = reservation.Patient.LastName,
                             CaregiverEmailAddress = reservation.Caregiver.Email,
                             CaregiverPhoneNumber = reservation.Caregiver.PhoneNumber,
                             OrderId = reservation.OrderId,
                             Gender = reservation.Caregiver.Gender,
                             Status = reservation.Status,
                             TotalPrice = reservation.totalPrice
                         };

                         return Ok(dto);*/

            var Patientreservation = await reservationsRepo.GetPatientReservationById(id);
            var Caregivrerreservation = await reservationsRepo.GetCaregiverReservationById(id);

            if (Patientreservation == null && Caregivrerreservation == null)
            {
                return NotFound();
            }

            var reservation = Patientreservation ?? Caregivrerreservation;

            var dto = new ReservationDto
            {
                PatientId = reservation.PatientId,
                CaregiverFirstName = reservation.Caregiver.FirstName,
                CaregiverLastName = reservation.Caregiver.LastName,
                PatientFirstName = reservation.Patient.FirstName,
                PatientLastName = reservation.Patient.LastName,
                CaregiverEmailAddress = reservation.Caregiver.Email,
                CaregiverPhoneNumber = reservation.Caregiver.PhoneNumber,
                OrderId = reservation.OrderId,
                Gender = reservation.Caregiver.Gender,
                Status = reservation.Status,
                TotalPrice = reservation.totalPrice
            };

            return Ok(dto);
        }
        
        #endregion

        #region Reserve a caregiver
        [HttpPost]
        public async Task<IActionResult> CreateReservationsAsync([FromForm] PostReservationDto  dto, [FromQuery] string CaregiverId)
        {
            //edits to use generic 
            var caregiver = await _dbCaregiver.GetAsync(a => a.Id == CaregiverId);
            var pricePerDay = caregiver.PricePerDay;
            string reservationStatus = ReservationStatus.OnProgress.ToString();
            var calculatedValue = CalculateTotalPrice(pricePerDay, dto.EndDate, dto.StartDate);
            var loggedInUserId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);


            if (caregiver == null)
            {
                return NotFound();
            }

            var caregiverPatientReservation = new CaregiverPatientReservation
            {
            CaregiverId= CaregiverId,
            PatientId=loggedInUserId,
            Status = reservationStatus.ToString(),
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            totalPrice = calculatedValue,
            };

            #region Email section
            var r = "sohilaafify23@gmail.com";
            var s = "Test subject";
            var m = "Your Reservation is confirmed!";
            await reservationsRepo.SendEmailAsync(r, s, m);
            #endregion

            await reservationsRepo.AddReservarion(caregiverPatientReservation);

            return Ok(dto);
        }

        #endregion


        #region  Confirm or reject or cancel reservation request
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservationStatusAsync(int id, [FromForm] ReservationStatusDto dto)
        {
            var reservation = await  reservationsRepo.GetReservationById(id);

            if (reservation == null) return NotFound($"No reservation was found with ID: {id}");

            if (dto.Status == ReservationStatus.Confirmed)
            {
                reservation.Status = dto.Status.ToString();
              if(reservationsRepo.CheckReservationDatesExists(reservation.OrderId)) { return BadRequest(); }
              else { AddDatesToDatabase(reservation); }
                   
                
            }
            else if (dto.Status == ReservationStatus.CannotProceed || dto.Status == ReservationStatus.Cancelled)
            {
                reservation.Status = dto.Status.ToString()  ;
                await reservationsRepo.DeleteReservationStatus(id);
            }
            reservationsRepo.UpdateReservationStatus(reservation);
            return Ok(dto);
        }
        #endregion


        #region Add dates to database
        protected async void AddDatesToDatabase(CaregiverPatientReservation reservation)
        {

            for (DateTime date = reservation.StartDate; date <= reservation.EndDate; date = date.AddDays(1))
            {
                ReservationDates reservationDate = new ReservationDates
                {
                    OrderId = reservation.OrderId,
                    ReservationDate = date
                };
                await reservationsRepo.AddReservationDates(reservationDate);
            }
        }
        #endregion

        #region Calculate Total Price method
        private int CalculateTotalPrice(int pricePerHour, DateTime EndDate, DateTime startDate)
        {
            TimeSpan difference = EndDate - startDate;
            int totalDays = ((int)difference.TotalDays)+1;

            return pricePerHour * totalDays;
        }
        #endregion

        #region Remove Reservation Dates
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveReservationDates(int id)
        {
            var reservation = await reservationsRepo.DeleteReservationStatus(id);
            if (reservation == null) return NotFound($"No reservation was found with ID: {id}");
            return Ok(reservation);

        }
        #endregion




        #region in case i needed  this 
        // var pricrPerDay = _context.Caregivers.Select(a => a.PricePerDay).FirstOrDefaultAsync(a => dto.CaregiverId == dto.CaregiverId);
        // var loggedInUserId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
        // ReservationStatus ReservationStatus = ReservationStatus.OnProgress;
        // var calculatedValue = CalculateTotalPrice(pricrPerDay);
        //var CaregiverPatientReservation = new CaregiverPatientReservation
        // {
        //    PatientId= dto.PatientId,
        // CaregiverId = dto.CaregiverId,
        // StartDate = dto.StartDate,
        //     EndDate = dto.EndDate,
        //     totalPrice = dto.totalPrice,
        // };
        // CaregiverPatientReservation.PatientId = loggedInUserId;
        // CaregiverPatientReservation.Status = ReservationStatus;

        // CaregiverPatientReservation.totalPrice= calculatedValue;
        // await _context.Reservations.AddAsync(CaregiverPatientReservation);
        // _context.SaveChanges();
        // return Ok(dto);


        /*
List<DateTime> generatedDates = new List<DateTime>();

for (DateTime date = reservation.StartDate; date <= reservation.EndDate; date = date.AddDays(1))
{
    generatedDates.Add(date);
}

// Now you can save the generated dates to the ReservationDates table
foreach (DateTime generatedDate in generatedDates)
{
    ReservationDates reservation1 = new ReservationDates
    {
        OrderId = reservation.OrderId,
        ReservationDate = generatedDate
    };

    // Save the reservation to the database
    _context.ReservationDates.Add(reservation1);
}

// Save changes to the database

_context.SaveChanges();
*/

        #endregion
    }
}
