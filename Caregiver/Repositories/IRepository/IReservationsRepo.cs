using Caregiver.Models;

namespace Caregiver.Repositories.IRepository
{
    public interface IReservationsRepo
    {
        Task<IEnumerable<CaregiverPatientReservation>> GetAll();

        Task<CaregiverPatientReservation> GetReservationById(int id);

    }
}
