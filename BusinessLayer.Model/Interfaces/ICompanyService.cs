using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
	public enum SaveResult
	{
		Success,
        MissingCode,
        InvalidValue,
        DuplicateKey,
        CannotChangeCode
	}

	public interface ICompanyService
    {
	    Task<IEnumerable<CompanyInfo>> GetAllCompaniesAsync();
	    Task<CompanyInfo> GetCompanyByCodeAsync(string companyCode);
	    Task<SaveResult> SaveAsync(CompanyInfo companyInfo);
	    Task<SaveResult> SaveAsync(CompanyInfo companyInfo, CompanyInfo existing);
	    Task<bool> DeleteAsync(string companyCode);
    }
}
