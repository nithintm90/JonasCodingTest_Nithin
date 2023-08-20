using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using Newtonsoft.Json.Linq;
using WebApi.Models;
using System.Net;
using Serilog;
using System.Threading;

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
        public async Task<IHttpActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var items = await _employeeService.GetAllEmployees(cancellationToken);
                var companies = _mapper.Map<IEnumerable<EmployeeDto>>(items);
                return Ok(companies);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                return Content(HttpStatusCode.InternalServerError, "An Internal Error Occurred");
            }
        }

        // GET api/<controller>/5
        public async Task<IHttpActionResult> Get([FromUri] string employeeCode, CancellationToken cancellationToken)
        {
            try
            {
                var item = await _employeeService.GetEmployeeByCode(employeeCode, cancellationToken);
                return Ok(_mapper.Map<EmployeeDto>(item));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());

                return Content(HttpStatusCode.InternalServerError, "An Internal Error Occurred");
            }
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> Post([FromBody] EmployeeDto value, CancellationToken cancellationToken)
        {
            if (value == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                //put validation checks here
                var item = _mapper.Map<EmployeeInfo>(value);
                var result = await _employeeService.SaveEmployee(item, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                return Content(HttpStatusCode.InternalServerError, "An Internal Error Occurred");
            }
        }

        // PUT api/<controller>/5
        public async Task<IHttpActionResult> Put([FromUri] string employeeCode, [FromBody] EmployeeDto value, CancellationToken cancellationToken)
        {
            //Ideally put should not create a resource..
            if (value == null || !ModelState.IsValid || String.IsNullOrEmpty(employeeCode))
            {
                if (String.IsNullOrEmpty(employeeCode))
                {
                    ModelState.AddModelError("employeeCode", "employeeCode must be provided");
                }
                return BadRequest(ModelState);
            }
            else if (employeeCode != value.EmployeeCode)
            {
                ModelState.AddModelError("employeeCode", "employeeCode does not match!"); //assumption
                return BadRequest(ModelState);
            }

            try
            {
                var item = _mapper.Map<EmployeeInfo>(value);
                var result = await _employeeService.SaveEmployee(item, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                return Content(HttpStatusCode.InternalServerError, "An Internal Error Occurred");
            }
        }

        // DELETE api/<controller>/5
        public async Task<IHttpActionResult> Delete([Required] string employeeCode, CancellationToken cancellationToken)
        {
            if (employeeCode == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            try
            {
                var result = await _employeeService.DeleteEmployee(employeeCode, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                return Content(HttpStatusCode.InternalServerError, "An Internal Error Occurred");
            }
        }
    }
}