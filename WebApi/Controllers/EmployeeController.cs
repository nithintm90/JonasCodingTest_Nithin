using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using Serilog;
using WebApi.Models;

namespace WebApi.Controllers
{
	public class EmployeeController : ApiController
	{
		private readonly IEmployeeService _employeeService;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public EmployeeController(IEmployeeService employeeService, IMapper mapper, ILogger logger)
		{
			_employeeService = employeeService;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
		{
			var items = await _employeeService.GetAllAsync();
			return _mapper.Map<IEnumerable<EmployeeDto>>(items);
		}

		public async Task<IHttpActionResult> GetAsync(string employeeCode)
		{
			var item = await _employeeService.GetByCodeAsync(employeeCode);
			if (item is null)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<EmployeeDto>(item));
		}

		public async Task<IHttpActionResult> PostAsync([FromBody] EmployeeDto employeeDto)
		{
			var EmployeeInfo = _mapper.Map<EmployeeInfo>(employeeDto);
			var res = await _employeeService.SaveAsync(EmployeeInfo);
			return SaveResultToActionResult(res, EmployeeInfo);
		}

		public async Task<IHttpActionResult> PutAsync(string employeeCode, [FromBody] EmployeeDto employeeDto)
		{
			var oldItem = await _employeeService.GetByCodeAsync(employeeCode);
			if (oldItem is null)
			{
				return NotFound();
			}

			var newItem = _mapper.Map<EmployeeInfo>(employeeDto);
			return SaveResultToActionResult(await _employeeService.SaveAsync(newItem, oldItem), newItem);
		}

		public async Task<IHttpActionResult> DeleteAsync(string employeeCode)
		{
			if (await _employeeService.DeleteAsync(employeeCode))
			{
				return Ok();
			}

			// This operation shouldn't fail.
			_logger
				.ForContext(nameof(employeeCode), employeeCode)
				.Fatal("Unknown error attempting to delete employee.");
			return InternalServerError();
		}

		private IHttpActionResult SaveResultToActionResult(EmployeeSaveResult result, EmployeeInfo employeeInfo)
		{
			switch (result)
			{
				case EmployeeSaveResult.Success:
					return Created(
						$"/api/Employee/{employeeInfo?.EmployeeCode}",
						_mapper.Map<EmployeeDto>(employeeInfo)
					);

				case EmployeeSaveResult.DuplicateKey:
					return BadRequest("Employee code already exists.");

				case EmployeeSaveResult.MissingCode:
					return BadRequest("Missing Employee code.");

				case EmployeeSaveResult.InvalidValue:
					return BadRequest("Cannot specify value for site ID.");

				case EmployeeSaveResult.CannotChangeCode:
					return BadRequest("Cannot change Employee code.");

				default:
					_logger
						.ForContext(nameof(employeeInfo), employeeInfo, true)
						.ForContext(nameof(result), result)
						.Error("Unknown result.");
					throw new NotSupportedException();
			}
		}
	}
}