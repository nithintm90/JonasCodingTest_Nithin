using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyInfo>> GetAllCompaniesAsync();
        Task<CompanyInfo> GetCompanyByCodeAsync(string code);
        Task CreateCompanyAsync(CreateUpdateCompanyRequest req);
        Task UpdateCompanyAsync(CreateUpdateCompanyRequest req, string code);
        Task DeleteCompanyAsync(string code);
    }
}
