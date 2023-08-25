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
	public class CompanyController : ApiController
	{
		private readonly ICompanyService _companyService;
		private readonly IMapper _mapper;

		public CompanyController(ICompanyService companyService, IMapper mapper)
		{
			_companyService = companyService;
			_mapper = mapper;
		}

		public async Task<IEnumerable<CompanyDto>> GetAllAsync()
		{
			var items = await _companyService.GetAllCompaniesAsync();
			return _mapper.Map<IEnumerable<CompanyDto>>(items);
		}

		public async Task<IHttpActionResult> GetAsync(string companyCode)
		{
			var item = await _companyService.GetCompanyByCodeAsync(companyCode);
			if (item is null)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<CompanyDto>(item));
		}

		public async Task<IHttpActionResult> PostAsync([FromBody] CompanyDto companyDto)
		{
			var companyInfo = _mapper.Map<CompanyInfo>(companyDto);
			var res = await _companyService.SaveAsync(companyInfo);
			return SaveResultToActionResult(res, companyInfo);
		}

		public async Task<IHttpActionResult> PutAsync(string companyCode, [FromBody] CompanyDto companyDto)
		{
			var oldItem = await _companyService.GetCompanyByCodeAsync(companyCode);
			if (oldItem is null)
			{
				return NotFound();
			}

			var newItem = _mapper.Map<CompanyInfo>(companyDto);
			return SaveResultToActionResult(await _companyService.SaveAsync(newItem, oldItem), newItem);
		}

		public async Task<IHttpActionResult> DeleteAsync(string companyCode)
		{
			if (await _companyService.DeleteAsync(companyCode))
			{
				return Ok();
			}

			// This operation shouldn't fail.
			return InternalServerError();
		}

		private IHttpActionResult SaveResultToActionResult(SaveResult result, CompanyInfo companyInfo)
		{
			switch (result)
			{
				case SaveResult.Success:
					return Created(
						$"/api/company/{companyInfo?.CompanyCode}",
						_mapper.Map<CompanyDto>(companyInfo)
					);

				case SaveResult.DuplicateKey:
					return BadRequest("Company code already exists.");

				case SaveResult.MissingCode:
					return BadRequest("Missing company code.");

				case SaveResult.InvalidValue:
					return BadRequest("Cannot specify value for site ID.");

				case SaveResult.CannotChangeCode:
					return BadRequest("Cannot change company code.");

				default:
					throw new NotSupportedException();
			}
		}
	}
}