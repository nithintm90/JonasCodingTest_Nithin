using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly IDbWrapper<Employee> _employeeDbWrapper;

        public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper)
        {
            _employeeDbWrapper = employeeDbWrapper;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _employeeDbWrapper.FindAllAsync().ConfigureAwait(false);
        }

        public async Task<Employee> GetByCode(string employeeCode)
        {
            return (await _employeeDbWrapper.FindAsync(t => t.EmployeeCode.Equals(employeeCode)).ConfigureAwait(false))?.FirstOrDefault();
        }

        public async Task<bool> SaveEmployee(Employee employee)
        {
            var itemRepo = (await _employeeDbWrapper.FindAsync(t =>
                t.EmployeeCode.Equals(employee.EmployeeCode)).ConfigureAwait(false))?.FirstOrDefault();

            if (itemRepo != null)
            {
                itemRepo.EmployeeName = employee.EmployeeName;
                itemRepo.Occupation = employee.Occupation;
                itemRepo.EmployeeStatus = employee.EmployeeStatus;
                itemRepo.EmailAddress = employee.EmailAddress;
                itemRepo.Phone = employee.Phone;
                itemRepo.LastModified = employee.LastModified;
                return await _employeeDbWrapper.UpdateAsync(itemRepo).ConfigureAwait(false);
            }

            return await _employeeDbWrapper.InsertAsync(employee).ConfigureAwait(false);
        }

        public async Task<bool> DeleteEmployee(string employeeCode)
        {
            return await _employeeDbWrapper.DeleteAsync(c => c.EmployeeCode.Equals(employeeCode)).ConfigureAwait(false);
        }
    }
}
