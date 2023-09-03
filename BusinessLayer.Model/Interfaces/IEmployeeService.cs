using BusinessLayer.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync();
        Task<EmployeeInfo> GetEmployeeByCodeAsync(string employeeCode);
        Task<bool> SaveEmployeeAsync(EmployeeInfo employeeInfo);
        Task<bool> UpdateEmployeeAsync(EmployeeInfo employeeInfo, string employeeCode);
        Task<bool> DeleteEmployeeAsync(string employeeCode);
    }
}
