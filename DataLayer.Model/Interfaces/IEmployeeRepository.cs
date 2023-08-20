using DataAccessLayer.Model.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll(CancellationToken cancellationToken);
        Task<Employee> GetByCode(string employeeCode, CancellationToken cancellationToken);
        Task<bool> SaveEmployee(Employee employee, CancellationToken cancellationToken);
        Task<bool> DeleteEmployee(string employeeCode, CancellationToken cancellationToken);
    }
}
