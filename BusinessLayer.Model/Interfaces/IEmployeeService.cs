using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeInfo>> GetAllEmployees(CancellationToken cancellationToken);
        Task<EmployeeInfo> GetEmployeeByCode(string employeeCode, CancellationToken cancellationToken);
        Task<bool> SaveEmployee(EmployeeInfo employeeInfo, CancellationToken cancellationToken);
        Task<bool> DeleteEmployee(string employeeCode, CancellationToken  cancellationToken);
    }
}
