using DataAccessLayer.Model.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAll(CancellationToken cancellationToken);
        Task<Company> GetByCode(string companyCode, CancellationToken cancellationToken);
        Task<bool> SaveCompany(Company company, CancellationToken cancellationToken);
        Task<bool> DeleteCompany(string companyCode, CancellationToken cancellationToken);
    }
}
