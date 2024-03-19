using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Caregiver.Repositories.Repository
{
    public class ReservationsRepo : IReservationsRepo
    {
        private readonly ApplicationDBContext db;

        public ReservationsRepo(ApplicationDBContext _db)
        {
            db = _db;
        }

        public async Task<IEnumerable<CaregiverPatientReservation>> GetAll()
        {
            return await db.Reservations.Include(m => m.Caregiver).Include(m => m.Patient).ToListAsync();
        }

        public async Task<CaregiverPatientReservation> GetReservationById(int id)
        {
            return await db.Reservations.Include(m => m.Caregiver).Include(m => m.Patient).FirstOrDefaultAsync(g => g.OrderId == id);
        }
    }
}
