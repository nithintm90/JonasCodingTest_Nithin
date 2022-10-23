using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
	    private readonly IDbWrapper<Company> _companyDbWrapper;

	    public CompanyRepository(IDbWrapper<Company> companyDbWrapper)
	    {
		    _companyDbWrapper = companyDbWrapper;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _companyDbWrapper.FindAllAsync();
        }

        public async Task<Company> GetByCodeAsync(string siteId, string companyCode)
        {
            if (siteId.Trim() == "")
            {
                throw new BusinessException(501, "Site cannot be empty.");
            }
            else if (companyCode.Trim() == "")
            {
                throw new BusinessException(501, "Company cannot be empty.");
            }
            var temp = await _companyDbWrapper.FindAsync(t => t.SiteId.Equals(siteId) && t.CompanyCode.Equals(companyCode));
            return (temp)?.FirstOrDefault();
        }

        public async Task<bool> SaveCompanyAsync(Company company)
        {
            if (company.SiteId.Trim() == "")
            {
                throw new BusinessException(501, "Site cannot be empty.");
            }
            else if (company.CompanyCode.Trim() == "")
            {
                throw new BusinessException(501, "Company code cannot be empty.");
            }
            else if (company.CompanyName.Trim() == "")
            {
                throw new BusinessException(501, "Company name cannot be empty.");
            }
            var itemRepo = (await _companyDbWrapper.FindAsync(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode)))?.FirstOrDefault();
            if (itemRepo !=null)
            {
                itemRepo.CompanyName = company.CompanyName;
                itemRepo.AddressLine1 = company.AddressLine1;
                itemRepo.AddressLine2 = company.AddressLine2;
                itemRepo.AddressLine3 = company.AddressLine3;
                itemRepo.Country = company.Country;
                itemRepo.EquipmentCompanyCode = company.EquipmentCompanyCode;
                itemRepo.FaxNumber = company.FaxNumber;
                itemRepo.PhoneNumber = company.PhoneNumber;
                itemRepo.PostalZipCode = company.PostalZipCode;
                itemRepo.LastModified = DateTime.Now;
                return await _companyDbWrapper.UpdateAsync(itemRepo);
            }

            company.LastModified = DateTime.Now;
            return await _companyDbWrapper.InsertAsync(company);
        }

        public async Task<bool> DeleteCompanyAsync(string siteId, string companyCode)
        {
            if (siteId.Trim() == "")
            {
                throw new BusinessException(501, "Site cannot be empty.");
            }
            else if (companyCode.Trim() == "")
            {
                throw new BusinessException(501, "Company cannot be empty.");
            }
            return await _companyDbWrapper.DeleteAsync(t => t.SiteId.Equals(siteId) && t.CompanyCode.Equals(companyCode));
        }
    }
}
