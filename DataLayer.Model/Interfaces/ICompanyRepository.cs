using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> GetByCodeAsync(string siteId, string companyCode);
        Task<bool> SaveCompanyAsync(Company company);
        Task<bool> DeleteCompanyAsync(string siteId, string companyCode);
    }
}
