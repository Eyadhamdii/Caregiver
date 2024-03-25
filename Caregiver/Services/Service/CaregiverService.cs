using AutoMapper;
using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Caregiver.Services.IService;

namespace Caregiver.Services.Service
{
    public class CaregiverService  : ICaregiverService
	{
		private readonly IGenericRepo<CaregiverUser> _careGenericRepo;
		private readonly IMapper _mapper;

		public CaregiverService(IGenericRepo<CaregiverUser> genericRepo, IMapper mapper)
		{
			_careGenericRepo = genericRepo;
			_mapper = mapper;
		}


		public async Task<IEnumerable<CaregiverCardDTO>> GetAllCurrentCaregiver()
		{
			
				//services
				IEnumerable<CaregiverUser> caregivers = await _careGenericRepo.GetAllAsync(a => a.IsDeleted == false);
				//services
				if(caregivers != null)
				{
				return _mapper.Map<List<CaregiverCardDTO>>(caregivers);
				

				} return null;

		
		}

		public async Task<IEnumerable<CaregiverCardDTO>> GetAllCaregiverByType(string Role) {
			if (Role != "Caregiver" && Role != "Nurse" && Role != "BabySitter") return null;

			IEnumerable<CaregiverUser> caregivers = await _careGenericRepo.GetAllAsync(a => a.JobTitle == Role && a.IsDeleted == false);
				if(caregivers != null)
				{
					return _mapper.Map<List<CaregiverCardDTO>>(caregivers);
				}
			return null;
			
		}

		public async Task<bool> SoftDeleteCaregiver(string id)
		{
			CaregiverUser caregiver = await _careGenericRepo.GetAsync(a => a.Id == id);
				if (caregiver == null)
				{
					return false;
				}
			var result = await _careGenericRepo.SoftDeleteUser(caregiver);
			if (result == true) return true;
			return false;

		}

		//i should return caregiver dto not the model
		public async Task<CaregiverUser> GetCaregiverById(string id)
		{
			CaregiverUser caregiver = await _careGenericRepo.GetAsync(a => a.Id == id);
			if (caregiver == null)
			{
				return null;
			}

			return caregiver;
		}

		public async Task<CaregiverUpdateDTO> UpdateCaregiverAsync(string id, CaregiverUpdateDTO caregiverUpdate)
		{
			
				CaregiverUser caregiverToUpdate = await _careGenericRepo.GetAsync(a => a.Id == id);
				if (caregiverToUpdate == null) return null;

				//It is used for updating the properties of an existing CaregiverUser object (caregiverToUpdate) with the values provided in the caregiverUpdate DTO.
				_mapper.Map(caregiverUpdate, caregiverToUpdate);

				var result = await _careGenericRepo.UpdateUserAsync(caregiverToUpdate);
				if (result == null) return null;

				return _mapper.Map<CaregiverUpdateDTO>(_mapper.Map(caregiverUpdate, caregiverToUpdate));

		}



	}
}
