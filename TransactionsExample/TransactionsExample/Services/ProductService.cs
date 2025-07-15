using TransactionsExample.DTOs;
using TransactionsExample.Exceptions;
using TransactionsExample.Models;
using TransactionsExample.Repositories;

namespace TransactionsExample.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _unitOfWork.ProductRepository.GetAll();
        }

        public async Task<Product> GetOne(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetOne(id);
            if (product == null)
            {
                throw new ProductNotFoundException(id);
            }
            return product;
        }
    }
}
