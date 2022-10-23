using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService companyService, IMapper mapper)
        {
            _employeeService = companyService;
            _mapper = mapper;
        }
        // GET api/<controller>
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            var items = await _employeeService.GetAllEmployeeAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(items);
        }

        // GET api/<controller>?siteId=1&companyCode=abc&employeeCode=123
        public async Task<EmployeeDto> Get(string siteId, string companyCode, string employeeCode)
        {
            var item = await _employeeService.GetEmployeeByCodeAsync(siteId, companyCode, employeeCode);
            return _mapper.Map<EmployeeDto>(item);
        }

        // POST api/<controller>
        public async Task<bool> Post([FromBody] EmployeeInfo obj)
        {
            return await _employeeService.SaveEmployeeAsync(obj);
        }

        // PUT api/<controller>?employeeCode=123
        public async Task<bool> Put(string employeeCode, [FromBody] EmployeeInfo obj)
        {
            return await _employeeService.SaveEmployeeAsync(obj);
        }

        // DELETE api/<controller>?siteId=1&companyCode=abc&employeeCode=123
        public async Task<bool> Delete(string siteId, string companyCode, string employeeCode)
        {
            return await _employeeService.DeleteEmployeeAsync(siteId, companyCode, employeeCode);
        }
    }
}