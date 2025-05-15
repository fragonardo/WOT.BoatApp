using BoatApp.Shared.Domain.Model;
using System.Linq.Expressions;

namespace BoatApp.Shared.Domain.Repository;


public interface IReadRepository<TEntity, TId> where TEntity : Entity<TId>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> query, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    Task<TEntity> FindAsync(TId Id, CancellationToken cancellationToken);

#if NET6_0_OR_GREATER
    /// <summary>
    /// Finds all entities of <typeparamref name="T" />, that matches the encapsulated query logic of the
    /// <paramref name="specification"/>, from the database.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <returns>
    ///  Returns an IAsyncEnumerable which can be enumerated asynchronously.
    /// </returns>
    //IAsyncEnumerable<T> AsAsyncEnumerable(ISpecification<T> specification);
#endif
}
