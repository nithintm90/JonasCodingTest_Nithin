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
        public async Task<IEnumerable<CompanyDto>> GetAllAsync()
        {
            var items = await _companyService.GetAllCompaniesAsync();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        // GET api/<controller>/5
        public async Task<CompanyDto> GetAsync(string companyCode)
        {
            var item = await _companyService.GetCompanyByCodeAsync(companyCode);
            return _mapper.Map<CompanyDto>(item);
        }

        // POST api/<controller>
        public async Task Post([FromBody] CreateUpdateCompanyRequest req)
        {
            await _companyService.CreateCompanyAsync(req);
            Ok();
        }

        // PUT api/<controller>/5
        public async Task Put(string id, [FromBody] CreateUpdateCompanyRequest req)
        {
            await _companyService.UpdateCompanyAsync(req, id);
            Ok();
        }

        // DELETE api/<controller>/5
        public async Task Delete(string id)
        {
            await _companyService.DeleteCompanyAsync(id);
            Ok();
        }
    }
}