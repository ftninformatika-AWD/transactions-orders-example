using Microsoft.EntityFrameworkCore.Storage;
using TransactionsExample.Domain;
using TransactionsExample.Services;

namespace TransactionsExample.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction _transaction;

    public IOrderRepository OrderRepository { get; }
    public IProductRepository ProductRepository { get; }

    public UnitOfWork(AppDbContext context,
        IOrderRepository orderRepository,
        IProductRepository productRepository)
    {
        _context = context;
        OrderRepository = orderRepository;
        ProductRepository = productRepository;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
        await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
    }
}