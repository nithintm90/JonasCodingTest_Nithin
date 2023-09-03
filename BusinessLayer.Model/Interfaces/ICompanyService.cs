using BusinessLayer.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Interfaces
{
    public interface ICompanyService
    {
        IEnumerable<CompanyInfo> GetAllCompanies();
        CompanyInfo GetCompanyByCode(string companyCode);
        bool SaveCompany(CompanyInfo companyInfo);
        bool UpdateCompany(CompanyInfo companyInfo, string companyCode);
        bool DeleteCompany(string companyCode);

        Task<IEnumerable<CompanyInfo>> GetAllCompaniesAsync();
        Task<CompanyInfo> GetCompanyByCodeAsync(string companyCode);
        Task<bool> SaveCompanyAsync(CompanyInfo companyInfo);
        Task<bool> UpdateCompanyAsync(CompanyInfo companyInfo, string companyCode);
        Task<bool> DeleteCompanyAsync(string companyCode);
    }
}
