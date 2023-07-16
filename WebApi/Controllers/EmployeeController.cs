using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeService employeeService, IMapper mapper, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _logger = logger;
        }
        // GET api/<controller>
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            _logger.LogInformation("GetAllEmployees called");
            var items = await _employeeService.GetAllEmployees().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<EmployeeDto>>(items);
        }

        // GET api/<controller>/5
        public async Task<EmployeeDto> Get(string employeeCode)
        {
            _logger.LogInformation($"GetEmployeeByCode called with employeeCode {employeeCode}");
            var item = await _employeeService.GetEmployeeByCode(employeeCode).ConfigureAwait(false);
            return _mapper.Map<EmployeeDto>(item);
        }

        // POST api/<controller>
        public async Task<bool> Post([FromBody] EmployeeDto employee)
        {
            _logger.LogInformation($"Post called with employee body {JsonConvert.SerializeObject(employee)}");
            var employeeInfo = _mapper.Map<EmployeeInfo>(employee);
            return await _employeeService.SaveEmployee(employeeInfo).ConfigureAwait(false);
        }

        // PUT api/<controller>/5
        public async Task<bool> Put(string employeeCode, [FromBody] EmployeeDto employee)
        {
            _logger.LogInformation($"PUT called with employee body {JsonConvert.SerializeObject(employee)}");

            if (employeeCode != employee.EmployeeCode)
            {
                _logger.LogCritical($"Employee code does not match: {employeeCode}");
                throw new Exception("Employee code does not match");
            }
            var employeeInfo = _mapper.Map<EmployeeInfo>(employee);
            return await _employeeService.SaveEmployee(employeeInfo).ConfigureAwait(false);
        }

        // DELETE api/<controller>/5
        public async Task<bool> Delete(string employeeCode)
        {
            _logger.LogInformation($"Delete called with employeeCode {employeeCode}");
            return await _employeeService.DeleteEmployee(employeeCode).ConfigureAwait(false);
        }
    }
}
