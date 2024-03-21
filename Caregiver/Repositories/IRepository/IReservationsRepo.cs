using Caregiver.Dtos;
using Caregiver.Models;

namespace Caregiver.Repositories.IRepository
{
    public interface IReservationsRepo
    {
        Task<IEnumerable<CaregiverPatientReservation>> GetAll();

        Task<IEnumerable<PatientsGetAllReservationDto>> GetPatientAllReservations();

        Task<CaregiverPatientReservation> GetReservationById(int id);
        Task<CaregiverPatientReservation> GetPatientReservationById(int id);

    }
}
