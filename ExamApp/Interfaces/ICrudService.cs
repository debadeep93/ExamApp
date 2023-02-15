using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamApp.Interfaces
{
    /// <summary>
    /// Base interface to abstract all CRUD operations.
    /// Implementing this interface leads to less duplication of code when implementing more features having the same required methods.
    /// </summary>
    public interface ICrudService<T> where T : class
    {
        /// <summary>
        /// Base method to retrieve a list of the entities.
        /// </summary>
        /// <returns>A list of entities</returns>
        Task<IEnumerable<T>> GetAllAsync();
        /// <summary>
        /// Base method to retrieve a single entity by Id
        /// </summary>
        /// <param name="id">The identifier of the entity</param>
        /// <returns>The entity corresponding to that Id</returns>
        Task<T> GetAsync(Guid id);

        /// <summary>
        /// Base method to retrieve a single entity by integer id.
        /// </summary>
        /// <param name="id">The integer identifier</param>
        /// <returns>The entity corresponding to that id</returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// Base method to add an entity
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns></returns>
        Task AddAsync(T entity);
        /// <summary>
        /// Base method to update an entity.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="course">The entity model with updated data.</param>
        /// <returns></returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Base method to delete an entity
        /// </summary>
        /// <param name="id">The identifier of the resource.</param>
        /// <returns></returns>
        Task DeleteAsync(T entity);
    }
}
