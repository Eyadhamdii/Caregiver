using Caregiver.Dtos;
using Caregiver.Models;

namespace Caregiver.Repositories.IRepository
{
    public interface IReservationsRepo
    {
        Task<IEnumerable<ReservationDto>> GetAll();

        Task<IEnumerable<ReservationDto>> GetPatientAllReservations();

        Task<IEnumerable<ReservationDto>> GetCaregiverAllReservations();
        Task<IEnumerable<CaregiverPatientReservation>> GetReservationsByStatus(string status);

        Task<CaregiverPatientReservation> GetReservationById(int id);
        Task<CaregiverPatientReservation> GetPatientReservationById(int id);
        Task<CaregiverPatientReservation> GetCaregiverReservationById(int id);

        Task<CaregiverPatientReservation> AddReservarion(CaregiverPatientReservation CaregiverPatientReservation);

        Task<ReservationDates> AddReservationDates(ReservationDates ReservationDates);

        bool CheckReservationDatesExists(int id);

        CaregiverPatientReservation UpdateReservationStatus(CaregiverPatientReservation CaregiverPatientReservation);

        Task <IEnumerable<ReservationDates>> GetReservationDatesById(int id);

        Task<IEnumerable<ReservationDates>> DeleteReservationStatus(int id);

        Task SendEmailAsync(string email, string subject, string message);

    }
}
