﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        // NLog logger class
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        // GET api/<controller>
        [HttpGet]
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            var items = await _companyService.GetAllCompaniesAsync();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/company/{companyCode}")]
        [ResponseType(typeof(CompanyDto))]
        public async Task<IHttpActionResult> Get(string companyCode)
        {
            var item = await _companyService.GetCompanyByCodeAsync(companyCode);
            if (item == null)
            {
                _logger.Error("NotFound - companyCode: " + companyCode);
                return NotFound();
            }

            return Ok(_mapper.Map<CompanyDto>(item));
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]CompanyDto companyDto)
        {
            try
            {
                bool success = await _companyService.SaveCompanyAsync(_mapper.Map<CompanyInfo>(companyDto));
                if (success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                //log the received create object too
                _logger.Error(ex, "Exception while Create new company. Obj: " + JsonConvert.ToString(companyDto));
                return InternalServerError(ex);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody] CompanyDto companyDto)
        {
            //can edit existing company only
            if (id == 0)
            {
                return BadRequest();
            }

            try
            {
                CompanyInfo obj = _mapper.Map<CompanyInfo>(companyDto);
                if (id.ToString() != obj.CompanyCode)
                {
                    return BadRequest();
                }

                bool success = await _companyService.SaveCompanyAsync(obj);
                if (success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                //log the received update object too.
                _logger.Error(ex, "Exception while updating company. Obj: " + JsonConvert.ToString(companyDto));
                return InternalServerError(ex);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            //can delete existing company only
            if (id == 0)
            {
                return BadRequest();
            }

            try
            {
                bool success = await _companyService.DeleteCompanyAsync(id.ToString());
                if (success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception while deleting company. company code: " + id);
                return InternalServerError(ex);
            }
        }
    }
}