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
            var items = await _companyService.GetAllCompaniesAsync();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        // GET api/<controller>/5
        public async Task<CompanyDto> Get(string companyCode)
        {
            var item = await _companyService.GetCompanyByCodeAsync(companyCode);
            return _mapper.Map<CompanyDto>(item);
        }

        // POST api/<controller>
        public async Task<bool> Post([FromBody] CompanyDto companyDto)
        {
            var company = _mapper.Map<CompanyInfo>(companyDto);

            return await this._companyService.SaveCompanyAsync(company);
        }

        // PUT api/<controller>/5
        public async Task<bool> Put(string id, [FromBody] CompanyDto companyDto)
        {

            if (id != companyDto.CompanyCode)
            {
                throw new Exception("Invalid Category Id");
            }

            var companyInfo = await this._companyService.GetCompanyByCodeAsync(id);

            _mapper.Map(companyDto, companyInfo);

            if (companyInfo == null)
            {
                throw new Exception($"CompanyCode {id} is not found.");
            }

            try
            {
                return await this._companyService.SaveCompanyAsync(companyInfo);
            }
            catch (Exception)
            {
                throw new Exception($"Error occured while updating CompanyCode {id}.");
            }
            
        }

        // DELETE api/<controller>/5
        public async Task<bool> Delete(string companyCode)
        {
            bool result = await _companyService.DeleteCompanyAsync(companyCode);
             return result;
        }
    }
}