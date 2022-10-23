using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{
	/// <summary>
	/// The wrapper for all database functions.
	/// </summary>
	/// <typeparam name="T">the database object model inherited from DataObject</typeparam>
	public interface ILogWrapper<LogEntity>
	{
		/// <summary>
		/// Insert the object into the database.
		/// </summary>
		/// <param name="data">object to insert</param>
		/// <returns>true on success</returns>
		bool Insert(LogEntity data);

		/// <summary>
		/// Get a list of all objects of the provided type.
		/// </summary>
		/// <returns>a lst of objects on success</returns>
		IEnumerable<LogEntity> FindAll();

		/// <summary>
		/// Delete all objects of the associated type.
		/// </summary>
		/// <returns>true on success</returns>
		bool DeleteAll();


		/// <summary>
		/// Insert the object into the database.
		/// </summary>
		/// <param name="data">object to insert</param>
		/// <returns>task for true on success</returns>
		Task<bool> InsertAsync(LogEntity data);

		/// <summary>
		/// Get a list of all objects of the provided type.
		/// </summary>
		/// <returns>task for a lst of objects on success</returns>
		Task<IEnumerable<LogEntity>> FindAllAsync();
		/// <summary>
		/// Delete all objects of the associated type.
		/// </summary>
		/// <returns>task for true on success</returns>
		Task<bool> DeleteAllAsync();
	}
}
