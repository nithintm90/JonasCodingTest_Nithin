using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Model.Interfaces;

namespace BusinessLayer.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployees()
        {
            var res = await _employeeRepository.GetAll().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
        }

        public async Task<EmployeeInfo> GetEmployeeByCode(string employeeCode)
        {
            var result = await _employeeRepository.GetByCode(employeeCode).ConfigureAwait(false);
            return _mapper.Map<EmployeeInfo>(result);
        }

        public async Task<bool> SaveEmployee(EmployeeInfo employee)
        {
            var result = _mapper.Map<Employee>(employee);
            return await _employeeRepository.SaveEmployee(result).ConfigureAwait(false);
        }

        public async Task<bool> DeleteEmployee(string employeeCode)
        {
            return await _employeeRepository.DeleteEmployee(employeeCode).ConfigureAwait(false);
        }
    }
}
