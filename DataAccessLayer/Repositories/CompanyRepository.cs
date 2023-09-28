using AutoMapper;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using Serilog;

namespace DataAccessLayer.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IDbWrapper<Company> companyDbWrapper, IMapper mapper, ILogger logger)
            : base(companyDbWrapper, mapper, logger) {}
    }
}
