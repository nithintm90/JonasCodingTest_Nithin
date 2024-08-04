using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async  Task<IEnumerable<CompanyInfo>> GetAllCompaniesAsync()
        {
            var res = await _companyRepository.GetAll();
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }

        public  async Task<CompanyInfo> GetCompanyByCodeAsync(string companyCode)
        {
            var result = await _companyRepository.GetByCode(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }

       

      

        public async Task SaveCompanyAsync(CompanyInfo companyInfo)
        {
            var company = _mapper.Map < Company > (companyInfo);
             await _companyRepository.SaveCompany(company);

        }

        public async Task UpdateCompanyAsync(string companyCode, CompanyInfo companyInfo )
        {
            var ExistingCompany = await GetCompanyByCodeAsync(companyCode);
            if (ExistingCompany == null)
            {
                throw new Exception("Company ID doesn't exist");
            }
            else
            {
                var company = _mapper.Map<Company>(companyInfo);
                await _companyRepository.SaveCompany(company);
            }

            
        }

        public async Task DeleteCompanyAsync(string CompanyCode)
        {
            await _companyRepository.DeleteCompany(CompanyCode);
        }

        
    }
}
