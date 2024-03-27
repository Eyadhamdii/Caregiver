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

		//get all caregivers which deleted and active and admin delete them 
		public async Task<List<AdminCaregiverDTO>> GetAllWithDeletedCaregivers()
		{
			var caregivers = _careGenericRepo.GetAllWithNavAsync("Reservations");

			//automappers..
			return caregivers.Select(Caregiver => _mapper.Map<AdminCaregiverDTO>(Caregiver)).ToList();


		}

		//get all current caregiver , accepted, active , admin didn't delete them => all titles
		public async Task<List<AdminCaregiverDTO>> GetAllCaregivers()
		{
			var caregivers = _careGenericRepo.GetAllWithNavAsync("Reservations", a => a.IsDeleted == false && a.IsAccepted == true && a.IsDeletedByAdmin == false);

			//automappers..
			return caregivers.Select(Caregiver => _mapper.Map<AdminCaregiverDTO>(Caregiver)).ToList();


		}


		//get all current caregiver , accepted, active , admin didn't delete them
		public async Task<List<AdminCaregiverDTO>> GetCaregiversJobTitle(string title)
		{
			var caregivers = _careGenericRepo.GetAllWithNavAsync("Reservations", a=>a.JobTitle == title && a.IsDeleted == false && a.IsAccepted == true && a.IsDeletedByAdmin == false);
			//automappers..
			return caregivers.Select(Caregiver => _mapper.Map<AdminCaregiverDTO>(Caregiver)).ToList();
		}


		#region Request Accept & Delete

		//is accepted => true
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

	//when decline the request..
		public async Task<bool> HardDeleteCaregiver(string id)
		{
			CaregiverUser caregiver = await _careGenericRepo.GetAsync(a => a.Id == id);
			if (caregiver == null)
			{
				return false;
			}
			var result = await _careGenericRepo.HardDeleteUser(caregiver);
			if (result == true) return true;
			return false;

		}

		#endregion



		#region Delete and GetBack Caregiver

		//is deleted by admin => true
		public async Task<bool> AdminDeleteCaregiver(string id)
		{
			CaregiverUser caregiver = await _careGenericRepo.GetAsync(a => a.Id == id);
			if (caregiver == null )
			{
				return false;
			}
			var result = await _careGenericRepo.AdminDeleteUser(caregiver);
			if (result == true) return true;
			return false;

		}


		//is deleted by admin => false ,, caregiver want her account back after the admin delete it
		
		public async Task<bool> AdminReturnCaregiver(string id)
		{
			CaregiverUser caregiver = await _careGenericRepo.GetAsync(a => a.Id == id);
			if (caregiver == null)
			{
				return false;
			}
			var result = await _careGenericRepo.AdminReturnUser(caregiver);
			if (result == true) return true;
			return false;

		}

		#endregion



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
