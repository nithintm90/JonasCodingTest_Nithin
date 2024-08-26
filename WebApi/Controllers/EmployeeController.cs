using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        // NLog logger class
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        // GET api/<controller>
        [HttpGet]
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            var items = await _employeeService.GetAllEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(items);
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/employee/{employeeCode}")]
        [ResponseType(typeof(EmployeeDto))]
        public async Task<IHttpActionResult> Get(string employeeCode)
        {
            var item = await _employeeService.GetEmployeeByCodeAsync(employeeCode);
            if (item == null)
            {
                _logger.Error("NotFound - employeeCode: " + employeeCode);
                return NotFound();
            }

            return Ok(_mapper.Map<EmployeeDto>(item));
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]EmployeeDto employeeDto)
        {
            try
            {
                bool success = await _employeeService.SaveEmployeeAsync(_mapper.Map<EmployeeInfo>(employeeDto));
                if (success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                //log the received create object too
                _logger.Error(ex, "Exception while Create new employee. Obj: " + JsonConvert.ToString(employeeDto));
                return InternalServerError(ex);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody] EmployeeDto employeeDto)
        {
            //can edit existing employee only
            if (id == 0)
            {
                return BadRequest();
            }

            try
            {
                EmployeeInfo obj = _mapper.Map<EmployeeInfo>(employeeDto);
                if (id.ToString() != obj.EmployeeCode)
                {
                    return BadRequest();
                }

                bool success = await _employeeService.SaveEmployeeAsync(obj);
                if (success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                //log the received update object too.
                _logger.Error(ex, "Exception while updating employee. Obj: " + JsonConvert.ToString(employeeDto));
                return InternalServerError(ex);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            //can delete existing employee only
            if (id == 0)
            {
                return BadRequest();
            }

            try
            {
                bool success = await _employeeService.DeleteEmployeeAsync(id.ToString());
                if (success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception while deleting employee. employee code: " + id);
                return InternalServerError(ex);
            }
        }
    }
}