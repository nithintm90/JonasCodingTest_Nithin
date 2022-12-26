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

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        // GET api/<controller>
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            try
            {
                var items = await _employeeService.GetAllEmployeeAsync();
                return _mapper.Map<IEnumerable<EmployeeDto>>(items);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return null;
            }
        }


        // POST api/<controller>
        public async Task<bool> Post([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                var employee = _mapper.Map<EmployeeInfo>(employeeDto);

                return await this._employeeService.SaveEmployeeAsync(employee);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return false;
            }
        }


    }
}