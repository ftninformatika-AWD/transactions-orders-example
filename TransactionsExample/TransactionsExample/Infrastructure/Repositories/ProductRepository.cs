using Microsoft.EntityFrameworkCore;
using TransactionsExample.Domain;

namespace TransactionsExample.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
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

    public async Task RemoveFromStockWithSave(Product product, int count)
    {
        product.Stock -= count;
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveFromStock(Product product, int count)
    {
        product.Stock -= count;
        _dbContext.Products.Update(product);
        await Task.CompletedTask;
    }
}