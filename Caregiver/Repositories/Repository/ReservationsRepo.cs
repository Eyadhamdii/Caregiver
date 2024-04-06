using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Net.Mail;

namespace Caregiver.Repositories.Repository
{
    public class ReservationsRepo : IReservationsRepo
    {
        private readonly ApplicationDBContext db;
        private UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public ReservationsRepo(ApplicationDBContext _db, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {

            db = _db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        //to admin 
        public async Task<IEnumerable<ReservationDto>> GetAll()
        {

            return await db.Reservations.Include(m => m.Caregiver).Include(m => m.Patient).Select(source => new ReservationDto
            {
                CaregiverFirstName = source.Caregiver.FirstName,
                CaregiverLastName = source.Caregiver.LastName,
                CaregiverEmailAddress = source.Caregiver.Email,
                Photo = source.Caregiver.Photo,
                CaregiverPhoneNumber = source.Caregiver.PhoneNumber,
                OrderId = source.OrderId,
                Status = source.Status,
                CaregiverGender = source.Caregiver.Gender,
                PatientGender = source.Patient.Gender,
                TotalPrice = source.TotalPrice,
                TotalPriceWithfees = source.TotalPriceWithfees,
                Fees = source.Fees,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                PatientId = source.PatientId,
                PricePerDay = source.Caregiver.PricePerDay,
                PatientFirstName = source.Patient.FirstName,
                PatientLastName = source.Patient.LastName,
                PatientPhoneNumber = source.Patient.PhoneNumber,
                JobTitle = source.Caregiver.JobTitle,
                PatientEmailAddress = source.Patient.Email
            }).ToListAsync();
        }

        public async Task<IEnumerable<ReservationDto>> GetPatientAllReservations()
        {
            var loggedInUserId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);


            return await db.Reservations.Include(p => p.Caregiver).Include(p => p.Patient).Where(r => r.PatientId == loggedInUserId).Select(source => new ReservationDto
            {
                CaregiverFirstName = source.Caregiver.FirstName,
                CaregiverLastName = source.Caregiver.LastName,
                CaregiverEmailAddress = source.Caregiver.Email,
                Photo = source.Caregiver.Photo,
                CaregiverPhoneNumber = source.Caregiver.PhoneNumber,
                OrderId = source.OrderId,
                Status = source.Status,
                CaregiverGender = source.Caregiver.Gender,
                PatientGender = source.Caregiver.Gender,
                TotalPrice = source.TotalPrice,
                StartDate = source.StartDate,
                PatientId = source.PatientId,
                TotalPriceWithfees = source.TotalPriceWithfees,
                Fees = source.Fees,
                EndDate = source.EndDate,
                PricePerDay = source.Caregiver.PricePerDay,
                PatientFirstName = source.Patient.FirstName,
                PatientLastName = source.Patient.LastName,
                PatientPhoneNumber = source.Patient.PhoneNumber,
                PatientEmailAddress = source.Patient.Email


            }).ToListAsync();

        }




        public async Task<CaregiverPatientReservation> GetPatientReservationById(int id)
        {
            var loggedInUserId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            return await db.Reservations.Include(m => m.Caregiver).Include(m => m.Patient).Where(r => r.PatientId == loggedInUserId).FirstOrDefaultAsync(g => g.OrderId == id);
        }


        public async Task<IEnumerable<ReservationDto>> GetCaregiverAllReservations()
        {
            var loggedInUserId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

            return await db.Reservations.Include(p => p.Caregiver).Include(p => p.Patient).Where(r => r.CaregiverId == loggedInUserId).Select(source => new ReservationDto
            {
                CaregiverFirstName = source.Caregiver.FirstName,
                CaregiverLastName = source.Caregiver.LastName,
                CaregiverEmailAddress = source.Caregiver.Email,
                Photo = source.Caregiver.Photo,
                CaregiverPhoneNumber = source.Caregiver.PhoneNumber,
                OrderId = source.OrderId,
                Status = source.Status,
                CaregiverGender = source.Caregiver.Gender,
                PatientGender = source.Patient.Gender,
                TotalPrice = source.TotalPrice,
                StartDate = (DateTime)source.StartDate,
                PatientId = source.PatientId,
                TotalPriceWithfees = source.TotalPriceWithfees,
                Fees = source.Fees,
                EndDate = source.EndDate,
                PricePerDay = source.Caregiver.PricePerDay,
                JobTitle = source.Caregiver.JobTitle,
                PatientFirstName = source.Patient.FirstName,
                PatientLastName = source.Patient.LastName,
                PatientPhoneNumber = source.Patient.PhoneNumber,
                PatientEmailAddress = source.Patient.Email


            }).ToListAsync();

        }

        public async Task<CaregiverPatientReservation> GetCaregiverReservationById(int id)
        {
            var loggedInUserId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
			return await db.Reservations.Include(m => m.Caregiver).Include(m => m.Patient).Where(r => r.CaregiverId == loggedInUserId).FirstOrDefaultAsync(g => g.OrderId == id);
        }

        public async Task<CaregiverPatientReservation> GetReservationById(int id)
        {
			var reserve = await db.Reservations.Where(m => m.OrderId == id).FirstOrDefaultAsync();
			if (reserve != null && reserve.DependentId != null)
			{
				return await db.Reservations.Include(m => m.Caregiver).Where(m => m.OrderId == id).Include(a => a.Dependant).FirstOrDefaultAsync(a => a.DependentId == reserve.DependentId);
			}
			return await db.Reservations.Include(m => m.Caregiver).Include(m => m.Patient).FirstOrDefaultAsync(g => g.OrderId == id);
        }

        //public Task SendEmailAsync(string email, string subject, string message)
        //{
        //    var client = new SmtpClient("smtp.gmail.com", 587)
        //    {
        //        EnableSsl = true,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential("caregiverteam23@gmail.com", "jrzg jygk lbkv reqg")
        //    };

        //    return client.SendMailAsync(
        //        new MailMessage(from: "caregiverteam23@gmail.com",
        //                        to: email,
        //                        subject,
        //                        message
        //                        ));
        //}

        public async Task<CaregiverPatientReservation> AddReservarion(CaregiverPatientReservation CaregiverPatientReservation)
        {
            await db.Reservations.AddAsync(CaregiverPatientReservation);
            db.SaveChanges();
            return (CaregiverPatientReservation);
        }

        public async Task<ReservationDates> AddReservationDates(ReservationDates ReservationDates)
        {
            await db.ReservationDates.AddAsync(ReservationDates);
            db.SaveChanges();
            return (ReservationDates);
        }

        public CaregiverPatientReservation UpdateReservationStatus(CaregiverPatientReservation CaregiverPatientReservation)
        {
            db.Update(CaregiverPatientReservation);
            db.SaveChanges();
            return (CaregiverPatientReservation);
        }

        public async Task<IEnumerable<ReservationDates>> GetReservationDatesById(int id)
        {
            return await db.ReservationDates.Where(p => p.OrderId == id).ToListAsync();
        }

        public async Task<IEnumerable<ReservationDates>> DeleteReservationStatus(int id)
        {
            var dates = await GetReservationDatesById(id);
            db.RemoveRange(dates);
            db.SaveChanges();
            return (dates);
        }

        public bool CheckReservationDatesExists(int id)
        {
            return db.ReservationDates.Select(item => item.OrderId).Contains(id);
        }

        public async Task<IEnumerable<CaregiverPatientReservation>> GetReservationsByStatus(string status)
        {
            return await db.Reservations.Where(r => r.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<ReservationsDateDto>> GetAllReservationsDateDto(string id)
        {
            return await db.ReservationDates.Where(r => r.CaregiverId == id).Select(source => new ReservationsDateDto
            {
                ReservationDate = source.ReservationDate,
                CaregiverId = source.CaregiverId
            }).ToListAsync(); ;
        }

        public async Task<IEnumerable<TransactionsDto>> GetAllTransactions()
        {

            return await db.Reservations.Include(m => m.Caregiver).Include(m => m.Patient).Where(r => r.Status == "Confirmed").Select(source => new TransactionsDto
            {
                FirstName = source.Patient.FirstName,
                LastStatusUpdate = source.LastStatusUpdate,
                Status=source.Status,
                PatientId=source.PatientId,
                TotalPriceWithfees=source.TotalPriceWithfees,
            }).ToListAsync();
        }
    }
}