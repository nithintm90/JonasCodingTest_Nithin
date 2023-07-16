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
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployees()
        {
            var res = await _employeeRepository.GetAll().ConfigureAwait(false);
            _logger.LogInformation($"GetAllEmployees called with result {res.Count()}");
            return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
        }

        public async Task<EmployeeInfo> GetEmployeeByCode(string employeeCode)
        {
            var result = await _employeeRepository.GetByCode(employeeCode).ConfigureAwait(false);
            _logger.LogInformation($"GetEmployeeByCode called and has result: {result != null}");
            return _mapper.Map<EmployeeInfo>(result);
        }

        public async Task<bool> SaveEmployee(EmployeeInfo employee)
        {
            try
            {
                var result = _mapper.Map<Employee>(employee);
                return await _employeeRepository.SaveEmployee(result).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in SaveEmployee for Employee with code: {employee?.EmployeeCode}");
                return false;
            }
        }

        public async Task<bool> DeleteEmployee(string employeeCode)
        {
            try
            {
                return await _employeeRepository.DeleteEmployee(employeeCode).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in DeleteEmployee for Employee with code: {employeeCode}");
                return false;
            }
        }
    }
}
