using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using Microsoft.Extensions.Logging;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api/company")]
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
        public IEnumerable<CompanyDto> GetAll()
        {
            var items = _companyService.GetAllCompanies();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        // GET api/<controller>/5
        public CompanyDto Get(string companyCode)
        {
            var item = _companyService.GetCompanyByCode(companyCode);
            return _mapper.Map<CompanyDto>(item);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IHttpActionResult> Post(CompanyDto companyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //Apply Logger
            _logger.LogInformation("Post method called from Company");

            var company = _mapper.Map<CompanyInfo>(companyDto);
            var result = await _companyService.SaveCompanyAsync(company);

            if (!result)
                throw new Exception("The Post Operation failed");
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(string companyCode, CompanyDto companyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Apply Logger
            _logger.LogInformation("Put method called from Company");

            var company = _mapper.Map<CompanyInfo>(companyDto);
            var result = await _companyService.UpdateCompanyAsync(companyCode, company);

            if (!result)
                throw new Exception("The PUT Operation failed");
            return Ok();
            
        }


        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string companyCode)
        {
            //Apply Logger
            _logger.LogInformation("Delete method called from Company");

            var result = await _companyService.DeleteCompanyAsync(companyCode);

            if (!result)
                throw new Exception("The DELETE Operation failed");
            return Ok();
        }
    }
}