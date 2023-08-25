using System.Collections.Generic;
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
        IEnumerable<CompanyInfo> GetAllCompanies();
        CompanyInfo GetCompanyByCode(string companyCode);
        SaveResult Save(CompanyInfo companyInfo);
        SaveResult Save(CompanyInfo companyInfo, CompanyInfo existing);
		bool Delete(string companyCode);
    }
}
