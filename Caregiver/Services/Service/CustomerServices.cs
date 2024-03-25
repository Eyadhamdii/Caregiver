using AutoMapper;
using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Caregiver.Services.IService;

namespace Caregiver.Services.Service
{
	public class CustomerServices : ICustomerService
	{
		private readonly IGenericRepo<PatientUser> _customerGenericRepo;
		private readonly IMapper _mapper;

		public CustomerServices(IGenericRepo<PatientUser> genericRepo, IMapper mapper)
		{
			_customerGenericRepo = genericRepo;
			_mapper = mapper;
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
	}
}
