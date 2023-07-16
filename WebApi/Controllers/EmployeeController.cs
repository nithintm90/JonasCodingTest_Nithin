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
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }
        // GET api/<controller>
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            var items = await _employeeService.GetAllEmployees().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<EmployeeDto>>(items);
        }

        // GET api/<controller>/5
        public async Task<EmployeeDto> Get(string employeeCode)
        {
            var item = await _employeeService.GetEmployeeByCode(employeeCode).ConfigureAwait(false);
            return _mapper.Map<EmployeeDto>(item);
        }

        // POST api/<controller>
        public async Task<bool> Post([FromBody] EmployeeDto employee)
        {
            var employeeInfo = _mapper.Map<EmployeeInfo>(employee);
            return await _employeeService.SaveEmployee(employeeInfo).ConfigureAwait(false);
        }

        // PUT api/<controller>/5
        public async Task<bool> Put(string employeeCode, [FromBody] EmployeeDto employee)
        {
            if (employeeCode != employee.EmployeeCode)
            {
                throw new Exception("Employee code does not match");
            }
            var employeeInfo = _mapper.Map<EmployeeInfo>(employee);
            return await _employeeService.SaveEmployee(employeeInfo).ConfigureAwait(false);
        }

        // DELETE api/<controller>/5
        public async Task<bool> Delete(string employeeCode)
        {
            return await _employeeService.DeleteEmployee(employeeCode).ConfigureAwait(false);
        }
    }
}
