using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbWrapper<Employee> _employeeDbWrapper;
        private readonly IDbWrapper<Company> _companyDbWrapper;

        public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper, IDbWrapper<Company> companyDbWrapper)
	    {
		    _employeeDbWrapper = employeeDbWrapper;
            _companyDbWrapper = companyDbWrapper;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeDbWrapper.FindAllAsync();
        }

        public async Task<Employee> GetByCodeAsync(string siteId, string companyCode, string employeeCode)
        {
            if (siteId.Trim() == "")
            {
                throw new BusinessException(501, "Site cannot be empty.");
            }
            else if (companyCode.Trim() == "")
            {
                throw new BusinessException(501, "Company cannot be empty.");
            }
            else if (employeeCode.Trim() == "")
            {
                throw new BusinessException(501, "Employee Name cannot be empty.");
            }
            return (await _employeeDbWrapper.FindAsync(t => t.SiteId.Equals(siteId) && t.CompanyCode.Equals(companyCode) && t.EmployeeCode.Equals(employeeCode)))?.FirstOrDefault();
        }

        public async Task<bool> SaveEmployeeAsync(Employee employee)
        {
            if (employee.SiteId.Trim() == "")
            {
                throw new BusinessException(501, "Site cannot be empty.");
            }
            else if (employee.CompanyCode.Trim() == "")
            {
                throw new BusinessException(501, "Company cannot be empty.");
            }
            else if (employee.EmployeeCode.Trim() == "")
            {
                throw new BusinessException(501, "Employee code cannot be empty.");
            }
            else if (employee.EmployeeName.Trim() == "")
            {
                throw new BusinessException(501, "Employee Name cannot be empty.");
            }
            var companyRepo = (await _companyDbWrapper.FindAsync(t =>
                t.SiteId.Equals(employee.SiteId) && t.CompanyCode.Equals(employee.CompanyCode)))?.FirstOrDefault();
            if(companyRepo == null)
            {
                throw new BusinessException(501, "Company does not exist.");
            }
            var itemRepo = (await _employeeDbWrapper.FindAsync(t =>
                t.SiteId.Equals(employee.SiteId) && t.CompanyCode.Equals(employee.CompanyCode) && t.EmployeeCode.Equals(employee.EmployeeCode)))?.FirstOrDefault();
            if (itemRepo !=null)
            {
                itemRepo.EmployeeName = employee.EmployeeName;
                itemRepo.Occupation = employee.Occupation;
                itemRepo.EmployeeStatus = employee.EmployeeStatus;
                itemRepo.EmailAddress = employee.EmailAddress;
                itemRepo.Phone = employee.Phone;
                itemRepo.LastModified = DateTime.Now;
                return await _employeeDbWrapper.UpdateAsync(itemRepo);
            }
            employee.LastModified = DateTime.Now;
            return await _employeeDbWrapper.InsertAsync(employee);
        }

        public async Task<bool> DeleteEmployeeAsync(string siteId, string companyCode, string employeeCode)
        {
            if (siteId.Trim() == "")
            {
                throw new BusinessException(501, "Site cannot be empty.");
            }
            else if (companyCode.Trim() == "")
            {
                throw new BusinessException(501, "Company cannot be empty.");
            }
            else if (employeeCode.Trim() == "")
            {
                throw new BusinessException(501, "Employee Name cannot be empty.");
            }
            return await _employeeDbWrapper.DeleteAsync(t => t.SiteId.Equals(siteId) && t.CompanyCode.Equals(companyCode) && t.EmployeeCode.Equals(employeeCode));
        }
    }
}
