using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [RoutePrefix("api/employee")]
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

        [HttpGet]
        public async Task<IEnumerable<EmployeeInfo>> GetAll()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return employees;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(EmployeeInfo employeeInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Apply Logger
            _logger.LogInformation("Post method called from Employee");

            var result = await _employeeService.SaveEmployeeAsync(employeeInfo);
            if (!result)
                throw new Exception("Could not Save the Employee");
            

            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(string employeeCode, EmployeeInfo employeeInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Apply Logger
            _logger.LogInformation("Put method called from Employee");

            var result = await _employeeService.UpdateEmployeeAsync(employeeCode, employeeInfo);
            if (!result)
                throw new Exception("Could not Update the Employee");


            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string employeeCode)
        {
            //Apply Logger
            _logger.LogInformation("Delete method called from Employee");

            var result = await _employeeService.DeleteEmployeeAsync(employeeCode);
            if (!result)
                throw new Exception("Could not Delete the Employee");


            return Ok();
        }
    }
}
