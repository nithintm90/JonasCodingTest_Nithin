using System.Collections.Generic;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface ICompanyService
    {
        IEnumerable<CompanyInfo> GetAllCompanies();
        CompanyInfo GetCompanyByCode(string companyCode);
        bool SaveCompanyDetails(CompanyInfo companyInfo);
        bool UpdateCompanyDetails(string companyCode, CompanyInfo companyInfo);
        bool DeleteCompanyDetails(string companyCode);
    }
}
