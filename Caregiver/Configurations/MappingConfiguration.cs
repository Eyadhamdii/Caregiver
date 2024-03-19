using Caregiver.Dtos;
using Caregiver.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Caregiver.Dtos.UpdateDTOs;


namespace Caregiver.Configurations
{
	public class MappingConfiguration : Profile
	{

		public MappingConfiguration()
		{
			CreateMap<RegisterPatientDTO, PatientUser>().ReverseMap();
			CreateMap<RegisterCaregiverDTO, CaregiverUser>().ReverseMap();
			CreateMap<CaregiverUser, CaregiverCardDTO>().ReverseMap();
			CreateMap<CaregiverUser, CaregiverUpdateDTO>().ReverseMap();

		}

	}
}
