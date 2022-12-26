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
            try
            {
                var items = await _companyService.GetAllCompaniesAsync();
                return _mapper.Map<IEnumerable<CompanyDto>>(items);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return null;
            }
        }

        // GET api/<controller>/5
        public async Task<CompanyDto> Get(string companyCode)
        {
            try
            {
                var item = await _companyService.GetCompanyByCodeAsync(companyCode);
                return _mapper.Map<CompanyDto>(item);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return null;
            }
        }

        // POST api/<controller>
        public async Task<bool> Post([FromBody] CompanyDto companyDto)
        {
            try
            {
                var company = _mapper.Map<CompanyInfo>(companyDto);

                return await this._companyService.SaveCompanyAsync(company);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return false;
            }
        }

        // PUT api/<controller>/5
        public async Task<bool> Put(string id, [FromBody] CompanyDto companyDto)
        {

            if (id != companyDto.CompanyCode)
            {
                throw new Exception("Invalid Category Id");
            }
            try
            {
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
                catch (Exception ex)
                {
                    Logger.LogError($"Error occured while updating CompanyCode {id} Error: {ex.Message} ");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return false;
            }
            
        }

        // DELETE api/<controller>/5
        public async Task<bool> Delete(string companyCode)
        {
            try
            {
                bool result = await _companyService.DeleteCompanyAsync(companyCode);
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return false;
            }
        }
    }
}