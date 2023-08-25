using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
	public enum CompanySaveResult
	{
		Success,
        MissingCode,
        InvalidValue,
        DuplicateKey,
        CannotChangeCode
	}

	public interface ICompanyService
    {
	    Task<IEnumerable<CompanyInfo>> GetAllAsync();
	    Task<CompanyInfo> GetByCodeAsync(string companyCode);
	    Task<CompanySaveResult> SaveAsync(CompanyInfo companyInfo);
	    Task<CompanySaveResult> SaveAsync(CompanyInfo companyInfo, CompanyInfo existing);
	    Task<bool> DeleteAsync(string companyCode);
    }
}
