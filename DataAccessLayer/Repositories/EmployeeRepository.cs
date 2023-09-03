using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbWrapper<Employee> _employeeDbWrapper;

        public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper)
        {
            _employeeDbWrapper = employeeDbWrapper;
        }

        public async Task<bool> DeleteByCodeAsync(string employeeCode)
        {
            return await _employeeDbWrapper.DeleteAsync(t => t.EmployeeCode.Equals(employeeCode));
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeDbWrapper.FindAllAsync();
        }

        public async Task<Employee> GetByCodeAsync(string employeeCode)
        {
            return (await _employeeDbWrapper.FindAsync(t => t.EmployeeCode.Equals(employeeCode)))?.FirstOrDefault();
        }

        public async Task<bool> SaveEmployeeAsync(Employee employee)
        {
            var existingEmployee = _employeeDbWrapper.Find(t => t.SiteId.Equals(employee.SiteId)
                                                                && t.EmployeeCode.Equals(employee.EmployeeCode))?.FirstOrDefault();
            if (existingEmployee == null)
                return await _employeeDbWrapper.InsertAsync(employee);

            existingEmployee.EmployeeName = employee.EmployeeName;
            existingEmployee.Occupation = employee.Occupation;
            existingEmployee.EmployeeStatus = employee.EmployeeStatus;
            existingEmployee.EmailAddress = employee.EmailAddress;
            existingEmployee.Phone = employee.Phone;
            existingEmployee.LastModified = employee.LastModified;

            return await _employeeDbWrapper.UpdateAsync(existingEmployee);
        }
    }
}