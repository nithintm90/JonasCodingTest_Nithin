using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{
    public interface IGenericRepository<T> where T : DataEntity, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByCodeAsync(string code);
        Task<bool> Exists(string code);
        Task DeleteAsync(string code);
        Task<bool> Create(T company);
        Task<bool> Update(T company);
    }
}
