using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using log4net;
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
        private readonly ILog _logger;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        // GET api/employee
        public async Task<IHttpActionResult> GetAllAsync()
        {
            try
            {
                _logger.Info("GetAllAsync request");
                var items = await _employeeService.GetAllEmployeesAsync();
                return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(items));
            }
            catch (Exception ex)
            {
                _logger.Error($"GetAllAsync exception , error: {ex.Message}", ex);
                return InternalServerError(ex);
            }
        }

        // GET api/employee/{employeeCode}
        public async Task<IHttpActionResult> GetAsync(string employeeCode)
        {
            try
            {
                _logger.Info($"GetAsync request, employee code: {employeeCode}");
                if (string.IsNullOrEmpty(employeeCode))
                    return BadRequest("The employee code can not be empty.");

                var item = await _employeeService.GetEmployeeByCodeAsync(employeeCode);
                return Ok(_mapper.Map<EmployeeInfo, EmployeeDto>(item));
            }
            catch (Exception ex)
            {
                _logger.Error($"GetAsync exception, employee code: {employeeCode}, error: {ex.Message}", ex);
                return InternalServerError(ex);
            }
        }

        // POST api/employee
        public async Task<IHttpActionResult> PostAsync([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                _logger.Info($"PostAsync request, employee code: {employeeDto.CompanyCode}");
                if (employeeDto == null)
                    return BadRequest("The employee can not be empty.");

                var employeeInfo = _mapper.Map<EmployeeInfo>(employeeDto);
                var result = await _employeeService.SaveEmployeeAsync(employeeInfo);
                _logger.Info($"PostAsync result, employee code: {employeeDto.CompanyCode}, result:{result}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error($"PostAsync exception, employee code: {employeeDto.CompanyCode}, error: {ex.Message}", ex);
                return InternalServerError(ex);
            }
        }

        // PUT api/employee/{employeeCode}
        public async Task<IHttpActionResult> PutAsync(string employeeCode, [FromBody] EmployeeDto employeeDto)
        {
            try
            {
                _logger.Info($"PutAsync request, employee code: {employeeCode}");
                if (string.IsNullOrEmpty(employeeCode))
                    return BadRequest("The employee code can not be empty.");
                if (employeeDto == null)
                    return BadRequest("The employee can not be empty.");

                var employeeInfo = _mapper.Map<EmployeeInfo>(employeeDto);
                var result = await _employeeService.UpdateEmployeeAsync(employeeInfo, employeeCode);
                _logger.Info($"PutAsync result, employee code: {employeeDto.CompanyCode}, result:{result}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error($"PutAsync exception, employee code: {employeeCode}, error: {ex.Message}", ex);
                return InternalServerError(ex);
            }
        }

        // DELETE api/employee/{employeeCode}
        public async Task<IHttpActionResult> DeleteAsync(string employeeCode)
        {
            try
            {
                _logger.Info($"DeleteAsync request, employee code: {employeeCode}");
                if (string.IsNullOrEmpty(employeeCode))
                    return BadRequest("The employee code can not be empty.");

                var result = await _employeeService.DeleteEmployeeAsync(employeeCode);
                _logger.Info($"DeleteAsync result, employee code: {employeeCode}, result:{result}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error($"DeleteAsync exception, employee code: {employeeCode}, error: {ex.Message}", ex);
                return InternalServerError(ex);
            }
        }
    }
}