using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.Linq;
using System.Threading;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(ICompanyRepository companyRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteEmployeeAsync(string employeeCode)
        {
            return await _employeeRepository.DeleteByCodeAsync(employeeCode);
        }

        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync()
        {
            var res = await _employeeRepository.GetAllAsync();
            var employeeList = _mapper.Map<IEnumerable<EmployeeInfo>>(res);

            //To set company name for each employee
            var companies = (await _companyRepository.GetAllAsync()).ToList();
            if (companies?.Any() == true && employeeList?.Any() == true)
                foreach (var employee in employeeList)
                    employee.CompanyName = companies.FirstOrDefault(a => a.CompanyCode.Equals(employee.CompanyCode, System.StringComparison.InvariantCultureIgnoreCase))
                        ?.CompanyName;

            return employeeList;
        }

        public async Task<EmployeeInfo> GetEmployeeByCodeAsync(string employeeCode)
        {
            var result = await _employeeRepository.GetByCodeAsync(employeeCode);
            var employee = _mapper.Map<EmployeeInfo>(result);

            //To set company name
            if (employee != null)
                employee.CompanyName = (await _companyRepository.GetByCodeAsync(employee.CompanyCode))?.CompanyName;

            return employee;
        }

        public async Task<bool> SaveEmployeeAsync(EmployeeInfo employeeInfo)
        {
            var employee = _mapper.Map<Employee>(employeeInfo);
            return await _employeeRepository.SaveEmployeeAsync(employee);
        }

        public async Task<bool> UpdateEmployeeAsync(EmployeeInfo employeeInfo, string employeeCode)
        {
            var employee = _mapper.Map<Employee>(employeeInfo);
            employee.EmployeeCode = employeeCode;
            return await _employeeRepository.SaveEmployeeAsync(employee);
        }
    }
}