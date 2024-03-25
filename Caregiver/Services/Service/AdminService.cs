using AutoMapper;
using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;

namespace Caregiver.Services.Service
{
	public class AdminService
	{

		private readonly IGenericRepo<CaregiverUser> _careGenericRepo;
		private readonly IMapper _mapper;
		public AdminService(IGenericRepo<CaregiverUser> genericRepo, IMapper mapper)
		{
			_careGenericRepo = genericRepo;
			_mapper = mapper;
		}


		//current + past Caregivers
		public async Task<IEnumerable<CaregiverCardDTO>> GetAllCurrentAndPastCaregiver()
		{

			//services
			IEnumerable<CaregiverUser> caregivers = await _careGenericRepo.GetAllAsync();
			//services
			if (caregivers != null)
			{
				return _mapper.Map<List<CaregiverCardDTO>>(caregivers);


			}
			return null;


		}

		//that have deleted themselves
		public async Task<IEnumerable<CaregiverCardDTO>> GetAllCurrentCaregiver()
		{
			IEnumerable<CaregiverUser> caregivers = await _careGenericRepo.GetAllAsync(a => a.IsDeleted == false);
			if (caregivers != null)
			{
				return _mapper.Map<List<CaregiverCardDTO>>(caregivers);


			}
			return null;


		}

		public async Task<object> AllCaregiver()
		{
			IEnumerable<CaregiverUser> caregivers = await _careGenericRepo.GetAllWithNavAsync(["Reservations"]);
			
			if (caregivers != null)
			{
				return caregivers;


			}
			return null;


		}








	}
}
