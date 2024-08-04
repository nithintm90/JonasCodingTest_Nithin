using AutoMapper;
using BusinessLayer.Model.Models;
using System;
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
            CreateMap<ArSubledgerInfo, ArSubledgerDto>();
            CreateMap<EmployeeInfo, EmployeeDto>()
                                               .ForMember(Dto => Dto.PhoneNumber, memberOptions => memberOptions.MapFrom(Info => Info.Phone))
                                              .ForMember(Dto => Dto.OccupationName, memberOptions => memberOptions.MapFrom(Info => Info.Occupation))
                                              .ForMember(Dto => Dto.LastModifiedDateTime, memberOptions => memberOptions.MapFrom(Info => ((DateTime)Info.LastModified).ToShortDateString()))
                                              .ForMember(Dto => Dto.CompanyName, memberOptions => memberOptions.MapFrom(Info => Info.CompanyCode))
                                              .ForMember(Dto => Dto.CompanyCode, memberOptions => memberOptions.Ignore())
                                              .ForMember(Dto => Dto.SiteId, memberOptions => memberOptions.Ignore());
                                              
            CreateMap<EmployeeDto, EmployeeInfo>()
                                    .ForMember(Info => Info.Phone, memberOptions => memberOptions.MapFrom(Dto => Dto.PhoneNumber))
                                   .ForMember(Info => Info.Occupation, memberOptions => memberOptions.MapFrom(Dto => Dto.OccupationName))
                                   .ForMember(Info => Info.LastModified, memberOptions => memberOptions.MapFrom(Dto => (DateTime.Parse(Dto.LastModifiedDateTime) )));
                                
        }
    }
}