using TransactionsExample.Domain;
using TransactionsExample.Services.Exceptions;

namespace TransactionsExample.Services;

public class ProductService : IProductService
{
    private IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> GetAll()
    {
        return await _productRepository.GetAll();
    }

    public async Task<Product> GetOne(int id)
    {
        var product = await _productRepository.GetOne(id);
        if (product == null)
        {
            throw new ProductNotFoundException(id);
        }
        return product;
    }
}