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

		public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper)
		{
			_employeeDbWrapper = employeeDbWrapper;
		}

		public async Task<IEnumerable<Employee>> GetAllAsync()
		{
			return await _employeeDbWrapper.FindAllAsync();
		}

		public async Task<Employee> GetByCodeAsync(string employeeCode)
		{
			var possible = await _employeeDbWrapper.FindAsync(t => t.EmployeeCode.Equals(employeeCode));
			return possible.FirstOrDefault();
		}

		public async Task<bool> SaveAsync(Employee employee)
		{
			var possible = await _employeeDbWrapper.FindAsync(t => t.SiteId.Equals(employee.SiteId) && t.EmployeeCode.Equals(employee.EmployeeCode));
			var itemRepo = possible.FirstOrDefault();
			if (itemRepo != null)
			{
				itemRepo.EmployeeName = employee.EmployeeName;
				itemRepo.Occupation = employee.Occupation;
				itemRepo.EmployeeStatus = employee.EmployeeStatus;
				itemRepo.EmailAddress = employee.EmailAddress;
				itemRepo.Phone = employee.Phone;
				itemRepo.LastModified = employee.LastModified = DateTime.Now;
				return await _employeeDbWrapper.UpdateAsync(itemRepo);
			}

			return await _employeeDbWrapper.InsertAsync(employee);
		}

		public async Task<bool> DeleteAsync(string employeeCode)
		{
			return await _employeeDbWrapper.DeleteAsync(t => t.EmployeeCode == employeeCode);
		}
	}
}