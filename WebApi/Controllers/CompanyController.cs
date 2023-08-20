using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using Newtonsoft.Json.Linq;
using WebApi.Models;
using System.Net;
using Serilog;
using System.Threading;

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
        public async Task<IHttpActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var items = await _companyService.GetAllCompanies(cancellationToken);
                var companies = _mapper.Map<IEnumerable<CompanyDto>>(items);
                return Ok(companies);
            }
            catch (Exception ex)
            {
                Logger.LogError(Newtonsoft.Json.JsonConvert.SerializeObject(ex));
                return StatusCode(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        // GET api/<controller>/5
        public async Task<IHttpActionResult> Get([FromUri]string companyCode, CancellationToken cancellationToken)
        {
            try
            {
                var item = await _companyService.GetCompanyByCode(companyCode, cancellationToken);
                return Ok(_mapper.Map<CompanyDto>(item));
            }
            catch (Exception ex)
            {
                Logger.LogError(Newtonsoft.Json.JsonConvert.SerializeObject(ex));

                return StatusCode(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> Post([FromBody] CompanyDto value, CancellationToken cancellationToken)
        {
            if (value == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                //put validation checks here
                var item = _mapper.Map<CompanyInfo>(value);
                var result = await _companyService.SaveCompany(item, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.LogError(Newtonsoft.Json.JsonConvert.SerializeObject(ex));
                return StatusCode(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        // PUT api/<controller>/5
        public async Task<IHttpActionResult> Put([FromUri]int id, [FromBody] CompanyDto value, CancellationToken cancellationToken)
        {
            //Ideally put should not create a resource..
            if (value == null || !ModelState.IsValid)
            {
                if (id <= 0)
                {
                    ModelState.AddModelError("id", "id must be greater than zero");
                }
                return BadRequest(ModelState);
            }

            try
            {
                var item = _mapper.Map<CompanyInfo>(value);
                var result = await _companyService.SaveCompany(item, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.LogError(Newtonsoft.Json.JsonConvert.SerializeObject(ex));
                return StatusCode(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        // DELETE api/<controller>/5
        public async Task<IHttpActionResult> Delete([Required] string companyCode, CancellationToken cancellationToken)
        {
            if (companyCode == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _companyService.DeleteCompany(companyCode, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.LogError(Newtonsoft.Json.JsonConvert.SerializeObject(ex));
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }
}