using System.Collections.Generic;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeInfo> GetAllEmployee();
        EmployeeInfo GetEmployeeByCode(string employeeCode);
        bool SaveEmployeeDetails(EmployeeInfo employeeInfo);
    }
}
