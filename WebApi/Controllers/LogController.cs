using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class LogController : ApiController
    {
        private readonly ILoggerRepository _LogService;

        public LogController(ILoggerRepository logService)
        {
            _LogService = logService;
        }
        // GET api/<controller>
        public async Task<IEnumerable<LogEntity>> GetAll()
        {
            return await _LogService.GetAllAsync();
        }
    }
}