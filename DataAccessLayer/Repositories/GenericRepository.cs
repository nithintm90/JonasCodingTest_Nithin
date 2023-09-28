using AutoMapper;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : DataEntity, new()
    {
        protected readonly IDbWrapper<T> DbWrapper;
        protected readonly IMapper Mapper;
        protected readonly ILogger Logger;

        public GenericRepository(IDbWrapper<T> companyDbWrapper, IMapper mapper, ILogger logger)
        {
            Mapper = mapper;
            DbWrapper = companyDbWrapper;
            Logger = logger;
        }

        public async Task<bool> Create(T entity)
        {
            Logger.Information($"Creating an entity: {typeof(T)}");

            entity.LastModified = DateTime.UtcNow;
            return await DbWrapper.InsertAsync(entity);
        }

        public async Task DeleteAsync(string code)
        {
            Logger.Information($"Deleting an entity: {typeof(T)} by code {code}");

            await DbWrapper.DeleteAsync(q => q.Code == code);
        }

        public async Task<bool> Exists(string code)
        {
            Logger.Information($"Checking if an entity {typeof(T)} exists by code {code}");

            IEnumerable<T> company = await DbWrapper.FindAsync(t => t.Code.Equals(code));
            return company.Any();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            Logger.Information($"Retrieving all entities {typeof(T)}");

            return await DbWrapper.FindAllAsync();
        }

        public async Task<T> GetByCodeAsync(string code)
        {
            Logger.Information($"Retrieving an entity {typeof(T)} by code {code}");

            IEnumerable<T> company = await DbWrapper.FindAsync(t => t.Code.Equals(code));
            return company.FirstOrDefault();
        }

        public async Task<bool> Update(T dto)
        {
            Logger.Information($"Updating an entity: {typeof(T)}");

            var entityToUpdate = DbWrapper.Find(t =>
                            t.SiteId.Equals(dto.SiteId) && t.Code.Equals(dto.Code))?.FirstOrDefault();
            if (entityToUpdate != null)
            {
                entityToUpdate = Mapper.Map(dto, entityToUpdate);
                entityToUpdate.LastModified = DateTime.UtcNow;
                return await DbWrapper.UpdateAsync(entityToUpdate);
            }

            return false;
        }
    }
}
