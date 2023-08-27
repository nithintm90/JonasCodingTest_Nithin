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
	public class CompanyService : ICompanyService
	{
		// This would normally be stored elsewhere. Could be config on disk or a db table.
		public static readonly string[] Sites = { "Bravo", "Hotel", "Lima" };

		private readonly ICompanyRepository _companyRepository;
		private readonly IMapper _mapper;

		public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
		{
			_companyRepository = companyRepository;
			_mapper = mapper;
		}
		public async Task<IEnumerable<CompanyInfo>> GetAllAsync()
		{
			var res = await _companyRepository.GetAllAsync();
			return _mapper.Map<IEnumerable<CompanyInfo>>(res);
		}

		public async Task<CompanyInfo> GetByCodeAsync(string companyCode)
		{
			var result = await _companyRepository.GetByCodeAsync(companyCode);
			return _mapper.Map<CompanyInfo>(result);
		}

		public async Task<CompanySaveResult> SaveAsync(CompanyInfo companyInfo)
		{
			return await SaveAsync(companyInfo, null);
		}

		public async Task<CompanySaveResult> SaveAsync(CompanyInfo companyInfo, CompanyInfo existing)
		{
			var company = _mapper.Map<Company>(companyInfo);

			if (string.IsNullOrWhiteSpace(company.CompanyCode))
			{
				return CompanySaveResult.MissingCode;
			}

			if (!(existing is null) && companyInfo.CompanyCode != existing.CompanyCode)
			{
				return CompanySaveResult.CannotChangeCode;
			}

			if (existing is null && !(company.SiteId is null))
			{
				return CompanySaveResult.InvalidValue;
			}

			// Assuming this is for sharding.
			// The algorithm would be (much) more intelligent in production.
			var rand = new Random();
			company.SiteId = Sites[rand.Next(Sites.Length)];

			if (await _companyRepository.SaveAsync(company))
			{
				return CompanySaveResult.Success;
			}

			return CompanySaveResult.DuplicateKey;
		}

		public async Task<bool> DeleteAsync(string companyCode)
		{
			return await _companyRepository.DeleteAsync(companyCode);
		}
	}
}
