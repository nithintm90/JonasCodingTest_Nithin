using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http;
using System.Web.Routing;
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

		public IEnumerable<CompanyDto> GetAll()
		{
			var items = _companyService.GetAllCompanies();
			return _mapper.Map<IEnumerable<CompanyDto>>(items);
		}

		public IHttpActionResult Get(string companyCode)
		{
			var item = _companyService.GetCompanyByCode(companyCode);
			if (item is null)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<CompanyDto>(item));
		}

		public IHttpActionResult Post([FromBody] CompanyDto companyDto)
		{
			var companyInfo = _mapper.Map<CompanyInfo>(companyDto);
			var res = _companyService.Save(companyInfo);
			return SaveResultToActionResult(res, companyInfo);
		}

		public IHttpActionResult Put(string companyCode, [FromBody] CompanyDto companyDto)
		{
			var oldItem = _companyService.GetCompanyByCode(companyCode);
			if (oldItem is null)
			{
				return NotFound();
			}

			var newItem = _mapper.Map<CompanyInfo>(companyDto);
			return SaveResultToActionResult(_companyService.Save(newItem, oldItem), newItem);
		}

		public IHttpActionResult Delete(string companyCode)
		{
			if (_companyService.Delete(companyCode))
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