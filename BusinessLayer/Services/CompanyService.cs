using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;

namespace BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public IEnumerable<CompanyInfo> GetAllCompanies()
        {
            var res = _companyRepository.GetAll();
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }

        public CompanyInfo GetCompanyByCode(string companyCode)
        {
            var result = _companyRepository.GetByCode(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }
        public bool SaveCompanyDetails(CompanyInfo companyInfo)
        {
          var result = _companyRespository.SaveCompany(_mapper.Map<Company>(companyInfo))
          return result;
        }
        public bool UpdateCompanyDetails(string companyCode, CompanyInfo companyInfo)
        {
          companyInfo.CompanyCode = companyCode;
          var result = _companyRespository.SaveCompany(_mapper.Map<Company>(companyInfo))
          return result;
        }
        public bool DeleteCompanyDetails(string companyCode)
        {
            var result = _companyRepository.DeleteByCode(companyCode);
            return result;
        }
        
        
    }
}
