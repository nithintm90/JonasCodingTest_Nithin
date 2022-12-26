using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployeeAsync()
        {
            var res = await _employeeRepository.GetAllAsync();
           
            var employees =  _mapper.Map<IEnumerable<EmployeeInfo>>(res);


            foreach (var employee in employees)
            {
                employee.CompanyName = (await _companyRepository.GetByCodeAsync(employee.CompanyCode)).CompanyName;
            }

         
            return employees;
        }

        public async Task<bool> SaveEmployeeAsync(EmployeeInfo employeeInfo)
        {
            var employee = _mapper.Map<Employee>(employeeInfo);
            return await _employeeRepository.SaveEmployeeAsync(employee);
        }
    }
}
