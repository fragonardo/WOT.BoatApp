using BoatApp.Shared.Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BoatApp.Shared.Domain.Repository
{
    public abstract class EfRepositoryBase<TEntity, TId> : IUnitOfWork, IRepository<TEntity, TId> where TEntity : Entity<TId>
    {
        protected readonly DbContext _db;
        protected readonly DbSet<TEntity> _dbSet;
        protected IMediator _mediator;

        protected EfRepositoryBase(DbContext context, IMediator mediator)
        {
            _db = context;
            _dbSet = _db.Set<TEntity>();
            _mediator = mediator;
        }

        public async Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if(entity.IsTransient())
            {
                await _dbSet.AddAsync(entity, cancellationToken);
            }
            else
            {
                _dbSet.Update(entity);
            }
            _ = await SaveEntitiesAsync(cancellationToken);
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity); 
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public abstract Task<TEntity> FindAsync(TId Id, CancellationToken cancellationToken = default);
                        

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await DispatchDomainEventsAsync(cancellationToken);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            _ = await SaveChangesAsync(cancellationToken);

            return true;
        }

        private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken = default)
        {
            var domainEntities = _db.ChangeTracker
                .Entries<TEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent, cancellationToken);
        }

        public void Dispose()
        {
            _db.Dispose();            
        }
    }
}
