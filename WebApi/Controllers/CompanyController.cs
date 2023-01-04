using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
         private readonly ILogger _logger;

        public CompanyController(ICompanyService companyService, IMapper mapper,ILogger logger)
        {
            _companyService = companyService;
            _mapper = mapper;
            _logger = logger;
        }
        // GET api/<controller>
        public async IEnumerable<CompanyDto> GetAll()
        {
        try {
            var items = await _companyService.GetAllCompanies();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
            }
            catch(Exception e)
            {
            _logger.log(e);
            }
        }

        // GET api/<controller>/5
        public async CompanyDto Get(string companyCode)
        {
        try{
            var item = await _companyService.GetCompanyByCode(companyCode);
            return _mapper.Map<CompanyDto>(item);
            }
             catch(Exception e)
            {
            _logger.log(e);
            }
        }

        // POST api/<controller>
        public async bool Post(CompanyDto companyDto)
        {
        try{
            var result = await _companyService.SaveCompanyDetails(_mapper.Map<CompanyInfo>(companyDto));
            return result;
            }
             catch(Exception e)
            {
            _logger.log(e);
            }
        }

        // PUT api/<controller>/5
        public async bool Put(string companyCode, CompanyDto companyDto)
        {
        try{
            var result = await _companyService.SaveCompanyDetails(string companyCode, _mapper.Map<CompanyInfo>(companyDto));
            return result;
            }
             catch(Exception e)
            {
            _logger.log(e);
            }
        }

        // DELETE api/<controller>/5
        public async bool Delete(string companyCode)
        {
        try{
            var result = await _companyService.DeleteCompanyDetails(string companyCode);
            return result;
            }
             catch(Exception e)
            {
            _logger.log(e);
            }
        }
    }
}
