using AutoMapper;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using Serilog;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IDbWrapper<Employee> companyDbWrapper, IMapper mapper, ILogger logger) 
            : base(companyDbWrapper, mapper, logger) {}
    }
}
