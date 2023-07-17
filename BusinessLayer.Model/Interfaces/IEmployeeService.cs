using BusinessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeInfo>> GetAllEmployees();
        Task<EmployeeInfo> GetEmployeeByCode(string employeeCode);
        Task<bool> SaveEmployee(EmployeeInfo employee);
        Task<bool> DeleteEmployee(string employeeCode);
    }
}
