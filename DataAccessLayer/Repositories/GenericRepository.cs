using AutoMapper;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : DataEntity, new()
    {
        private readonly IDbWrapper<T> _dbWrapper;
        private readonly IMapper _mapper;
        public GenericRepository(IDbWrapper<T> companyDbWrapper, IMapper mapper)
        {
            _mapper = mapper;
            _dbWrapper = companyDbWrapper;
        }

        public async Task<bool> Create(T entity)
        {
            entity.LastModified = DateTime.UtcNow;
            return await _dbWrapper.InsertAsync(entity);
        }

        public async Task DeleteAsync(string code)
        {
            await _dbWrapper.DeleteAsync(q => q.Code == code);
        }

        public async Task<bool> Exists(string code)
        {
            IEnumerable<T> company = await _dbWrapper.FindAsync(t => t.Code.Equals(code));
            return company.Any();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbWrapper.FindAllAsync();
        }

        public async Task<T> GetByCodeAsync(string code)
        {
            IEnumerable<T> company = await _dbWrapper.FindAsync(t => t.Code.Equals(code));
            return company.FirstOrDefault();
        }

        public async Task<bool> Update(T dto)
        {
            var entityToUpdate = _dbWrapper.Find(t =>
                            t.SiteId.Equals(dto.SiteId) && t.Code.Equals(dto.Code))?.FirstOrDefault();
            if (entityToUpdate != null)
            {
                entityToUpdate = _mapper.Map(dto, entityToUpdate);
                entityToUpdate.LastModified = DateTime.UtcNow;
                return await _dbWrapper.UpdateAsync(entityToUpdate);
            }

            return false;
        }
    }
}
