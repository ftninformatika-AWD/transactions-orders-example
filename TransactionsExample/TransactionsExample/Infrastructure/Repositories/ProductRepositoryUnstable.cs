using Microsoft.EntityFrameworkCore;
using TransactionsExample.Domain;

namespace TransactionsExample.Infrastructure.Repositories;

public class ProductRepositoryUnstable : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepositoryUnstable(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Product>> GetAll()
    {
        return await _dbContext.Products
            .ToListAsync();
    }

    public async Task<Product?> GetOne(int id)
    {
        return await _dbContext.Products
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task RemoveFromStock(Product product, int count)
    {
        await Task.Delay(1000);
        throw new DbUpdateException("Removing from stock failed.");
    }

    public async Task RemoveFromStockWithoutSave(Product product, int count)
    {
        await Task.Delay(1000);
        throw new DbUpdateException("Removing from stock failed.");
    }
}