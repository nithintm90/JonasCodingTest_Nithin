using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
	    private readonly IDbWrapper<Company> _companyDbWrapper;
        private readonly ILogger<CompanyRepository> _logger;
	    public CompanyRepository(IDbWrapper<Company> companyDbWrapper, ILogger<CompanyRepository> logger)
        {
            _companyDbWrapper = companyDbWrapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await _companyDbWrapper.FindAllAsync().ConfigureAwait(false);
        }

        public async Task<Company> GetByCode(string companyCode)
        {
            return (await _companyDbWrapper.FindAsync(t => t.CompanyCode.Equals(companyCode)).ConfigureAwait(false))?.FirstOrDefault();
        }

        public async Task<bool> SaveCompany(Company company)
        {
            var itemRepo = (await _companyDbWrapper.FindAsync(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode)).ConfigureAwait(false))?.FirstOrDefault();
            if (itemRepo != null)
            {
                _logger.LogInformation($"Updating as company with company code: {company} exists");
                itemRepo.CompanyName = company.CompanyName;
                itemRepo.AddressLine1 = company.AddressLine1;
                itemRepo.AddressLine2 = company.AddressLine2;
                itemRepo.AddressLine3 = company.AddressLine3;
                itemRepo.Country = company.Country;
                itemRepo.EquipmentCompanyCode = company.EquipmentCompanyCode;
                itemRepo.FaxNumber = company.FaxNumber;
                itemRepo.PhoneNumber = company.PhoneNumber;
                itemRepo.PostalZipCode = company.PostalZipCode;
                itemRepo.LastModified = company.LastModified;
                return await _companyDbWrapper.UpdateAsync(itemRepo).ConfigureAwait(false);
            }

            _logger.LogInformation($"Inserting as company with company code: {company} is new");

            return await _companyDbWrapper.InsertAsync(company).ConfigureAwait(false);
        }

        public async Task<bool> DeleteCompany(string companyCode)
        {
            return await _companyDbWrapper.DeleteAsync(c => c.CompanyCode.Equals(companyCode)).ConfigureAwait(false);
        }
    }
}
