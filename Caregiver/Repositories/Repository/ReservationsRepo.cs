using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<CaregiverPatientReservation>> GetAll()
        {
           
            return await db.Reservations.Include(m => m.Caregiver).Include(m => m.Patient).ToListAsync();
        }

        public async Task<IEnumerable<PatientsGetAllReservationDto>> GetPatientAllReservations()
        {
            var loggedInUserId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

            return  await db.Reservations.Include(p=>p.Caregiver).Where(r => r.PatientId == loggedInUserId).Select(source => new PatientsGetAllReservationDto
            {
                FirstName = source.Caregiver.FirstName,
                LastName = source.Caregiver.LastName,
                EmailAddress = source.Caregiver.Email,
                Photo = source.Caregiver.Photo,
                PhoneNumber= source.Caregiver.PhoneNumber,
                OrderId=source.OrderId,
                Status = source.Status,
                Gender = source.Caregiver.Gender,
                TotalPrice=source.totalPrice,
                StartDate=source.StartDate,
                PatientId=source.PatientId
                
            }).ToListAsync();
           
        }

        public async Task<CaregiverPatientReservation> GetPatientReservationById(int id)
        {
            var loggedInUserId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            return await db.Reservations.Include(m => m.Caregiver).Include(m => m.Patient).Where(r => r.PatientId == loggedInUserId).FirstOrDefaultAsync(g => g.OrderId == id);
        }

        public async Task<CaregiverPatientReservation> GetReservationById(int id)
        {
            return await db.Reservations.Include(m => m.Caregiver).Include(m => m.Patient).FirstOrDefaultAsync(g => g.OrderId == id);
        }

    }
}
