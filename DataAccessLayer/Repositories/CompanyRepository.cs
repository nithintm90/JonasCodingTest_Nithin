using AutoMapper;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IDbWrapper<Company> companyDbWrapper, IMapper mapper): base(companyDbWrapper, mapper) {}
    }
}
