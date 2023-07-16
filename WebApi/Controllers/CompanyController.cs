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
        // GET api/<controller>
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            var items = await _companyService.GetAllCompanies().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        // GET api/<controller>/5
        public async Task<CompanyDto> Get(string companyCode)
        {
            var item = await _companyService.GetCompanyByCode(companyCode).ConfigureAwait(false);
            return _mapper.Map<CompanyDto>(item);
        }

        // POST api/<controller>
        public async Task<bool> Post([FromBody]CompanyDto company)
        {
            var companyInfo = _mapper.Map<CompanyInfo>(company);
            return await _companyService.SaveCompany(companyInfo).ConfigureAwait(false);
        }

        // PUT api/<controller>/5
        public async Task<bool> Put(string companyCode, [FromBody]CompanyDto company)
        {
            if (companyCode != company.CompanyCode)
            {
                throw new Exception("Company code does not match");
            }
            var companyInfo = _mapper.Map<CompanyInfo>(company);
            return await _companyService.SaveCompany(companyInfo).ConfigureAwait(false);
        }

        // DELETE api/<controller>/5
        public async Task<bool> Delete(string companyCode)
        {
            return await _companyService.DeleteCompany(companyCode).ConfigureAwait(false);
        }
    }
}