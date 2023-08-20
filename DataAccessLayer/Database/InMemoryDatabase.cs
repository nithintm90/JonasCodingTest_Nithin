using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Database
{
	public class InMemoryDatabase<T> : IDbWrapper<T> where T : DataEntity
	{
		private static Dictionary<Tuple<string, string>, DataEntity> DatabaseInstance = new Dictionary<Tuple<string, string>, DataEntity>();

		public InMemoryDatabase()
		{
			//DatabaseInstance = new Dictionary<Tuple<string, string>, DataEntity>();
		}

		public bool Insert(T data, CancellationToken cancellationToken)
		{
			try
			{
				DatabaseInstance.Add(Tuple.Create(data.SiteId, data.CompanyCode), data);
				return true;
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		public bool Update(T data, CancellationToken cancellationToken)
		{
			try
			{
				if (DatabaseInstance.ContainsKey(Tuple.Create(data.SiteId, data.CompanyCode)))
				{
					DatabaseInstance.Remove(Tuple.Create(data.SiteId, data.CompanyCode));
					Insert(data, cancellationToken);
					return true;
				}

				return false;
			}
			catch(Exception ex)
            {
                throw;
            }
		}

		public IEnumerable<T> Find(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
		{
			try
			{
				var entities = FindAll(cancellationToken);
				return entities.Where(expression.Compile());
			}
			catch(Exception ex)
            {
                throw;
            }
		}

		public IEnumerable<T> FindAll(CancellationToken cancellationToken)
		{
			try
			{
				return DatabaseInstance.Values.OfType<T>();
			}
			catch(Exception ex)
            {
                throw;
            }
		}

		public bool Delete(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
		{
			try
			{
				var entities = FindAll(cancellationToken);
				var entity = entities.Where(expression.Compile());
				foreach (var dataEntity in entity.ToList())
				{
					DatabaseInstance.Remove(Tuple.Create(dataEntity.SiteId, dataEntity.CompanyCode));
				}
				
				return true;
			}
			catch(Exception ex)
            {
                throw;
            }
		}

		public bool DeleteAll(CancellationToken cancellationToken)
		{
			try
			{
				DatabaseInstance.Clear();
				return true;
			}
			catch(Exception ex)
            {
                throw;
            }
		}

		public bool UpdateAll(Expression<Func<T, bool>> filter, string fieldToUpdate, object newValue, CancellationToken cancellationToken)
		{
			try
			{
				var entities = FindAll(cancellationToken);
				var entity = entities.Where(filter.Compile());
				foreach (var dataEntity in entity)
				{
					var newEntity = UpdateProperty(dataEntity, fieldToUpdate, newValue, cancellationToken);

					DatabaseInstance.Remove(Tuple.Create(dataEntity.SiteId, dataEntity.CompanyCode));
					Insert(newEntity, cancellationToken);
				}

				return true;
			}
			catch(Exception ex)
            {
                throw;
            }
		}

		private T UpdateProperty(T dataEntity, string key, object value, CancellationToken cancellationToken)
		{
			Type t = typeof(T);
			var prop = t.GetProperty(key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

			if (prop == null)
			{
				throw new Exception("Property not found");
			}

			prop.SetValue(dataEntity, value, null);
			return dataEntity;
		}

		public Task<bool> InsertAsync(T data, CancellationToken cancellationToken)
		{
			return Task.FromResult(Insert(data, cancellationToken));
		}

		public Task<bool> UpdateAsync(T data, CancellationToken cancellationToken)
		{
			return Task.FromResult(Update(data, cancellationToken));
		}

		public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
		{
			return Task.FromResult(Find(expression, cancellationToken));
		}

		public Task<IEnumerable<T>> FindAllAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult(FindAll(cancellationToken));
		}

		public Task<bool> DeleteAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
		{
			return Task.FromResult(Delete(expression, cancellationToken));
		}

		public Task<bool> DeleteAllAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult(DeleteAll(cancellationToken));
		}

		public Task<bool> UpdateAllAsync(Expression<Func<T, bool>> filter, string fieldToUpdate, object newValue, CancellationToken cancellationToken)
		{
			return Task.FromResult(UpdateAll(filter, fieldToUpdate, newValue, cancellationToken));
		}

	
	}
}
