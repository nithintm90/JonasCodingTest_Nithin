using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;
using System;

namespace BusinessLayer
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMapper();
        }

        private void CreateMapper()
        {
            CreateMap<string, DateTime>().ConvertUsing<StringToDateTimeConverter>();
            CreateMap<DateTime, string>().ConvertUsing<DateTimeToStringConverter>();

            CreateMap<DataEntity, BaseInfo>();
            CreateMap<Company, CompanyInfo>();

            CreateMap<Employee, EmployeeInfo>()
                .ForMember(dest =>
                    dest.OccupationName,
                    opt => opt.MapFrom(src => src.Occupation))
                .ForMember(dest =>
                    dest.PhoneNumber,
                    opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest =>
                    dest.LastModifiedDateTime,
                    opt => opt.MapFrom(src => src.LastModified));

            CreateMap<EmployeeInfo, Employee>()
                .ForMember(dest =>
                dest.Occupation,
                opt => opt.MapFrom(src => src.OccupationName))
                .ForMember(dest =>
                dest.Phone,
                opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest =>
                dest.LastModified,
                opt => opt.MapFrom(src => src.LastModifiedDateTime));

            CreateMap<ArSubledger, ArSubledgerInfo>();
        }
    }
    public class StringToDateTimeConverter : ITypeConverter<string, DateTime>
    {
        public DateTime Convert(string source, DateTime destination, ResolutionContext context)
        {
            if (DateTime.TryParse(source, out var dateTime))
                return dateTime;

            return default;
        }
    }
    public class DateTimeToStringConverter : ITypeConverter<DateTime, string>
    {
        public string Convert(DateTime source, string destination, ResolutionContext context)
        {
            return source.ToString();
        }
    }

}