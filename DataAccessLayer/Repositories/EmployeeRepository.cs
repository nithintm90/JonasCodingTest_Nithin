using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            public async Task<IEnumerable<Employee>> GetAll()
            {
                return await _employeeDbWrapper.FindAllAsync();
            }

            public async Task<Employee> GetByCode(string employeeCode)
            {
                var employee = await _employeeDbWrapper.FindAsync(t => t.EmployeeCode.Equals(employeeCode));
                return employee.FirstOrDefault();
            }

            public async Task<bool> DeleteEmployee(string employeeCode)
            {
                return await _employeeDbWrapper.DeleteAsync(C => C.EmployeeCode == employeeCode);
            }



            public async Task<bool> SaveEmployee(Employee employee)
            {
                var itemRepo = _employeeDbWrapper.Find(t =>
                    t.SiteId.Equals(employee.SiteId) && t.CompanyCode.Equals(employee.EmployeeCode))?.FirstOrDefault();
                if (itemRepo != null)
                {
                    itemRepo.EmployeeCode = employee.EmployeeCode;
                    itemRepo.EmployeeName = employee.EmployeeName;
                    itemRepo.EmailAddress = employee.EmailAddress;
                    itemRepo.Phone = employee.Phone;
                    itemRepo.Occupation= employee.Occupation;
                    itemRepo.CompanyCode = employee.CompanyCode;
                    itemRepo.SiteId = employee.SiteId;
                   
                    itemRepo.EmployeeStatus = employee.EmployeeStatus;
                    itemRepo.LastModified = employee.LastModified;
                    return await _employeeDbWrapper.UpdateAsync(itemRepo);
                }

                return await _employeeDbWrapper.InsertAsync(employee);
            }


        
    }
}
