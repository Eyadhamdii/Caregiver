using Caregiver.Dtos;
using Caregiver.Models;
using AutoMapper;


namespace Caregiver.Configurations
{
	public class MappingConfiguration : Profile
	{

		public MappingConfiguration()
		{
			CreateMap<RegisterCustomerDTO, Patient>().ReverseMap();


		}
	}
}
