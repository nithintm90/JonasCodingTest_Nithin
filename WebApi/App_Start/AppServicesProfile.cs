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
            CreateMap<CompanyInfo, CompanyDto>()
                .ForMember(q => q.CompanyCode, w => w.MapFrom(q => q.Code));
            CreateMap<EmployeeInfo, EmployeeDto>()
                .ForMember(q => q.EmployeeCode, w => w.MapFrom(q => q.Code))
                .ForMember(q => q.PhoneNumber, w => w.MapFrom(q => q.Phone))
                .ForMember(q => q.OccupationName, w => w.MapFrom(q => q.Occupation))
                .ForMember(q => q.CompanyName, w => w.MapFrom(q => q.Company.CompanyName));
            CreateMap<ArSubledgerInfo, ArSubledgerDto>();
        }
    }
}