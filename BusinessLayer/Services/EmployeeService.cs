using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;
using System.Linq;

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
            var employees = await _employeeRepository.GetAllAsync();
            var companies = (await _companyRepository.GetAllAsync());
            var mappedEmployees = _mapper.Map<IEnumerable<EmployeeInfo>>(employees);
            foreach(var mappedEmployee in mappedEmployees)
            {
                mappedEmployee.CompanyName = companies.FirstOrDefault(x => 
                    x.SiteId.Equals(mappedEmployee.SiteId) && x.CompanyCode.Equals(mappedEmployee.CompanyCode))?.CompanyName;
            }
            return mappedEmployees;
        }

        public async Task<EmployeeInfo> GetEmployeeByCodeAsync(string siteId, string companyCode, string employeeCode)
        {
            var result = await _employeeRepository.GetByCodeAsync(siteId, companyCode, employeeCode);
            var company = await _companyRepository.GetByCodeAsync(siteId, companyCode);
            return result != null? _mapper.Map<Employee, EmployeeInfo>(result, opt => {
                opt.AfterMap((src, dest) => dest.CompanyName = company.CompanyName) ;
            }) : null;
        }

        public async Task<bool> SaveEmployeeAsync(EmployeeInfo employee)
        {
            return await _employeeRepository.SaveEmployeeAsync(_mapper.Map<Employee>(employee));
        }

        public async Task<bool> DeleteEmployeeAsync(string siteId, string companyCode, string employeeCode)
        {
            return await _employeeRepository.DeleteEmployeeAsync(siteId, companyCode, employeeCode);
        }
    }
}
