using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;

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

        [HttpGet]
        public async Task<IEnumerable<EmployeeInfo>> GetAll()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return employees;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Save(EmployeeInfo employeeInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _employeeService.SaveEmployeeAsync(employeeInfo);
            if (!result)
                throw new Exception("Could not Save the Employee");
            

            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update(string employeeCode, EmployeeInfo employeeInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _employeeService.UpdateEmployeeAsync(employeeCode, employeeInfo);
            if (!result)
                throw new Exception("Could not Update the Employee");


            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string employeeCode)
        {
            var result = await _employeeService.DeleteEmployeeAsync(employeeCode);
            if (!result)
                throw new Exception("Could not Delete the Employee");


            return Ok();
        }
    }
}
