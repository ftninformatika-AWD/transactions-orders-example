namespace TransactionsExample.Domain;

public interface IProductRepository
{
    Task<Product?> GetOne(int id);
    Task<List<Product>> GetAll();
    Task RemoveFromStockWithSave(Product product, int count);
    Task RemoveFromStock(Product product, int count);
}