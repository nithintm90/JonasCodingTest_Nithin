using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
	public enum EmployeeSaveResult
	{
		Success,
		MissingCode,
		InvalidValue,
		DuplicateKey,
		CannotChangeCode
	}

	public interface IEmployeeService
	{
		Task<IEnumerable<EmployeeInfo>> GetAllAsync();
		Task<EmployeeInfo> GetByCodeAsync(string employeeCode);
		Task<EmployeeSaveResult> SaveAsync(EmployeeInfo employeeInfo);
		Task<EmployeeSaveResult> SaveAsync(EmployeeInfo employeeInfo, EmployeeInfo existing);
		Task<bool> DeleteAsync(string employeeCode);
	}
}