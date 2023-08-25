using System;
using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace BusinessLayer.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IEmployeeRepository _employeeRepository;
		private readonly IMapper _mapper;

		public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
		{
			_employeeRepository = employeeRepository;
			_mapper = mapper;
		}
		public async Task<IEnumerable<EmployeeInfo>> GetAllAsync()
		{
			var res = await _employeeRepository.GetAllAsync();
			return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
		}

		public async Task<EmployeeInfo> GetByCodeAsync(string employeeCode)
		{
			var result = await _employeeRepository.GetByCodeAsync(employeeCode);
			return _mapper.Map<EmployeeInfo>(result);
		}

		public async Task<EmployeeSaveResult> SaveAsync(EmployeeInfo employeeInfo)
		{
			return await SaveAsync(employeeInfo, null);
		}

		public async Task<EmployeeSaveResult> SaveAsync(EmployeeInfo employeeInfo, EmployeeInfo existing)
		{
			var employee = _mapper.Map<Employee>(employeeInfo);

			if (string.IsNullOrWhiteSpace(employee.CompanyCode))
			{
				return EmployeeSaveResult.MissingCode;
			}

			if (!(existing is null) && employeeInfo.CompanyCode != existing.CompanyCode)
			{
				return EmployeeSaveResult.CannotChangeCode;
			}

			if (string.IsNullOrWhiteSpace(employee.EmployeeCode))
			{
				return EmployeeSaveResult.MissingCode;
			}

			if (!(existing is null) && employeeInfo.EmployeeCode != existing.EmployeeCode)
			{
				return EmployeeSaveResult.CannotChangeCode;
			}

			if (existing is null && !(employee.SiteId is null))
			{
				return EmployeeSaveResult.InvalidValue;
			}

			// Assuming this is for sharding.
			// The algorithm would be (much) more intelligent in production.
			var rand = new Random();
			employee.SiteId = CompanyService.Sites[rand.Next(CompanyService.Sites.Length)];

			if (await _employeeRepository.SaveAsync(employee))
			{
				return EmployeeSaveResult.Success;
			}

			return EmployeeSaveResult.DuplicateKey;
		}

		public async Task<bool> DeleteAsync(string employeeCode)
		{
			return await _employeeRepository.DeleteAsync(employeeCode);
		}
	}
}
