using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;
using System;
using BusinessLayer.Exceptions;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public EmployeeService(
            IEmployeeRepository employeeRepository, 
            ICompanyRepository companyRepository, 
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task CreateEmployeeAsync(CreateUpdateEmployeeRequest req)
        {
            if(req is null)
            {
                throw new ArgumentNullException(nameof(req));
            }

            if(await _employeeRepository.Exists(req.EmployeeCode))
            {
                throw new EntityConflictException($"Cannot create a Employee. An Employee with a Employee Code '{req.EmployeeCode}' already exists.");
            }

            if (!await _companyRepository.Exists(req.CompanyCode))
            {
                throw new EntityNotFoundException($"Cannot create a Employee. A company with a Company Code '{req.CompanyCode}' does not exists.");
            }

            Employee employee = _mapper.Map<Employee>(req);
            employee.Company = await _companyRepository.GetByCodeAsync(req.CompanyCode);
            await _employeeRepository.Create(employee);
        }

        public async Task DeleteEmployeeAsync(string code)
        {
            if (code is null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            if (!await _employeeRepository.Exists(code))
            {
                throw new EntityNotFoundException($"Cannot delete a Employee. An employee with a Employee Code '{code}' does not exist.");
            }

            await _employeeRepository.DeleteAsync(code);
        }

        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync()
        {
            IEnumerable<Employee> res = await _employeeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
        }

        public async Task<EmployeeInfo> GetEmployeeByCodeAsync(string companyCode)
        {
            if (companyCode is null)
            {
                throw new ArgumentNullException(nameof(companyCode));
            }

            var result = await _employeeRepository.GetByCodeAsync(companyCode);
            return _mapper.Map<EmployeeInfo>(result);
        }

        public async Task UpdateEmployeeAsync(CreateUpdateEmployeeRequest req, string code)
        {
            if (req is null)
            {
                throw new ArgumentNullException(nameof(req));
            }

            if (code is null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            if (!await _employeeRepository.Exists(code))
            {
                throw new EntityNotFoundException($"Cannot update a Employee. An employee with an Employee Code '{code}' does not exist.");
            }

            if (!await _companyRepository.Exists(req.CompanyCode))
            {
                throw new EntityNotFoundException($"Cannot update a Employee. A company with a Company Code '{req.CompanyCode}' does not exists.");
            }

            Employee employee = _mapper.Map<Employee>(req);
            await _employeeRepository.Update(employee);
        }
    }
}
