using BoatApp.Shared.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApp.Shared.Domain.Repository
{
    public interface IRepository<TEntity, TId> : IReadRepository<TEntity, TId> where TEntity : Entity<TId>
    {
        /// <summary>
        /// Adds an entity in the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the <typeparamref name="T" />.
        /// </returns>
        Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds an entity in the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the <typeparamref name="T" />.
        /// </returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds the given entities in the database
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an entity in the database
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        void Update(TEntity entity);

        /// <summary>
        /// Updates the given entities in the database
        /// </summary>
        /// <param name="entities">The entities to update.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        //Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes an entity in the database
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        void Delete(TEntity entity);

        /// <summary>
        /// Removes the given entities in the database
        /// </summary>
        /// <param name="entities">The entities to remove.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        void DeleteRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Removes the all entities of <typeparamref name="T" />, that matches the encapsulated query logic of the
        /// <paramref name="specification"/>, from the database.
        /// </summary>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        //Task DeleteRangeAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Persists changes to the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
