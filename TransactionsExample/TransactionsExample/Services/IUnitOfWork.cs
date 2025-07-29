using TransactionsExample.Domain;

namespace TransactionsExample.Services;

public interface IUnitOfWork : IDisposable
{
    IOrderRepository OrderRepository { get; }
    IProductRepository ProductRepository { get; }
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    Task SaveAsync();
}