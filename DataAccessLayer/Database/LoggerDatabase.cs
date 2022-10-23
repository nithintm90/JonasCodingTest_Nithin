using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Database
{
	public class LoggerDatabase<LogEntity>: ILogWrapper<LogEntity>
	{
		private Dictionary<Guid, LogEntity> DatabaseInstance;

		public LoggerDatabase()
		{
			DatabaseInstance = new Dictionary<Guid, LogEntity>();
		}

		public bool Insert(LogEntity data)
		{
			try
			{
				DatabaseInstance.Add(Guid.NewGuid(), data);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public IEnumerable<LogEntity> FindAll()
		{
			try
			{
				return DatabaseInstance.Values.OfType<LogEntity>();
			}
			catch
			{
				return Enumerable.Empty<LogEntity>();
			}
		}

		public bool DeleteAll()
		{
			try
			{
				DatabaseInstance.Clear();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public Task<bool> InsertAsync(LogEntity data)
		{
			return Task.FromResult(Insert(data));
		}

		public Task<IEnumerable<LogEntity>> FindAllAsync()
		{
			return Task.FromResult(FindAll());
		}

		public Task<bool> DeleteAllAsync()
		{
			return Task.FromResult(DeleteAll());
		}
			
	}
}
