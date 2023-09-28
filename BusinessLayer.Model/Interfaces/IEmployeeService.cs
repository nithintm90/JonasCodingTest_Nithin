using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync();
        Task<EmployeeInfo> GetEmployeeByCodeAsync(string code);
        Task CreateEmployeeAsync(CreateUpdateEmployeeRequest req);
        Task UpdateEmployeeAsync(CreateUpdateEmployeeRequest req, string code);
        Task DeleteEmployeeAsync(string code);
    }
}
