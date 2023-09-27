using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;

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
            CreateMap<DataEntity, BaseInfo>();
            CreateMap<Company, CompanyInfo>();
            CreateMap<CreateUpdateCompanyRequest, Company>()
                .ForMember(q => q.Code, w => w.MapFrom(q => q.CompanyCode));
            CreateMap<Employee, EmployeeInfo>();
            CreateMap<CreateUpdateEmployeeRequest, Employee>()
                .ForMember(q => q.Code, w => w.MapFrom(q => q.EmployeeCode));
            CreateMap<ArSubledger, ArSubledgerInfo>();
        }
    }

}