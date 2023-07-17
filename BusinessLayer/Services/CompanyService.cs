using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

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
        public async Task<IEnumerable<CompanyInfo>> GetAllCompanies()
        {
            var res = await _companyRepository.GetAll().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }

        public async Task<CompanyInfo> GetCompanyByCode(string companyCode)
        {
            var result = await _companyRepository.GetByCode(companyCode).ConfigureAwait(false);
            return _mapper.Map<CompanyInfo>(result);
        }

        public async Task<bool> SaveCompany(CompanyInfo company) {
            var result = _mapper.Map<Company>(company);
            return await _companyRepository.SaveCompany(result).ConfigureAwait(false);
        }

        public async Task<bool> DeleteCompany(string companyCode)
        {
            return await _companyRepository.DeleteCompany(companyCode).ConfigureAwait(false);
        }
    }
}
