using AutoMapper;
using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Caregiver.Repositories.Repository;
using Caregiver.Services.IService;

namespace Caregiver.Services.Service
{
	public class AdminService : IAdminService
	{

		private readonly IGenericRepo<CaregiverUser> _careGenericRepo;
		private readonly IMapper _mapper;
		private readonly IAdminRepo _adminRepo;

		public AdminService(IGenericRepo<CaregiverUser> careGenericRepo, IMapper mapper, IAdminRepo adminRepo)
		{
			_careGenericRepo = careGenericRepo;
			_mapper = mapper;
			_adminRepo = adminRepo;
		}
		
		
		public async Task<List<AdminCaregiverDTO>> GetAllCaregivers()
		{
			var caregivers =  _careGenericRepo.GetAllWithNavAsync("Reservations");

			//automappers..
			return caregivers.Select(Caregiver => _mapper.Map<AdminCaregiverDTO>(Caregiver)).ToList();


		}

		public async Task<List<AdminCaregiverDTO>> GetCaregiversJobTitle(string title)
		{
			var caregivers = _careGenericRepo.GetAllWithNavAsync("Reservations", a=>a.JobTitle == title);
			//automappers..
			return caregivers.Select(Caregiver => _mapper.Map<AdminCaregiverDTO>(Caregiver)).ToList();
		}



		public async Task<bool> AcceptRequestAsync(string id)
		{
			CaregiverUser caregiver = await _careGenericRepo.GetAsync(a => a.Id == id);
			if (caregiver == null)
			{
				return false;
			}
			var result = await _adminRepo.AcceptRequest(caregiver);
			if (result == true) return true;
			return false;

		}


		public async Task<bool> HardDeleteCaregiver(string id)
		{
			CaregiverUser caregiver = await _careGenericRepo.GetAsync(a => a.Id == id);
			if (caregiver == null && caregiver.IsAccepted == true)
			{
				return false;
			}
			var result = await _careGenericRepo.HardDeleteUser(caregiver);
			if (result == true) return true;
			return false;

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







	}
}
