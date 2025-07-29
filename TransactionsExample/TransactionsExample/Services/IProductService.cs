using TransactionsExample.Domain;

namespace TransactionsExample.Services;

public interface IProductService
{
    Task<List<Product>> GetAll();
    Task<Product> GetOne(int id);
}