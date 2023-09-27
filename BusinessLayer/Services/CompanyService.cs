using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;
using System;

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

        public async Task CreateCompanyAsync(CreateUpdateCompanyRequest companyInfo)
        {
            if(companyInfo is null)
            {
                throw new ArgumentNullException(nameof(companyInfo));
            }

            if(await _companyRepository.Exists(companyInfo.CompanyCode))
            {
                throw new Exception($"Cannot create a Company. A company with a Company Code '{companyInfo.CompanyCode}' already exists.");
            }

            Company company = _mapper.Map<Company>(companyInfo);
            await _companyRepository.Create(company);
        }

        public async Task DeleteCompanyAsync(string companyCode)
        {
            if (companyCode is null)
            {
                throw new ArgumentNullException(nameof(companyCode));
            }

            if (!await _companyRepository.Exists(companyCode))
            {
                throw new Exception($"Cannot delete a Company. A company with a Company Code '{companyCode}' does not exist.");
            }

            await _companyRepository.DeleteAsync(companyCode);
        }

        public async Task<IEnumerable<CompanyInfo>> GetAllCompaniesAsync()
        {
            IEnumerable<Company> res = await _companyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }

        public async Task<CompanyInfo> GetCompanyByCodeAsync(string companyCode)
        {
            if (companyCode is null)
            {
                throw new ArgumentNullException(nameof(companyCode));
            }

            var result = await _companyRepository.GetByCodeAsync(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }

        public async Task UpdateCompanyAsync(CreateUpdateCompanyRequest companyInfo, string companyCode)
        {
            if (companyInfo is null)
            {
                throw new ArgumentNullException(nameof(companyInfo));
            }

            if (companyCode is null)
            {
                throw new ArgumentNullException(nameof(companyCode));
            }

            if (!await _companyRepository.Exists(companyCode))
            {
                throw new Exception($"Cannot update a Company. A company with a Company Code '{companyCode}' does not exist.");
            }

            Company company = _mapper.Map<Company>(companyInfo);
            await _companyRepository.Update(company);
        }
    }
}
