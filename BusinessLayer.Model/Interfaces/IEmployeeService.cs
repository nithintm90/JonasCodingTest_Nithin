using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeInfo>> GetAllEmployeeAsync();
        Task<EmployeeInfo> GetEmployeeByCodeAsync(string siteId, string companyCode, string employeeCode);
        Task<bool> SaveEmployeeAsync(EmployeeInfo employee);
        Task<bool> DeleteEmployeeAsync(string siteId, string companyCode, string employeeCode);
    }
}
