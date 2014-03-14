using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchRockers.Common.Interfaces
{
    public interface IDataContext
    {
        /// <summary>
        /// Returns a <see cref="IQueryable{T}"/> data for a given
        /// entity.
        /// </summary>
        IQueryable<T> Query<T>() where T : class;

        /// <summary>
        /// Inserts a given item to the data store.
        /// </summary>
        T Add<T>(T item) where T : class;

        /// <summary>
        /// Inserts a collection of data items to the data store.
        /// </summary>
        /// <remarks>
        /// The changes are commited to the data store once.
        /// </remarks>
        void AddAll<T>(IEnumerable<T> items) where T : class;

        /// <summary>
        /// Update a data item in the data store.
        /// </summary>
        void Update<T>(T item) where T : class;

        /// <summary>
        /// Update a collection of data items to the data store.
        /// </summary>
        /// <remarks>
        /// The changes are commited to the data store once.
        /// </remarks>
        void UpdateAll<T>(IEnumerable<T> items) where T : class;

        /// <summary>
        /// delete a data item from the data store.
        /// </summary>
        void Delete<T>(T item) where T : class;

        /// <summary>
        /// Delete a collection of data items from the data store.
        /// </summary>
        /// <remarks>
        /// The changes are commited to the data store once.
        /// </remarks>
        void DeleteAll<T>(IEnumerable<T> items) where T : class;
    }
}
