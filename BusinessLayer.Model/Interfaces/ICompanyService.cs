using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface ICompanyService
    {
        IEnumerable<CompanyInfo> GetAllCompanies();
        CompanyInfo GetCompanyByCode(string companyCode);

        Task<bool> SaveCompanyAsync(CompanyInfo companyInfo);
        Task<bool> UpdateCompanyAsync(string companyCode,CompanyInfo companyInfo);
        Task<bool> DeleteCompanyAsync(string companyCode);
    }
}
