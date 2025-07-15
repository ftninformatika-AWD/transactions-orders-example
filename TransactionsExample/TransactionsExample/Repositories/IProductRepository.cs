using TransactionsExample.Models;

namespace TransactionsExample.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetOne(int id);
        Task<List<Product>> GetAll();
        Task RemoveFromStock(Product product, int count);
        Task RemoveFromStockWithoutSave(Product product, int count);
    }
}
