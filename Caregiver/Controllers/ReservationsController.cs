using AutoMapper;
using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Caregiver.Repositories.Repository;
using Caregiver.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
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
        private readonly ISmsServicecs _smsServicecs;
        private readonly IConfiguration _configuration;
       

        public ReservationsController(IConfiguration configuration, ApplicationDBContext context, IReservationsRepo _reservationsRepo, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, /*ICaregiverRepo dbCaregiver*/   IGenericRepo<CaregiverUser> dbCaregiver, IEmailRepo iEmailRepo, ISmsServicecs smsServicecs)
        {
            reservationsRepo = _reservationsRepo;
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _dbCaregiver = dbCaregiver;
            _IEmailRepo = iEmailRepo;
            _smsServicecs = smsServicecs;
            _configuration = configuration;
           
        }


        #region Get all reservations to admin
        [HttpGet("admin")]
        public async Task <IActionResult> GetAllReservations()
        {
            var reservations = await reservationsRepo.GetAll();
            if (reservations == null) return BadRequest();
            
            await   _IEmailRepo.SendEmail("Hello Eman","Trying this function","sohilaafify23@gmail.com");
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
                    CaregiverGender = reservation.Caregiver.Gender,
                    PatientGender = reservation.Patient.Gender,
                    Status = reservation.Status.ToString(),
                    TotalPrice= reservation.TotalPrice,
                    TotalPriceWithfees = reservation.TotalPriceWithfees,
                    Fees = reservation.Fees,
                    PricePerDay = reservation.Caregiver.PricePerDay,
                    JobTitle= reservation.Caregiver.JobTitle
                };

                return Ok(dto);

            }
                
               
        }   
        #endregion
         
        #region Get Patient reservation by id
        [HttpGet("PatientReservation")]
        public async Task<IActionResult> GetPatientReservation(int id)
        {

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
                PatientEmailAddress = reservation.Patient.Email,
                PatientPhoneNumber = reservation.Patient.PhoneNumber,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                Photo = reservation.Caregiver.Photo,
                Country = reservation.Caregiver.Country,
                OrderId = reservation.OrderId,
                CaregiverGender = reservation.Caregiver.Gender,
                PatientGender = reservation.Patient.Gender,
                Status = reservation.Status.ToString(),
                TotalPrice = reservation.TotalPrice,
                TotalPriceWithfees = reservation.TotalPriceWithfees,
                Fees = reservation.Fees,
                PricePerDay = reservation.Caregiver.PricePerDay,
                JobTitle = reservation.Caregiver.JobTitle
            };

            return Ok(dto);
        }
        
        #endregion

        #region Reserve a caregiver
        [HttpPost]
        public async Task<IActionResult> CreateReservationsAsync([FromBody] PostReservationDto  dto, [FromQuery] string CaregiverId)
        {
            //edits to use generic 
            var caregiver = await _dbCaregiver.GetAsync(a => a.Id == CaregiverId);
            var pricePerDay = caregiver.PricePerDay;
            string reservationStatus = ReservationStatus.OnProgress.ToString();
            var calculatedValue = CalculateTotalPrice(pricePerDay, dto.EndDate, dto.StartDate);
            var loggedInUserId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);


            if (caregiver == null || loggedInUserId == null)
            {
                return NotFound();
            }

            var caregiverPatientReservation = new CaregiverPatientReservation
            {
                CaregiverId = CaregiverId,
                PatientId = loggedInUserId,
                Status = reservationStatus.ToString(),
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                TotalPrice = calculatedValue,
                TotalPriceWithfees = calculatedValue + (calculatedValue * 0.1),
                Fees = calculatedValue * 0.1,
                LastStatusUpdate = DateTime.Now,
               
            };

            #region Email section
            // Caregiver
            await _IEmailRepo.SendEmail( $"Dear {caregiver.FirstName}, Please take a moment to verify the reservation that have been sent to you and confirm them as soon as possible.","New reservation request!",caregiver.Email);

            // Patient
            await _IEmailRepo.SendEmail( $"Dear {caregiver.FirstName}, Thank you for choosing our caregiver services. We appreciate your patience, and we will make it a priority to contact you promptly.", "Your Caregiver Reservation request is sent!", caregiver.Email);
            #endregion

            #region Send SMS
            //var result = _smsServicecs.sendMessage("+201096669249", "Test Test");
            //if(!string.IsNullOrEmpty(result.ErrorMessage))
            //    return BadRequest(result.ErrorMessage);
            #endregion

            await reservationsRepo.AddReservarion(caregiverPatientReservation);

            return Ok(caregiverPatientReservation.OrderId);
        }

        #endregion


        #region  Confirm or reject or cancel reservation request
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservationStatusAsync(int id, [FromForm] ReservationStatusDto dto)
        {
            var reservation = await  reservationsRepo.GetReservationById(id);

            if (reservation == null) return NotFound($"No reservation was found with ID: {id}");

         if (dto.Status == ReservationStatus.Pending)
            {
                reservation.Status = dto.Status.ToString();
                #region Email section
                // Patient
                await _IEmailRepo.SendEmail($"Dear {reservation.Patient.FirstName},  Thank you for choosing our services and reserving a nurse. We regret to inform you that the nurse you requested is currently \r\nunavailable to fulfill your request. We understand the importance of your healthcare needs and apologize for any inconvenience caused. We are committed to providing you with the best care possible and would be happy to assist you in finding an alternative solution or recommending another qualified healthcare professional.", "Oopsie Daisies! Your  Reservation Adventure Takes a Twist - Let's Explore Plan B!", reservation.Patient.Email);
                #endregion
                #region Send SMS
                // Patient
                // var PatientSms = _smsServicecs.sendMessage("+201096669249", $"Dear {reservation.Patient.FirstName}, Thank you for choosing our services and reserving a nurse. We regret to inform you that the nurse you requested is currently \r\nunavailable to fulfill your request. We understand the importance of your healthcare needs and apologize for any inconvenience caused. We are committed to providing you with the best care possible and would be happy to assist you in finding an alternative solution or recommending another qualified healthcare professional.");

                //if (!string.IsNullOrEmpty(PatientSms.ErrorMessage))
                //    return BadRequest(PatientSms.ErrorMessage);
                #endregion
             
            }

            else if (dto.Status == ReservationStatus.CannotProceed)
            {
                reservation.Status = dto.Status.ToString();
                #region Email section
                // Patient
                await _IEmailRepo.SendEmail($"Dear {reservation.Patient.FirstName},  Thank you for choosing our services and reserving a nurse. We regret to inform you that the nurse you requested is currently \r\nunavailable to fulfill your request. We understand the importance of your healthcare needs and apologize for any inconvenience caused. We are committed to providing you with the best care possible and would be happy to assist you in finding an alternative solution or recommending another qualified healthcare professional.", "Oopsie Daisies! Your  Reservation Adventure Takes a Twist - Let's Explore Plan B!", reservation.Patient.Email);
                #endregion
                #region Send SMS
                // Patient
                var PatientSms = _smsServicecs.sendMessage("+201096669249", $"Dear {reservation.Patient.FirstName}, Thank you for choosing our services and reserving a nurse. We regret to inform you that the nurse you requested is currently \r\nunavailable to fulfill your request. We understand the importance of your healthcare needs and apologize for any inconvenience caused. We are committed to providing you with the best care possible and would be happy to assist you in finding an alternative solution or recommending another qualified healthcare professional.");

                if (!string.IsNullOrEmpty(PatientSms.ErrorMessage))
                    return BadRequest(PatientSms.ErrorMessage);
                #endregion
            }
            else if (dto.Status == ReservationStatus.Cancelled)
            {
                // Check if the time is before 24 hours from the start date
                var timeDifference = reservation.StartDate - DateTime.UtcNow;
                if (timeDifference.TotalHours < 24)
                {
                    return BadRequest("Cancellation is not allowed within 24 hours of the start date.");
                }
                #region Email section

                // Caregiver
                await _IEmailRepo.SendEmail($"Dear {reservation.Caregiver.FirstName}, We regret to inform you that the customer,{reservation.Patient.FirstName}, has canceled their reservation with you for (Reservation ID: ({{reservation.OrderId}}). They have encountered unforeseen circumstances and will not require your services at that time. We apologize for any inconvenience caused. If you have any questions, please let us know. Thank you for your understanding. Best regards", "Reservation Cancellation Alert!", reservation.Caregiver.Email);
                #endregion

                #region Send SMS
                // Caregiver
                var result = _smsServicecs.sendMessage("+201096669249", $"Dear {reservation.Caregiver.FirstName}, We regret to inform you that the customer,{reservation.Patient.FirstName}, has canceled their reservation with you for (Reservation ID: ({reservation.OrderId}). They have encountered unforeseen circumstances and will not require your services at that time. " +
                    $"We apologize for any inconvenience caused. If you have any questions, please let us know. Thank you for your understanding. Best regards");
                if (!string.IsNullOrEmpty(result.ErrorMessage))
                    return BadRequest(result.ErrorMessage);
                #endregion

                reservation.Status = dto.Status.ToString();
                await reservationsRepo.DeleteReservationStatus(id);
            }
            reservation.LastStatusUpdate = DateTime.Now;
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


        private int CalculateTotalPrice(int pricePerHour, DateTime endDate, DateTime startDate)
        {
            TimeSpan difference = endDate.Date - startDate.Date;
            int totalDays = difference.Days + 1;

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

        [HttpPost("ConfirmReservations")]
        public async Task<IActionResult> ConfirmReservation([FromBody] sessionIdDto sessionIddto)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            string sessionId = sessionIddto.sessionId;
            var service = new Stripe.Checkout.SessionService();
            var session = service.Get(sessionId);
            string reservationStatus = ReservationStatus.Confirmed.ToString();
            var paymentStatus = session.PaymentStatus;
            var orderId = session.Metadata["order_id"];
            var reservation = await reservationsRepo.GetReservationById(int.Parse(orderId));
            if (paymentStatus == "paid")
            {
                if (reservationsRepo.CheckReservationDatesExists(reservation.OrderId)) { return BadRequest(); }
                else
                {
                    reservation.Status = reservationStatus;
                    AddDatesToDatabase(reservation);

                    /*
                    #region Email section
                    // Caregiver
                    await _IEmailRepo.SendEmail($"Dear {reservation.Caregiver.FirstName}, Thank you for choosing our caregiver services. We are excited to confirm your reservation", "Your Caregiver Reservation is Confirmed!", reservation.Caregiver.Email);

                    // Patient
                    await _IEmailRepo.SendEmail($"Dear {reservation.Patient.FirstName}, Thank you for choosing our caregiver services. We are excited to confirm your reservation", "You confirmed your reservation!", reservation.Patient.Email);
                    #endregion

                    #region Send SMS
                    // Patient
                    var PatientSms = _smsServicecs.sendMessage("+201096669249", $"Dear {reservation.Patient.FirstName}, Your Caregiver Reservation is Confirmed!, Thank you for choosing our caregiver services. We are excited to confirm your reservation");

                    // Caregiver
                    var CaregiverSms = _smsServicecs.sendMessage("+201096669249", $"Dear {reservation.Caregiver.FirstName}, You confirmed your reservation!, Thank you for choosing our caregiver services. We are excited to confirm your reservation");
                    var Sms = PatientSms ?? CaregiverSms;

                    if (!string.IsNullOrEmpty(Sms.ErrorMessage))
                        return BadRequest(Sms.ErrorMessage);


                    #endregion
                    */
                
                }
            }else
            {
                return BadRequest("Something went wrong");
            }

            return Ok($"Dates are added {session.PaymentIntentId}" );

        }


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


        /*
var timeDifference = DateTime.UtcNow - reservation.LastStatusUpdate;
if (timeDifference.TotalHours > 24)
{
    // Check if 24 hours have passed since the request time
    reservation.Status = ReservationStatus.Cancelled.ToString();
    return BadRequest("Reservation is cancelled as 24 hours have passed since the request time.");
}*/

        /*
         *         private int CalculateTotalPrice(int pricePerHour, DateTime EndDate, DateTime startDate)
        {
            TimeSpan difference = EndDate - startDate;
            int totalDays = ((int)difference.TotalDays)+1;

            return pricePerHour * totalDays;
        }
         */
        #endregion
    }
}
