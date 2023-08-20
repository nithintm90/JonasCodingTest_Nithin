using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public Task<IEnumerable<Company>> GetAll(CancellationToken cancellationToken)
        {
            return _companyDbWrapper.FindAllAsync(cancellationToken);
        }

        public Task<Company> GetByCode(string companyCode, CancellationToken cancellationToken)
        {
            return Task.FromResult(_companyDbWrapper.FindAsync(t => t.CompanyCode.Equals(companyCode), cancellationToken).Result.FirstOrDefault());
        }

        public Task<bool> SaveCompany(Company company, CancellationToken cancellationToken)
        {
            var itemRepo = Task.WhenAll(_companyDbWrapper.FindAsync(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode), cancellationToken)).Result?.FirstOrDefault()?.FirstOrDefault();
            
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
                itemRepo.LastModified = company.LastModified;
                return _companyDbWrapper.UpdateAsync(itemRepo, cancellationToken);
            }

            return _companyDbWrapper.InsertAsync(company, cancellationToken);
        }

        public Task<bool> DeleteCompany(string companyCode, CancellationToken cancellationToken)
        {

            return _companyDbWrapper.DeleteAsync(x => x.CompanyCode.Equals(companyCode), cancellationToken);
        }
    }
}
