using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.Threading.Tasks;
using System.Threading;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployees(CancellationToken cancellationToken)
        {
            var res = await _employeeRepository.GetAll(cancellationToken);
            return  _mapper.Map<IEnumerable<EmployeeInfo>>(res);
        }

        public async Task<EmployeeInfo> GetEmployeeByCode(string employeeCode, CancellationToken cancellationToken)
        {
            var result = await _employeeRepository.GetByCode(employeeCode, cancellationToken);
            return _mapper.Map<EmployeeInfo>(result);
        }

        public Task<bool> SaveEmployee(EmployeeInfo employeeInfo, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Employee>(employeeInfo);
            var result = _employeeRepository.SaveEmployee(item, cancellationToken);
            return result;
        }

        public Task<bool> DeleteEmployee(string employeeCode, CancellationToken cancellationToken)
        {
            var result = _employeeRepository.DeleteEmployee(employeeCode, cancellationToken);
            return result;
        }
    }
}
