using Caregiver.Dtos;

namespace Caregiver.Repositories.IRepository
{
    public interface IScheduleRepo
    {
        Task<UserManagerResponse>AddScheduleAsync(ScheduleDTO model);
    }
}
