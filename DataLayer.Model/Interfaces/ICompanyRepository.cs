using DataAccessLayer.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAll();
        Task<Company> GetByCode(string companyCode);
        Task<bool> SaveCompany(Company company);

        Task<bool> DeleteCompany(string companyCode);

       
    }
}
