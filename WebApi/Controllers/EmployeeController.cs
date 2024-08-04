using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger log;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            log = SerilogClass._logger;
        }
        // GET api/<controller>
        [HttpGet]
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            try
            {
                var items = await _employeeService.GetAllEmployeesAsync();
                return _mapper.Map<IEnumerable<EmployeeDto>>(items);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task<EmployeeDto> Get(string employeeCode)
        {
            try
            {
                var item = await _employeeService.GetEmployeeByCodeAsync(employeeCode);
                return _mapper.Map<EmployeeDto>(item);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] EmployeeDto employeeDto)
        {
            try
            {


                var employee = _mapper.Map<EmployeeInfo>(employeeDto);
                await _employeeService.SaveEmployeeAsync(employee);
                log.Information("An Employee is Saved");
                return Ok();


            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }

        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IHttpActionResult> Update(string id, [FromBody] EmployeeDto employeeDto)
        {
            try
            {
                var employee = _mapper.Map<EmployeeInfo>(employeeDto);
                await _employeeService.UpdateEmployeeAsync(id, employee);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }

        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteEmployee(string id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }
        }
    }
}

