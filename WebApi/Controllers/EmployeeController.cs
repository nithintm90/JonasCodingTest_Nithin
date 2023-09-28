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
        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var items = await _employeeService.GetAllEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(items);
        }

        // GET api/<controller>/5
        public async Task<EmployeeDto> GetAsync(string employeeCode)
        {
            var item = await _employeeService.GetEmployeeByCodeAsync(employeeCode);
            return _mapper.Map<EmployeeDto>(item);
        }

        // POST api/<controller>
        public async Task Post([FromBody] CreateUpdateEmployeeRequest req)
        {
            await _employeeService.CreateEmployeeAsync(req);
        }

        // PUT api/<controller>/5
        public async Task Put(string id, [FromBody] CreateUpdateEmployeeRequest req)
        {
            await _employeeService.UpdateEmployeeAsync(req, id);
        }

        // DELETE api/<controller>/5
        public async Task Delete(string id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
        }
    }
}