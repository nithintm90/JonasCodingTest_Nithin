using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.Threading.Tasks;
using System.Threading;

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
        public async Task<IEnumerable<CompanyInfo>> GetAllCompanies(CancellationToken cancellationToken)
        {
            var res = await _companyRepository.GetAll(cancellationToken);
            return  _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }

        public async Task<CompanyInfo> GetCompanyByCode(string companyCode, CancellationToken cancellationToken)
        {
            var result = await _companyRepository.GetByCode(companyCode, cancellationToken);
            return _mapper.Map<CompanyInfo>(result);
        }

        public Task<bool> SaveCompany(CompanyInfo companyInfo, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Company>(companyInfo);
            var result = _companyRepository.SaveCompany(item, cancellationToken);
            return result;
        }

        public Task<bool> DeleteCompany(string companyCode, CancellationToken cancellationToken)
        {
            var result = _companyRepository.DeleteCompany(companyCode, cancellationToken);
            return result;
        }
    }
}
