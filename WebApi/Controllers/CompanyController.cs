using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using log4net;
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
        private readonly ILog _logger;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
            _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
        // GET api/company
        public async Task<IHttpActionResult> GetAllAsync()
        {
            try
            {
                _logger.Info("GetAllAsync request");
                var items = await _companyService.GetAllCompaniesAsync();
                return Ok(_mapper.Map<IEnumerable<CompanyDto>>(items));
            }
            catch (Exception ex)
            {
                _logger.Error($"GetAllAsync exception , error: {ex.Message}", ex);
                return InternalServerError(ex);
            }
        }

        // GET api/company/{companyCode}
        public async Task<IHttpActionResult> GetAsync(string companyCode)
        {
            try
            {
                _logger.Info($"GetAsync request, company code: {companyCode}");
                if (string.IsNullOrEmpty(companyCode))
                    return BadRequest("The company code can not be empty.");

                var item = await _companyService.GetCompanyByCodeAsync(companyCode);
                return Ok(_mapper.Map<CompanyDto>(item));
            }
            catch (Exception ex)
            {
                _logger.Error($"GetAsync exception, company code: {companyCode}, error: {ex.Message}", ex);
                return InternalServerError(ex);
            }
        }

        // POST api/company
        public async Task<IHttpActionResult> PostAsync([FromBody] CompanyDto companyDto)
        {
            try
            {
                _logger.Info($"PostAsync request, company code: {companyDto.CompanyCode}");
                if (companyDto == null)
                    return BadRequest("The company can not be empty.");

                var companyInfo = _mapper.Map<CompanyInfo>(companyDto);
                var result = await _companyService.SaveCompanyAsync(companyInfo);
                _logger.Info($"PostAsync result, company code: {companyDto.CompanyCode}, result:{result}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error($"PostAsync exception, company code: {companyDto.CompanyCode}, error: {ex.Message}", ex);
                return InternalServerError(ex);
            }
        }

        // PUT api/company/{companyCode}
        public async Task<IHttpActionResult> PutAsync(string companyCode, [FromBody] CompanyDto companyDto)
        {
            try
            {
                _logger.Info($"PutAsync request, company code: {companyCode}");
                if (string.IsNullOrEmpty(companyCode))
                    return BadRequest("The company code can not be empty.");
                if (companyDto == null)
                    return BadRequest("The company can not be empty.");

                var companyInfo = _mapper.Map<CompanyInfo>(companyDto);
                var result = await _companyService.UpdateCompanyAsync(companyInfo, companyCode);
                _logger.Info($"PutAsync result, company code: {companyDto.CompanyCode}, result:{result}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error($"PutAsync exception, company code: {companyCode}, error: {ex.Message}", ex);
                return InternalServerError(ex);
            }
        }

        // DELETE api/company/{companyCode}
        public async Task<IHttpActionResult> DeleteAsync(string companyCode)
        {
            try
            {
                _logger.Info($"DeleteAsync request, company code: {companyCode}");
                if (string.IsNullOrEmpty(companyCode))
                    return BadRequest("The company code can not be empty.");

                var result = await _companyService.DeleteCompanyAsync(companyCode);
                _logger.Info($"DeleteAsync result, company code: {companyCode}, result:{result}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error($"DeleteAsync exception, company code: {companyCode}, error: {ex.Message}", ex);
                return InternalServerError(ex);
            }
        }
    }
}