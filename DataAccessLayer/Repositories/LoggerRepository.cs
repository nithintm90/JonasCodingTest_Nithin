using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class LoggerRepository : ILoggerRepository
    {
	    private readonly ILogWrapper<LogEntity> _logDbWrapper;

	    public LoggerRepository(ILogWrapper<LogEntity> logDbWrapper)
	    {
		    _logDbWrapper = logDbWrapper;
        }

        public async Task<IEnumerable<LogEntity>> GetAllAsync()
        {
            return await _logDbWrapper.FindAllAsync();
        }


        public async Task<bool> SaveLogAsync(LogEntity company)
        {
            company.IssuedAt = DateTime.Now;
            return await _logDbWrapper.InsertAsync(company);
        }

        public async Task<bool> ClearLogAsync()
        {
            return await _logDbWrapper.DeleteAllAsync();
        }
    }
}
