using AutoMapper;
using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Caregiver.Services.Service
{
	public class CaregiverSrevice
	{
		private readonly IGenericRepo<CaregiverUser> _genericRepo;
		private readonly ICaregiverRepo _dbCaregiver;
		private readonly APIResponse _response;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;

		public CaregiverSrevice(IGenericRepo<CaregiverUser> genericRepo, ICaregiverRepo dbCaregiver, APIResponse response, IMapper mapper, UserManager<User> userManager)
		{
			_genericRepo = genericRepo;
			_dbCaregiver = dbCaregiver;
			_response = response;
			_mapper = mapper;
			_userManager = userManager;
		}

		public async Task<ActionResult<IEnumerable<CaregiverCardDTO>>> GetAllCurrentCaregiver()
		{
			
				//services
				IEnumerable<CaregiverUser> caregivers = await _genericRepo.GetAllAsync(a => a.IsDeleted == false);
				//services
				if(caregivers != null)
				{
				return _mapper.Map<List<CaregiverCardDTO>>(caregivers);
				

				} return null;

		
		}


	}
}
