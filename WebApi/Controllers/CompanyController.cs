using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger log;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
             log = SerilogClass._logger;
        }
        // GET api/<controller>
        [HttpGet]
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            try
            {
                var items = await _companyService.GetAllCompaniesAsync();
                return _mapper.Map<IEnumerable<CompanyDto>>(items);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }
        }

        // GET api/<controller>/5
      [HttpGet]
        public async Task<CompanyDto> Get(string companyCode)
        {
            try
            {
                var item = await _companyService.GetCompanyByCodeAsync(companyCode);
                return _mapper.Map<CompanyDto>(item);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]CompanyDto companyDto)
        {
            try
            {

           
            var company = _mapper.Map<CompanyInfo>(companyDto);
            await _companyService.SaveCompanyAsync(company);
                log.Information("A Company is Saved");
                return Ok();
                
               
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }

        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IHttpActionResult> Update(string id, [FromBody]CompanyDto companyDto)
        {
            try
            {
                var company = _mapper.Map<CompanyInfo>(companyDto);
                await _companyService.UpdateCompanyAsync(id, company);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }

        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCompany(string id)
        {
            try
            {
                await _companyService.DeleteCompanyAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }
        }
    }
}