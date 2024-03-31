using Caregiver.Dtos;
using Caregiver.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Http.HttpResults;
using Caregiver.Migrations;


namespace Caregiver.Configurations
{
    public class MappingConfiguration : Profile
	{

		public MappingConfiguration()
		{
			//CreateMap<RegisterPatientDTO, PatientUser>().ReverseMap();
			CreateMap<RegisterPatientDTO, PatientUser>()
	.ForMember(dest => dest.Age, opt => opt.MapFrom((src, dest) => Helpers.Handlers.CalculateAge(dest.Birthdate)));

			CreateMap<CaregiverDataDTO, CaregiverUser>().ReverseMap();
			CreateMap<GetCustomerDTO, PatientUser>().ReverseMap();
			CreateMap<RegisterCaregiverDTO, CaregiverUser>().ReverseMap();
			CreateMap<CaregiverUser, CaregiverCardDTO>().ReverseMap();
			CreateMap<CaregiverUser, CaregiverUpdateDTO>().ReverseMap();

			CreateMap<CaregiverUser, AdminCaregiverDTO>()
		   .ForMember(dest => dest.TotalCustomers, opt => opt.MapFrom(src => src.Reservations.Count(a => a.Status == "Confirmed")))
		   .ForMember(dest => dest.TotalRevenu, opt => opt.MapFrom(src => src.Reservations.Where(a => a.Status == "Confirmed").Sum(a => a.TotalPrice)))
		   .ForMember(dest => dest.OngoingOrders, opt => opt.MapFrom(src => src.Reservations.Count(a => a.Status == "OnProgress")))
		   .ForMember(dest => dest.CanceledOrders, opt => opt.MapFrom(src => src.Reservations.Count(a => a.Status == "Cancelled")))
		   .ForMember(dest => dest.Status, opt => opt.MapFrom(src => DetermineStatus(src)));





		}


		public string DetermineStatus(CaregiverUser src)
		{
			if (!src.IsFormCompleted && !src.IsDeleted && !src.IsDeletedByAdmin && !src.IsAccepted)
			{
				return "form incomplete";
			}
			else if (src.IsFormCompleted && !src.IsDeleted && !src.IsDeletedByAdmin && !src.IsAccepted)
			{
				return "pending";
			}
			else if (src.IsFormCompleted && !src.IsDeleted && !src.IsDeletedByAdmin && src.IsAccepted)
			{
				return "active";
			}
			else if (src.IsFormCompleted && src.IsDeleted && !src.IsDeletedByAdmin && src.IsAccepted)
			{
				return "not active";
			}
			else if (src.IsDeletedByAdmin)
			{
				return "blocked";
			}
			else
			{
				return "";
			}
		}

	}
}
