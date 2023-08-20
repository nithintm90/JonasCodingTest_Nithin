using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyInfo>> GetAllCompanies(CancellationToken cancellationToken);
        Task<CompanyInfo> GetCompanyByCode(string companyCode, CancellationToken cancellationToken);
        Task<bool> SaveCompany(CompanyInfo companyInfo, CancellationToken cancellationToken);
        Task<bool> DeleteCompany(string companyCode, CancellationToken  cancellationToken);
    }
}
