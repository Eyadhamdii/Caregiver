using AutoMapper;
using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Caregiver.Services.IService;
using Microsoft.AspNetCore.Identity;

namespace Caregiver.Services.Service
{
	public class CustomerServices : ICustomerService
	{
		private readonly IGenericRepo<PatientUser> _customerGenericRepo;
        private readonly IGenericRepo<Dependant> _DependantGenericRepo;
        private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private UserManager<User> _userManager;



		public CustomerServices(IGenericRepo<PatientUser> genericRepo, IMapper mapper, IGenericRepo<Dependant> DependantGenericRepo, IHttpContextAccessor httpContextAccessor , UserManager<User> userManager)
		{
			_customerGenericRepo = genericRepo;
			_mapper = mapper;
			_DependantGenericRepo = DependantGenericRepo;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
		}
		public async Task<IEnumerable<GetCustomerDTO>> GetAllCurrentCustomer()
		{
			//services
			IEnumerable<PatientUser> patients = await _customerGenericRepo.GetAllAsync(a => a.IsDeleted == false);
			//services
			if (patients != null)
			{
				return _mapper.Map<List<GetCustomerDTO>>(patients);


			}
			return null;
		}


		public async Task<PatientUser> GetCustomerById(string id)
		{
			PatientUser patient = await _customerGenericRepo.GetAsync(a => a.Id == id);
			if (patient == null)
			{
				return null;
			}

			return patient;
		}

        public async Task<DependantDetailsDTO> GetDependantDetails()
        {
            //services
            //string loggedInUserId = "2d7b16c3-2090-43c2-862e-b89feb588d47";
			string loggedInUserId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
			Dependant patients = await _DependantGenericRepo.GetAsync(a => a.PatientId == loggedInUserId);
			
            //services
            if (patients != null)
            {
                return _mapper.Map<DependantDetailsDTO>(patients);


            }
            return null;
        }

        public async Task<bool> SoftDeleteCustomer(string id)
		{
			PatientUser patient = await _customerGenericRepo.GetAsync(a => a.Id == id);
			if (patient == null)
			{
				return false;
			}
			var result = await _customerGenericRepo.SoftDeleteUser(patient);
			if (result == true) return true;
			return false;
		}

        public async Task<GetCustomerDTO> UpdateCustomerAsync(string id, GetCustomerDTO CustomerUpdate)
        {
            PatientUser CustomerToUpdate = await _customerGenericRepo.GetAsync(a => a.Id == id);
            if (CustomerToUpdate == null) return null;

            //It is used for updating the properties of an existing CaregiverUser object (caregiverToUpdate) with the values provided in the caregiverUpdate DTO.
            _mapper.Map(CustomerUpdate, CustomerToUpdate);

            var result = await _customerGenericRepo.UpdateUserAsync(CustomerToUpdate);
            if (result == null) return null;

            return _mapper.Map<GetCustomerDTO>(_mapper.Map(CustomerUpdate, CustomerToUpdate));
        }
    }
}
