using BoatApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

namespace BoatApp.Infrastructure.Persistence;

public class BoatDbContext : DbContext
{
    public DbSet<Boat>? Boat { get; set; }
        
    private IDbContextTransaction? _currentTransaction;
    public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;

    public bool HasActiveTransaction => _currentTransaction != null;

    public BoatDbContext(DbContextOptions<BoatDbContext> options) : base(options)
    {
    }
    
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public async Task<IDbContextTransaction?> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction is not null) return null;
                
        _currentTransaction = await Database.BeginTransactionAsync(cancellationToken);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (HasActiveTransaction)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _currentTransaction?.RollbackAsync(cancellationToken);
        }
        finally
        {
            if (HasActiveTransaction)
            {
                _currentTransaction!.Dispose();
                _currentTransaction = null;
            }
        }
    }
    
}
