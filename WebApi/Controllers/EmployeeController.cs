using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using System.Collections.Generic;
using System.Threading;
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

        // GET api/employee
        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var items = await _employeeService.GetAllEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(items);
        }

        // GET api/employee/{employeeCode}
        public async Task<EmployeeDto> GetAsync(string employeeCode)
        {
            var item = await _employeeService.GetEmployeeByCodeAsync(employeeCode);
            return _mapper.Map<EmployeeInfo, EmployeeDto>(item);
        }

        // POST api/employee
        public async Task<bool> PostAsync([FromBody] EmployeeDto employeeDto)
        {
            var employeeInfo = _mapper.Map<EmployeeInfo>(employeeDto);
            return await _employeeService.SaveEmployeeAsync(employeeInfo);
        }

        // PUT api/employee/{employeeCode}
        public async Task<bool> PutAsync(string employeeCode, [FromBody] EmployeeDto employeeDto)
        {
            var employeeInfo = _mapper.Map<EmployeeInfo>(employeeDto);
            return await _employeeService.UpdateEmployeeAsync(employeeInfo, employeeCode);
        }

        // DELETE api/employee/{employeeCode}
        public async Task<bool> DeleteAsync(string employeeCode)
        {
            return await _employeeService.DeleteEmployeeAsync(employeeCode);
        }
    }
}