using System;
using System.Globalization;
using AutoMapper;
using BusinessLayer.Model.Models;
using WebApi.Models;

namespace WebApi
{
    public class AppServicesProfile : Profile
    {
        public AppServicesProfile()
        {
            CreateMapper();
        }

        private void CreateMapper()
        {
            CreateMap<BaseInfo, BaseDto>();
            CreateMap<CompanyInfo, CompanyDto>();
            CreateMap<EmployeeInfo, EmployeeDto>()
	            .ForMember(dest => dest.OccupationName, opt => opt.MapFrom(src => src.Occupation))
	            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
	            .ForMember(dest => dest.LastModifiedDateTime, opt => opt.ResolveUsing(e => e.LastModified.ToString(CultureInfo.InvariantCulture)));
            CreateMap<EmployeeDto, EmployeeInfo>()
	            .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.OccupationName))
	            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
	            .ForMember(dest => dest.LastModified, opt => opt.ResolveUsing(e => DateTime.TryParse(e.LastModifiedDateTime, out var parsed) ? parsed : DateTime.MinValue));
			CreateMap<ArSubledgerInfo, ArSubledgerDto>();
        }
    }
}