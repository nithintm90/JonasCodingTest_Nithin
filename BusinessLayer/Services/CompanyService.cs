using System;
using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace BusinessLayer.Services
{
	public class CompanyService : ICompanyService
	{
		public readonly string[] Sites = { "Bravo", "Hotel", "Lima" };

		private readonly ICompanyRepository _companyRepository;
		private readonly IMapper _mapper;

		public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
		{
			_companyRepository = companyRepository;
			_mapper = mapper;
		}
		public IEnumerable<CompanyInfo> GetAllCompanies()
		{
			var res = _companyRepository.GetAll();
			return _mapper.Map<IEnumerable<CompanyInfo>>(res);
		}

		public CompanyInfo GetCompanyByCode(string companyCode)
		{
			var result = _companyRepository.GetByCode(companyCode);
			return _mapper.Map<CompanyInfo>(result);
		}

		public SaveResult Save(CompanyInfo companyInfo)
		{
			return Save(companyInfo, null);
		}

		public SaveResult Save(CompanyInfo companyInfo, CompanyInfo existing)
		{
			var company = _mapper.Map<Company>(companyInfo);

			if (string.IsNullOrWhiteSpace(company.CompanyCode))
			{
				return SaveResult.MissingCode;
			}

			if (!(existing is null) && companyInfo.CompanyCode != existing.CompanyCode)
			{
				return SaveResult.CannotChangeCode;
			}

			if (existing is null && !(company.SiteId is null))
			{
				return SaveResult.InvalidValue;
			}

			// Assuming this is for sharding.
			// The algorithm would be (much) more intelligent in production.
			var rand = new Random();
			company.SiteId = Sites[rand.Next(Sites.Length)];

			if (_companyRepository.SaveCompany(company))
			{
				return SaveResult.Success;
			}

			return SaveResult.DuplicateKey;
		}

		public bool Delete(string companyCode)
		{
			return _companyRepository.Delete(companyCode);
		}
	}
}
