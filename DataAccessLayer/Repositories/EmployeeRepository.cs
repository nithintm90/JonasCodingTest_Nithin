using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
	    private readonly IDbWrapper<Employee> _employeeDbWrapper;

	    public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper)
	    {
		    _employeeDbWrapper = employeeDbWrapper;
        }

        public Task<IEnumerable<Employee>> GetAll(CancellationToken cancellationToken)
        {
            return _employeeDbWrapper.FindAllAsync(cancellationToken);
        }

        public Task<Employee> GetByCode(string employeeCode, CancellationToken cancellationToken)
        {
            return Task.FromResult(_employeeDbWrapper.FindAsync(t => t.EmployeeCode.Equals(employeeCode), cancellationToken).Result.FirstOrDefault());
        }

        public Task<bool> SaveEmployee(Employee employee, CancellationToken cancellationToken)
        {
            var itemRepo = Task.WhenAll(_employeeDbWrapper.FindAsync(t =>
                t.SiteId.Equals(employee.SiteId) && t.EmployeeCode.Equals(employee.EmployeeCode), cancellationToken)).Result?.FirstOrDefault()?.FirstOrDefault();
            
            if (itemRepo !=null)
            {
                itemRepo.EmployeeName = employee.EmployeeName;
                itemRepo.Occupation = employee.Occupation;
                itemRepo.EmployeeStatus = employee.EmployeeStatus;
                itemRepo.EmailAddress = employee.EmailAddress;
                itemRepo.Phone = employee.Phone;
                itemRepo.LastModified = employee.LastModified;
                return _employeeDbWrapper.UpdateAsync(itemRepo, cancellationToken);
            }

            return _employeeDbWrapper.InsertAsync(employee, cancellationToken);
        }

        public Task<bool> DeleteEmployee(string employeeCode, CancellationToken cancellationToken)
        {
            return _employeeDbWrapper.DeleteAsync(x => x.EmployeeCode.Equals(employeeCode), cancellationToken);
        }
    }
}
