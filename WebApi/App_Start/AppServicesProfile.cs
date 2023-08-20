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
                .ForMember(dest => dest.OccupationName, x => x.MapFrom(y => y.Occupation))
                .ForMember(dest => dest.PhoneNumber, x => x.MapFrom(y => y.Phone))
                .ForMember(dest => dest.LastModifiedDateTime, x => x.MapFrom(y => y.LastModified)
                );
            CreateMap<EmployeeDto, EmployeeInfo>()
                .ForMember(dest => dest.Occupation, x => x.MapFrom(y => y.OccupationName))
                .ForMember(dest => dest.Phone, x => x.MapFrom(y => y.PhoneNumber))
                .ForMember(dest => dest.LastModified, x => x.MapFrom(y => y.LastModifiedDateTime)
                );
            CreateMap<ArSubledgerInfo, ArSubledgerDto>();
        }
    }
}