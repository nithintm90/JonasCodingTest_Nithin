using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ICompanyService companyService, IMapper mapper, ILogger<CompanyController> logger)
        {
            _companyService = companyService;
            _mapper = mapper;
            _logger = logger;
        }
        // GET api/<controller>
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            _logger.LogInformation("GetAllCompanies called");
            var items = await _companyService.GetAllCompanies().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        // GET api/<controller>/5
        public async Task<CompanyDto> Get(string companyCode)
        {
            _logger.LogInformation($"GetCompanyByCode called with companyCode {companyCode}");
            var item = await _companyService.GetCompanyByCode(companyCode).ConfigureAwait(false);
            return _mapper.Map<CompanyDto>(item);
        }

        // POST api/<controller>
        public async Task<bool> Post([FromBody]CompanyDto company)
        {
            _logger.LogInformation($"Post called with company body {JsonConvert.SerializeObject(company)}");
            var companyInfo = _mapper.Map<CompanyInfo>(company);
            return await _companyService.SaveCompany(companyInfo).ConfigureAwait(false);
        }

        // PUT api/<controller>/5
        public async Task<bool> Put(string companyCode, [FromBody]CompanyDto company)
        {
            _logger.LogInformation($"PUT called for company code: {companyCode} with body {JsonConvert.SerializeObject(company)}");

            if (companyCode != company.CompanyCode)
            {
                _logger.LogCritical($"Company code does not match: {companyCode}");
                throw new Exception("Company code does not match");
            }
            var companyInfo = _mapper.Map<CompanyInfo>(company);
            return await _companyService.SaveCompany(companyInfo).ConfigureAwait(false);
        }

        // DELETE api/<controller>/5
        public async Task<bool> Delete(string companyCode)
        {
            _logger.LogInformation($"Delete called for company code: {companyCode}");
            return await _companyService.DeleteCompany(companyCode).ConfigureAwait(false);
        }
    }
}